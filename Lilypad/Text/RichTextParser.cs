namespace Lilypad.Text; 

public static class RichTextParser {
    static int _index;
    
    static List<RichText> _result = null!;

    static string _rawText = "";
    static string _input = null!;

    static bool EndOfInput => _index >= _input.Length;

    static readonly List<FormatParser> _formatParsers;
    static readonly List<ContentParser> _contentParsers;
    
    static RichTextParser() {
        _formatParsers = new List<FormatParser> {
            new ColorParser(),
            new InsertionParser(),
            new ClickEventParser()
        };
        
        _formatParsers.AddRange(Enum.GetValues<TextStyle>().Select(style => new StyleParser(style)));
        
        _contentParsers = new List<ContentParser> {
            new EntityNameParser(),
            new KeybindParser(),
            new ScoreParser(),
            new VariableParser(),
            new NBTParser()
        };
    }
    
    static void Reset() {
        _index = 0;
        _result = new List<RichText>();
        _rawText = "";
        
        foreach (var tagParser in _formatParsers) {
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
                    if (!_formatParsers.Any(tagParser => tagParser.OnClosingTag(tag))) {
                        throw new ArgumentException($"Invalid closing tag: {tag}");
                    }
                    AddText();
                } else {
                    var found = _formatParsers.Any(tagParser => tagParser.OnOpeningTag(tag, arguments));

                    if (found) {
                        AddText();
                    } else {
                        foreach (var tagParser in _contentParsers) {
                            if (!tagParser.TryParse(tag, arguments, out var content)) continue;
                            var tags = content.ToArray();

                            AddText();
                            AddComponent(tags.First(), tags.Skip(1));
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
        
        var argList = new List<string>();
        
        var arg = "";
        var inQuotes = false;
        
        foreach (var c in body) {
            switch (c) {
                case ' ' when !inQuotes: {
                    if (arg.Length > 0) {
                        argList.Add(arg);
                        arg = "";
                    }

                    break;
                }
                
                case '\'':
                    inQuotes = !inQuotes; break;
                default:
                    arg += c; break;
            }
        }
        
        if (arg.Length > 0) argList.Add(arg);
        arguments = argList.Skip(1).ToArray();
        
        return argList[0];
    }
    
    static void AddComponent(TextTag content, IEnumerable<TextTag>? extraFormats = null) {
        var text = new RichText {
            Content = content,
        };

        var formats = _formatParsers
            .Select(parser => parser.GetState())
            .Where(state => state != null)
            .SelectMany(state => state!);
        
        if (extraFormats != null) {
            formats = formats.Concat(extraFormats);
        }
        
        text.Formatting.AddRange(formats);
        _result.Add(text);
    }
    
    static void AddText() {
        if (_rawText.Length == 0) return;
        AddComponent(("text", _rawText));
        _rawText = "";
    }
    
    static char Peek() => _input[_index];
    static char Advance() => _input[_index++];
}