using System.Diagnostics.CodeAnalysis;

namespace Lilypad.Text;

public class EntityNameParser : ContentParser {
    public override bool TryParse(string tag, string[] arguments, [NotNullWhen(true)] out IEnumerable<TextTag>? contentTag) {
        contentTag = null;
        if (tag != "entity") return false;
        
        AssertArgumentCount(arguments, 1);
        contentTag = GetState(("selector", arguments[0]));
        
        return true;
    }
}