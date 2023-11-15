namespace Lilypad.Extensions; 

internal static class NullableExtensions {
    public static string ToStringOrEmpty<T>(this T? value) {
        return value?.ToString() ?? string.Empty;
    }
}