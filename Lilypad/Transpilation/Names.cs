using Lilypad.Extensions;

namespace Lilypad; 

internal static class Names {
    static readonly Dictionary<string, int> _counts = new();
    static readonly Dictionary<Type, string> _typeNames = new();

    public static string Get<T>() {
        if (_typeNames.TryGetValue(typeof(T), out var name)) {
            return Get(name);
        }
        
        var type = typeof(T);
        if (type.IsGenericType) {
            type = type.GetGenericArguments()[0];
        } else if (type.IsArray) {
            type = type.GetElementType()!;
        }
        
        name = type.Name.ToSnakeCase();
        _typeNames[type] = name;
        return Get(name);
    }
    
    public static string Get(string category) {
        if (!_counts.TryGetValue(category, out var count)) {
            count = 0;
        }
        _counts[category] = count + 1;
        return $"{category}_{count}";
    }
}