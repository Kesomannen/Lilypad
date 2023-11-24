using Lilypad.Helpers;

namespace Lilypad.Text; 

public class VariableParser : ContentParser {
    readonly ScoreParser _scoreParser = new();
    readonly NBTParser _nbtParser = new();
    
    public override bool TryParse(string tag, string[] arguments, out IEnumerable<TextTag>? tags) {
        tags = null;
        if (!tag.StartsWith("Variable")) return false;
        AssertArgumentCount(arguments, 0);

        var segments = tag.Replace(IVariable.Space.ToString(), " ").Split(IVariable.Separator);
        var type = segments[1].ToLower();
        
        if (_scoreParser.TryParse(type, segments[2..], out tags))
            return true;
        
        if (_nbtParser.TryParse(type, segments[2..], out tags))
            return true;
        
        throw new ArgumentException($"Unknown variable type: {type}");
    }
}