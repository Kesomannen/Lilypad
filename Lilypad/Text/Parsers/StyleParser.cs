using Lilypad.Extensions;

namespace Lilypad.Text; 

public class StyleParser : FormatParser {
    bool? _active;
    
    readonly TextStyle _style;
    
    public StyleParser(TextStyle style) {
        _style = style;
    }

    public override void Reset() {
        _active = null;
    }

    public override ITextFormat? GetState() {
        return _active.HasValue ? new StyleFormat {
            Style = _style, Active = _active.Value
        } : null;
    }
    
    public override bool OnOpeningTag(string tag, string[] arguments) {
        if (!Matches(tag)) return false;
        
        AssertArgumentCount(arguments, 0);
        _active = true;
        return true;
    }

    public override bool OnClosingTag(string closingTag) {
        if (!Matches(closingTag)) return false;
        
        _active = false;
        return true;
    }
    
    bool Matches(string tag) {
        var style = _style.ToString().ToSnakeCase();
        return tag == style || (tag.Length == 1 && tag[0] == style[0]);
    }
}