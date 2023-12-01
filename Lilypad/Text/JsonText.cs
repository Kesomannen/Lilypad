using Lilypad.Extensions;
using Newtonsoft.Json;

namespace Lilypad.Text; 

/// <summary>
/// Represents a Minecraft Raw JSON text component, used for displaying rich text.
/// Comprised of multiple <see cref="TextTag"/>s, with one dedicated to the
/// content of the tag and others for formatting. Can also contain other compounds
/// as children, which are placed in the 'extra' tag when serialized.
/// 
/// <br/><br/>Lilypad provides an easy way of creating these
/// through <see cref="JsonTextParser"/>, which parses a string
/// into a <see cref="JsonText"/>. You can also use the parser with the
/// implicit conversion from string.
/// </summary>
/// <example><code>
/// var jsonText = new JsonText {
///     Content = new TextTag("text", "Hello, world!"),
///     ["color"] = "red"
/// }; 
/// </code>
/// <br/>equivalent to
/// <code>
/// JsonText jsonText = "&lt;red&gt;Hello, world!";
/// </code>
/// <br/>and serializes to
/// <code>
/// {"text":"Hello, world","color":"red"}
/// </code>
/// </example>
public class JsonText : ISerializeInnerJson, ICustomNBTSerializer {
    public TextTag? Content { get; set; }
    public List<TextTag> Formatting { get; set; } = new();
    public List<JsonText> Children { get; set; } = new();
    
    public static readonly JsonText Empty = new();
    
    /// <summary>
    /// Constructs an empty <see cref="JsonText"/>.
    /// </summary>
    public JsonText() { }

    /// <summary>
    /// Constructs a <see cref="JsonText"/> with the given <see cref="Children"/>.
    /// </summary>
    public JsonText(params JsonText[] children) {
        Children.AddRange(children);
    }
    
    /// <summary>
    /// Constructs a <see cref="JsonText"/> with the given string as content.
    /// </summary>
    public JsonText(string text) : this(("text", text)) { }

    /// <summary>
    /// Constructs a <see cref="JsonText"/> with the given content and formatting tags.
    /// </summary>
    public JsonText(TextTag content, params TextTag[] formatting) {
        Content = content;
        Formatting.AddRange(formatting);
    }

    public object this[string name] {
        get {
            foreach (var textTag in Formatting.Where(textTag => textTag.Name == name)) {
                return textTag.Value;
            }
            throw new KeyNotFoundException($"No formatting tag with name {name} found.");
        }
        set {
            for (var i = 0; i < Formatting.Count; i++) {
                if (Formatting[i].Name != name) continue;
                Formatting[i] = new TextTag(name, value);
                return;
            }
            Formatting.Add(new TextTag(name, value));
        }
    }

    public string Serialize() {
        return Json.Serialize(ToJson(true));
    }

    public override string ToString() {
        return Serialize();
    }

    object ToJson(bool isTopLevel) {
        if (!Content.HasValue && Children.Count == 1) {
            return Children[0].ToJson(isTopLevel);
        }
        if (isTopLevel && !Content.HasValue && Children.Count > 1) {
            return GetChildJson();
        }

        var json = new Dictionary<string, object>();

        if (Content.HasValue) {
            json[Content.Value.Name] = Content.Value.Value;
        } else {
            json["text"] = string.Empty;
        }
        
        foreach (var textFormat in Formatting) {
            json[textFormat.Name] = textFormat.Value;
        }
        if (Children.Count > 0) {
            json["extra"] = GetChildJson();
        }
        return json;
        
        object[] GetChildJson() {
            return Children.Select(child => child.ToJson(false)).ToArray();
        }
    }

    public static implicit operator JsonText(string text) {
        return Parse(text);
    }
    
    public static JsonText Parse(string text) {
        var children = JsonTextParser.Parse(text);
        if (children.Count == 1) {
            return children[0];
        }
        var result = new JsonText();
        result.Children.AddRange(children);
        return result;
    }
    
    public static JsonText? ParseJson(string text) {
        var json = Json.Deserialize<Dictionary<string, object>>(text);
        return json == null ? null : ParseJson(json);
    }

    public static JsonText? ParseJson(Dictionary<string, object> json) {
        var result = new JsonText();
        foreach (var (key, value) in json) {
            switch (key) {
                case "extra":
                    if (value is not object[] children) continue;
                    foreach (var child in children) {
                        if (child is not Dictionary<string, object> childJson) continue;
                        
                        var childText = ParseJson(childJson);
                        if (childText == null) continue;
                        result.Children.Add(childText);
                    }
                    break;
                case "text":
                    result.Content = (key, value);
                    break;
                default:
                    result[key] = value;
                    break;
            }
        }
        return result;
    }
    
    public static JsonText operator +(JsonText left, JsonText right) {
        return new JsonText(left, right);
    }

    object ISerializeInnerJson.SerializedData => ToJson(true);
    string ICustomNBTSerializer.Serialize() => Serialize().Quote('\'');
}