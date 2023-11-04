namespace Lilypad.Text; 

public abstract class FormatParser : Parser {
    public abstract bool OnOpeningTag(string tag, string[] arguments);
    public abstract bool OnClosingTag(string closingTag);

    public abstract IEnumerable<TextTag>? GetState();
    public abstract void Reset();
}