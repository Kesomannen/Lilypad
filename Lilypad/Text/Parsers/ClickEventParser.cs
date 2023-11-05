using Lilypad.Extensions;

namespace Lilypad.Text; 

public class ClickEventParser : FormatParser {
    ClickEvent? _clickEvent;
    
    public override bool OnOpeningTag(string tag, string[] arguments) {
        var isExplicit = tag == "on_click";
        if (isExplicit) {
            AssertArgumentCount(arguments, 2);
        } else if (arguments.Length == 0) {
            return false;
        }

        if (!TryParseAction(isExplicit ? arguments[0] : tag, out var clickAction)) {
            if (isExplicit)
                throw new ArgumentException($"Invalid click action '{arguments[0]}'");
            return false;
        }

        if (!isExplicit) {
            AssertArgumentCount(arguments, 1);
        }
        
        var value = arguments[isExplicit ? 1 : 0];
        
        if (clickAction == ClickAction.RunCommand && value[0] != '/') {
            value = $"/function {value}";
        }
        
        _clickEvent = new ClickEvent {
            Action = clickAction, 
            Value = value
        };
        return true;
    }

    public override bool OnClosingTag(string closingTag) {
        if (closingTag != "on_click" && !TryParseAction(closingTag, out _)) return false;
        
        _clickEvent = null;
        return true;
    }

    public override IEnumerable<TextTag>? GetState() {
        return _clickEvent == null ? null : GetState(("clickEvent", _clickEvent.Value));
    }

    public override void Reset() {
        _clickEvent = null;
    }

    static bool TryParseAction(string action, out ClickAction clickAction) {
        if (Enum.TryParse(action.ToCamelCase(), true, out clickAction)) return true;
        
        switch (action) {
            case "url":
                clickAction = ClickAction.OpenUrl; break;
            case "run":
                clickAction = ClickAction.RunCommand; break;
            case "suggest":
                clickAction = ClickAction.SuggestCommand; break;
            case "page":
                clickAction = ClickAction.ChangePage; break;
            case "copy":
                clickAction = ClickAction.CopyToClipboard; break;
            default: return false;
        }
        
        return true;
    }
}

public struct ClickEvent {
    public EnumReference<ClickAction> Action;
    public string Value;
}

public enum ClickAction {
    OpenUrl,
    RunCommand,
    SuggestCommand,
    ChangePage,
    CopyToClipboard
}