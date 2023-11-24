namespace Lilypad.Text; 

public abstract class ContentParser : Parser {
    public abstract bool TryParse(string tag, string[] arguments, out IEnumerable<TextTag>? tags);
}