namespace Lilypad.Text; 

public class RichText {
    public ITextContent? Content { get; set; }
    public List<ITextFormat> Formatting { get; set; } = new();
    public List<RichText> Children { get; set; } = new();
    
    object ToJson() {
        if (Content == null) {
            return GetChildJson();
        }

        var json = new Dictionary<string, object> {
            [Content.Name] = Content.Value
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

    public static implicit operator RichText(string text) {
        var children = RichTextParser.Parse(text);
        if (children.Count == 1) {
            return children[0];
        }
        var result = new RichText();
        result.Children.AddRange(children);
        return result;
    }
}