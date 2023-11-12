namespace Lilypad.Extensions; 

internal static class StringExtensions {
    public static string ToCamelCase(this string text) {
        return string.Concat(text.Split('_').Select(word => char.ToUpper(word[0]) + word[1..]));
    }
    
    public static string ToSnakeCase(this string text) {
        return string.Concat(text.Select((character, index) => index > 0 && char.IsUpper(character) ? "_" + character : character.ToString())).ToLower();
    }
    
    public static string Escape(this string text, char[] charsToEscape, char escapeChar = '\\') {
        return string.Concat(text.Select(c => charsToEscape.Contains(c) ? $"{escapeChar}{c}" : c.ToString()));
    }
    
    public static string Escape(this string text, char charToEscape, char escapeChar = '\\') {
        return text.Escape(new[] { charToEscape }, escapeChar);
    }
    
    public static string Quote(this string text, char quoteChar = '"') {
        return $"{quoteChar}{text.Escape(quoteChar)}{quoteChar}";
    }
}