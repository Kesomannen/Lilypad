using System.Diagnostics.CodeAnalysis;

namespace Lilypad.Text;

public class EntityNameParser : ContentParser {
    public override bool TryParse(string tag, string[] arguments, [NotNullWhen(true)] out IEnumerable<TextTag>? tags) {
        tags = null;
        if (tag != "entity") return false;
        
        AssertArgumentCount(arguments, 1);
        tags = GetState(("selector", arguments[0]));
        
        return true;
    }
}