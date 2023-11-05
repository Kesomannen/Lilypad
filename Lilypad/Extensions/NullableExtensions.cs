namespace Lilypad.Extensions; 

public static class NullableExtensions {
    public static string ToStringOrEmpty<T>(this T? value, string defaultValue = "") {
        return value?.ToString() ?? defaultValue;
    }
}