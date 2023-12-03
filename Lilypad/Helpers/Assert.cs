namespace Lilypad.Helpers; 

internal static class Assert {
    public static void IsTrue(bool condition, string message) {
        if (!condition) throw new ArgumentException(message);
    }
    
    public static void IsFalse(bool condition, string message) {
        if (condition) throw new ArgumentException(message);
    }
    
    public static void NotNull(object? obj, string paramName) {
        if (obj == null) throw new ArgumentNullException(paramName);
    }

    public static void IsFinite<T>(Range<T?> range, string paramName) where T : struct {
        if (range.Min is null || range.Max is null) {
            throw new ArgumentException($"Range {paramName} cannot be inifinite");
        }
    }
}