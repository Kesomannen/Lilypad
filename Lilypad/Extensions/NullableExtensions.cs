namespace Lilypad.Extensions; 

public static class NullableExtensions {
    public static string ToStringOrEmpty<T>(this T? value) {
        return value?.ToString() ?? string.Empty;
    }
}