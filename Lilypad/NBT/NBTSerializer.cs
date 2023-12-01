using System.Reflection;
using System.Text;
using Lilypad.Extensions;
using Lilypad.Text;

namespace Lilypad;

public static class NBTSerializer {
    public static string Serialize(NBT compound) {
        var builder = new StringBuilder();
        builder.Append('{');
        
        var first = true;
        foreach (var (name, tag) in compound) {
            var value = SerializeValue(tag);
            if (value == null) continue;

            if (!first) {
                builder.Append(',');
            }
            first = false;
            
            builder.Append(name);
            builder.Append(':');
            builder.Append(value);
        }
        
        builder.Append('}');
        return builder.ToString();
    }

    public static string? SerializeValue(object? obj) {
        if (obj is null) return null;

        var str = obj.ToString();
        if (str == null) {
            throw new ArgumentNullException(nameof(str), "String representation of NBT Value cannot be null!");
        }
        return obj switch {
            sbyte => str + 'b',
            byte => str + 'b',
            bool => str.ToLower(),
            short => str + 's',
            int => str,
            long => str + 'l',
            float => str + 'f',
            double => str + 'd',
            string => str.Quote(),
            ICustomNBTSerializer serializer => serializer.Serialize(),
            NBT compound => Serialize(compound),
            IEnumerable<byte> bytes => SerializeArray(bytes, "B"),
            IEnumerable<int> integers => SerializeArray(integers, "I"),
            IEnumerable<long> longs => SerializeArray(longs, "L"),
            IEnumerable<object> values => SerializeArray(values, serialize: SerializeValue),
            Enum @enum => SerializeEnum(@enum),
            _ => throw new ArgumentException($"Invalid NBT Value type: {obj.GetType()}")
        };
        
        string SerializeArray<T>(IEnumerable<T> items, string? prefix = null, Func<T, string?>? serialize = null) {
            serialize ??= item => item?.ToString();
            var serialized = items
                .Select(serialize)
                .Where(val => val != null);
            
            return prefix == null ? 
                $"[{string.Join(',', serialized)}]" : 
                $"[{prefix};{string.Join(',', serialized)}]";
        }
        
        string SerializeEnum(Enum value) {
            return value.GetType().GetCustomAttribute<FlagsAttribute>() != null
                ? ((int)(object)value).ToString()
                : value.ToString().ToSnakeCase().Quote();
        }
    }
}