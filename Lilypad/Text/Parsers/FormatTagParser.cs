namespace Lilypad.Text; 

public abstract class FormatTagParser {
    public abstract bool OnOpeningTag(string tag, string[] arguments);
    public abstract bool OnClosingTag(string closingTag);

    public abstract ITextFormat? GetState();
    public abstract void Reset();

    protected static void AssertArgumentCount(string[] arguments, int count) {
        if (arguments.Length != count) {
            throw new ArgumentException($"Expected {count} arguments, got {arguments.Length}.");
        }
    }
}