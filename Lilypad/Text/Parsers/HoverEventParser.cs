using Lilypad.Extensions;
using Newtonsoft.Json;

namespace Lilypad.Text; 

public class HoverEventParser : FormatParser {
    HoverEvent? _hoverEvent;
    
    public override bool OnOpeningTag(string tag, string[] arguments) {
        var isExplicit = tag == "on_hover";
        var basis = isExplicit ? 0 : -1;
        if (arguments.Length == 0) {
            if (isExplicit) {
                throw new ArgumentException($"Expected 2 or more arguments, got {arguments.Length} instead. Arguments: {string.Join(", ", arguments)}");
            }

            return false;
        }

        if (!TryParseAction(isExplicit ? arguments[0] : tag, out var hoverAction)) {
            if (isExplicit)
                throw new ArgumentException($"Invalid hover action '{arguments[0]}'");
            return false;
        }

        object contents;
        switch (hoverAction) {
            case HoverAction.ShowText:
                AssertArgumentCount(arguments, basis + 2);
                contents = arguments[basis + 1];
                break;
            case HoverAction.ShowItem:
                AssertArgumentCount(arguments, basis + 1, basis + 4);
                var itemAction = new ItemHoverAction {
                    Item = arguments[basis + 1]
                };
                if (arguments.Length >= basis + 3) {
                    if (!int.TryParse(arguments[basis + 2], out var count)) {
                        throw new ArgumentException($"Invalid item count '{arguments[basis + 2]}'");
                    }
                    itemAction.Count = count;
                }
                if (arguments.Length == basis + 4) {
                    itemAction.Tag = arguments[basis + 3];
                }
                contents = itemAction;
                break;
            case HoverAction.ShowEntity:
                AssertArgumentCount(arguments, basis + 2, basis + 4);
                var entityAction = new EntityHoverAction {
                    Type = arguments[basis + 1],
                    Id = arguments[basis + 2]
                };
                if (arguments.Length == basis + 4) {
                    var name = arguments[basis + 3];
                    var text = JsonText.ParseJson(name) ?? JsonText.Parse(name);
                    entityAction.Name = text;
                }
                contents = entityAction;
                break;
            default: throw new ArgumentOutOfRangeException();
        }
        
        _hoverEvent = new HoverEvent {
            Action = hoverAction, 
            Contents = contents
        };
        return true;
    }

    public override bool OnClosingTag(string closingTag) {
        if (closingTag != "on_hover" && !TryParseAction(closingTag, out _)) return false;
        
        _hoverEvent = null;
        return true;
    }

    public override IEnumerable<TextTag>? GetState() {
        return _hoverEvent == null ? null : GetState(("hoverEvent", _hoverEvent.Value));
    }

    public override void Reset() {
        _hoverEvent = null;
    }

    static bool TryParseAction(string action, out HoverAction hoverAction) {
        return Enum.TryParse(action.ToCamelCase(), true, out hoverAction);
    }
}

public struct HoverEvent {
    public EnumReference<HoverAction> Action;
    public object Contents;
}

public struct ItemHoverAction {
    [JsonProperty("id")]
    public EnumReference<Item> Item;
    public int? Count;
    public string? Tag;
}

public struct EntityHoverAction {
    public EnumReference<Entity> Type;
    public Argument<Uuid> Id;
    public JsonText? Name;
}

/// <summary>
/// The type of tooltip to show.
/// </summary>
public enum HoverAction {
    /// <summary>
    /// Shows a <see cref="JsonText"/>.
    /// </summary>
    ShowText,
    
    /// <summary>
    /// Shows the tooltip of an item as if it was being hovering over it in an inventory.
    /// </summary>
    ShowItem,
    
    /// <summary>
    /// Shows an entity's name, type, and <see cref="Uuid"/>.
    /// </summary>
    ShowEntity
}