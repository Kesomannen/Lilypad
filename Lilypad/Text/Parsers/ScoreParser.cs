namespace Lilypad.Text; 

public class ScoreParser : ContentParser {
    public override bool TryParse(string tag, string[] arguments, out IEnumerable<TextTag>? tags) {
        tags = null;
        if (tag != "score") return false;
        
        AssertArgumentCount(arguments, 2);
        tags = GetState(("score", new Tag {
            Objective = arguments[0],
            Name = arguments[1]
        }));
        
        return true;
    }

    struct Tag {
        public string Objective;
        public string Name;
    }
}