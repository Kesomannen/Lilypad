namespace Lilypad.Text; 

public static class RichTextParser {
    static int _index;
    
    static List<RichText> _result = null!;

    static string _rawText = "";
    static string _input = null!;

    static bool EndOfInput => _index >= _input.Length;

    static readonly List<FormatTagParser> _formatTagParsers;
    static readonly List<ContentTagParser> _contentTagParsers;
    
    static RichTextParser() {
        _formatTagParsers = new List<FormatTagParser> {
            new ColorTagParser()
        };
        
        _formatTagParsers.AddRange(Enum.GetValues<TextStyle>().Select(style => new StyleTagParser(style)));
        
        _contentTagParsers = new List<ContentTagParser> {
            
        };
    }
    
    static void Reset() {
        _index = 0;
        _result = new List<RichText>();
        _rawText = "";
        
        foreach (var tagParser in _formatTagParsers) {
            tagParser.Reset();
        }
        foreach (var tagParser in _contentTagParsers) {
            tagParser.Reset();
        }
    }
    
    public static List<RichText> Parse(string input) {
        Reset();
        _input = input;
        
        while (!EndOfInput) {
            if (Peek() == '<') {
                AddText();
                var tag = ParseTag(out var arguments, out var isClosing);
                
                if (isClosing) {
                    if (!_formatTagParsers.Any(tagParser => tagParser.OnClosingTag(tag))) {
                        throw new ArgumentException($"Invalid closing tag: {tag}");
                    }
                    AddText();
                } else {
                    var found = _formatTagParsers.Any(tagParser => tagParser.OnOpeningTag(tag, arguments));

                    if (found) {
                        AddText();
                    } else {
                        foreach (var tagParser in _contentTagParsers) {
                            if (!tagParser.TryParse(tag, arguments, out var content)) continue;

                            AddText();
                            AddComponent(content);
                            found = true;
                            break;
                        }
                    }

                    if (!found) {
                        throw new ArgumentException($"Invalid opening tag: {tag}");
                    }
                }
            } else {
                _rawText += Advance();
            }
        }
        AddText();
        return _result;
    }
    
    static string ParseTag(out string[] arguments, out bool isClosing) {
        Advance();
        isClosing = Peek() == '/';
        if (isClosing) Advance();

        var body = "";
        while (!EndOfInput && Peek() != '>') {
            body += Advance();
        }
        Advance();
        
        var tag = body.Split(' ')[0];
        arguments = body.Split(' ').Skip(1).ToArray();
        return tag;
    }
    
    static void AddComponent(ITextContent content) {
        var text = new RichText {
            Content = content,
        };

        var formats = _formatTagParsers
            .Select(parser => parser.GetState())
            .Where(state => state != null);
        
        text.Formatting.AddRange(formats);
        _result.Add(text);
    }
    
    static void AddText() {
        if (_rawText.Length == 0) return;
        AddComponent(new RawTextContent { Text = _rawText });
        _rawText = "";
    }
    
    static char Peek() => _input[_index];
    static char Advance() => _input[_index++];
}