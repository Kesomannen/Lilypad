namespace Lilypad.Text; 

/// <summary>
/// Parses a string into a list of <see cref="JsonText"/>s.
/// Supports HTML-style tags to apply formatting and add content.
/// Can be used through the implicit conversion from string in <see cref="JsonText"/>.
///
/// <br/><br/>Supported formatting tags:
/// <list type="bullet">
///     <item><description><c>&lt;bold&gt;</c> or <c>&lt;b&gt;</c></description></item>
///     <item><description><c>&lt;italic&gt;</c> or <c>&lt;i&gt;</c></description></item>
///    <item><description><c>&lt;underlined&gt;</c> or <c>&lt;u&gt;</c></description></item>
///     <item><description><c>&lt;strikethrough&gt;</c> or <c>&lt;s&gt;</c></description></item>
///     <item><description><c>&lt;obfuscated&gt;</c> or <c>&lt;o&gt;</c></description></item>
///     <item><description>
///         <c>&lt;color [color]&gt;</c>
///         <br/>Where <c>[color]</c> is a <see cref="TextColor"/> in <c>snake_case</c>.
///     </description></item>
///     <item><description><c>&lt;color #RRGGBB&gt;</c></description></item>
/// </list>
/// </summary>
public static class JsonTextParser {
    static int _index;
    
    static List<JsonText> _result = null!;

    static string _rawText = "";
    static string _input = null!;

    static bool EndOfInput => _index >= _input.Length;

    static readonly List<FormatParser> _formatParsers;
    static readonly List<ContentParser> _contentParsers;
    
    static JsonTextParser() {
        _formatParsers = new List<FormatParser> {
            new ColorParser(),
            new InsertionParser(),
            new ClickEventParser(),
            new HoverEventParser()
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
    
    public static void AddFormatParser(FormatParser parser) {
        _formatParsers.Add(parser);
    }
    
    public static void AddContentParser(ContentParser parser) {
        _contentParsers.Add(parser);
    }
    
    static void Reset() {
        _index = 0;
        _result = new List<JsonText>();
        _rawText = "";
        
        foreach (var tagParser in _formatParsers) {
            tagParser.Reset();
        }
    }
    
    public static List<JsonText> Parse(string input) {
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
        var escaped = false;
        
        foreach (var c in body) {
            switch (c) {
                case ' ' when !inQuotes: {
                    if (arg.Length > 0) {
                        argList.Add(arg);
                        arg = "";
                    }

                    break;
                }
                
                case '\'' when !escaped:
                    inQuotes = !inQuotes;
                    break;
                
                case '\\':
                    if (escaped) {
                        arg += '\\';
                        escaped = false;
                    } else {
                        escaped = true;
                    }
                    break;
                
                default:
                    arg += c; 
                    escaped = false;
                    break;
            }
        }
        
        if (arg.Length > 0) argList.Add(arg);
        arguments = argList.Skip(1).ToArray();
        
        return argList[0];
    }
    
    static void AddComponent(TextTag content, IEnumerable<TextTag>? extraFormats = null) {
        var text = new JsonText {
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