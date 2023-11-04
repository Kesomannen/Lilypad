using System.Diagnostics.CodeAnalysis;

namespace Lilypad.Text; 

public abstract class ContentParser : Parser {
    public abstract bool TryParse(string tag, string[] arguments, [NotNullWhen(true)] out IEnumerable<TextTag>? tags);
}