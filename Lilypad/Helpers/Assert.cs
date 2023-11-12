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
}