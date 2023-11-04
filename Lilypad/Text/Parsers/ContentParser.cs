using System.Diagnostics.CodeAnalysis;

namespace Lilypad.Text; 

public abstract class ContentParser {
    public abstract bool TryParse(string tag, string[] arguments, [NotNullWhen(true)] out ITextContent? content);
    public abstract void Reset();

    protected void AssertArgumentCount(string[] arguments, int count) {
        if (arguments.Length != count) {
            throw new ArgumentException($"Expected {count} arguments, got {arguments.Length}.");
        }
    }
}