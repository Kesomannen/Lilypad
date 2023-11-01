using Lilypad.Extensions;

namespace Lilypad.Text; 

public class StyleTagParser : FormatTagParser {
    bool _active;
    bool _set;
    
    readonly TextStyle _style;
    
    public StyleTagParser(TextStyle style) {
        _style = style;
    }

    public override void Reset() {
        _active = false;
    }

    public override ITextFormat? GetState() {
        return _set ? new StyleFormat { Style = _style, Active = _active } : null;
    }
    
    public override bool OnOpeningTag(string tag, string[] arguments) {
        if (!Matches(tag)) return false;
        
        AssertArgumentCount(arguments, 0);
        _set = true;
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