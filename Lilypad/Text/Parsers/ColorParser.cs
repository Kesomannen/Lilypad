using System.Collections;
using Lilypad.Extensions;

namespace Lilypad.Text; 

public class ColorParser : FormatParser {
    readonly Stack<ColorFormat> _formats = new();
    
    bool _set;
    
    static readonly ColorFormat _defaultFormat = new(TextColor.White);

    public override IEnumerable<TextTag>? GetState() {
        if (!_set) return null;
        var format = _formats.Count > 0 ? _formats.Peek() : _defaultFormat;
        
        return format.Type switch {
            ColorType.ChatColor => GetState(("color", format.Color!.Value)),
            ColorType.Hex => GetState(("color", "#" + format.HexString!)),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public override bool OnOpeningTag(string tag, string[] arguments) {
        TextColor color;

        if (tag == "color") {
            AssertArgumentCount(arguments, 1);

            var arg = arguments[0].ToCamelCase();
            
            if (arg.StartsWith("#")) {
                _formats.Push(new ColorFormat(arg[1..]));
            } else if (Enum.TryParse(arg, out color)) {
                _formats.Push(new ColorFormat(color));
            } else {
                throw new ArgumentException($"Invalid color argument: {arg}");
            }

            _set = true;
            return true;
        }

        if (Enum.TryParse(tag.ToCamelCase(), out color)) {
            AssertArgumentCount(arguments, 0);
            _formats.Push(new ColorFormat(color));
            
            _set = true;
            return true;
        }
        
        return false;
    }

    public override bool OnClosingTag(string closingTag) {
        if (closingTag != "color" && !Enum.TryParse(closingTag.ToCamelCase(), out TextColor _)) return false;
        
        _formats.Pop();
        return true;
    }

    public override void Reset() {
        _formats.Clear();
        _set = false;
    }

    enum ColorType {
        ChatColor,
        Hex
    }

    readonly struct ColorFormat {
        public readonly ColorType Type;
        public readonly string? HexString;
        public readonly TextColor? Color;

        public ColorFormat(TextColor color) {
            Type = ColorType.ChatColor;
            Color = color;
        }
        
        public ColorFormat(string hexString) {
            Type = ColorType.Hex;
            HexString = hexString;
        }
    }
}