namespace Lilypad.Extensions; 

internal static class StringExtensions {
    public static string ToCamelCase(this string text) {
        return string.Concat(text.Split('_').Select(word => char.ToUpper(word[0]) + word[1..]));
    }
    
    public static string ToSnakeCase(this string text) {
        return string.Concat(text.Select((character, index) => index > 0 && char.IsUpper(character) ? "_" + character : character.ToString())).ToLower();
    }
}