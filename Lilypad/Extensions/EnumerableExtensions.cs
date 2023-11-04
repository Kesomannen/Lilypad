namespace Lilypad.Extensions; 

internal static class EnumerableExtensions {
    public static IEnumerable<T> AsEnumerable<T>(this T item) {
        yield return item;
    }
}