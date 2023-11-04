namespace Lilypad.Text; 

public class InsertionParser : FormatParser {
    string? _text;
    
    public override bool OnOpeningTag(string tag, string[] arguments) {
        if (tag != "insert") return false;
        
        AssertArgumentCount(arguments, 1);
        _text = arguments[0];
        return true;
    }

    public override bool OnClosingTag(string closingTag) {
        if (closingTag != "insert") return false;
        
        _text = null;
        return true;
    }

    public override ITextFormat? GetState() {
        return _text != null ? new InsertionFormat { Text = _text } : null;
    }

    public override void Reset() {
        _text = null;
    }
}