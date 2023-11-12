namespace Lilypad.Text; 

public class JsonText {
    public TextTag? Content { get; set; }
    public List<TextTag> Formatting { get; set; } = new();
    public List<JsonText> Children { get; set; } = new();
    
    public static readonly JsonText Empty = new();
    
    public JsonText() { }

    public JsonText(params JsonText[] children) {
        Children.AddRange(children);
    }
    
    public JsonText(string text) : this(("text", text)) { }

    public JsonText(TextTag content, params TextTag[] formatting) {
        Content = content;
        Formatting.AddRange(formatting);
    }
    
    object ToJson() {
        if (Content == null) {
            if (Children.Count == 0) {
                return string.Empty;
            }
            
            return GetChildJson();
        }

        var json = new Dictionary<string, object> {
            [Content.Value.Name] = Content.Value.Value
        };
        
        foreach (var textFormat in Formatting) {
            json[textFormat.Name] = textFormat.Value;
        }
        if (Children.Count > 0) {
            json["extra"] = GetChildJson();
        }
        return json;
        
        object[] GetChildJson() {
            return Children.Select(child => child.ToJson()).ToArray();
        }
    }

    public override string ToString() {
        return Json.Serialize(ToJson());
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
}