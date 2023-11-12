using System.Text;
using Lilypad.Extensions;
using Lilypad.Text;

namespace Lilypad; 

public static class NBTSerializer {
    public static string Serialize(NBTCompound compound) {
        var builder = new StringBuilder();
        builder.Append('{');
        
        var first = true;
        foreach (var (name, tag) in compound) {
            if (!first) {
                builder.Append(',');
            }
            first = false;
            
            builder.Append(name);
            builder.Append(':');
            builder.Append(SerializeValue(tag));
        }
        
        builder.Append('}');
        return builder.ToString();
    }

    static string SerializeValue(NBTValue value) {
        var str = value.Value.ToString();
        switch (value.Type) {
            case NBTValueType.Byte:
                AssertType<sbyte>();
                return str! + 'b';
            
            case NBTValueType.Boolean:
                AssertType<bool>();
                return str!.ToLower();
            
            case NBTValueType.Short:
                AssertType<short>();
                return str! + 's';
            
            case NBTValueType.Int:
                AssertType<int>();
                return str!;
            
            case NBTValueType.Long:
                AssertType<long>();
                return str! + 'l';
            
            case NBTValueType.Float:
                AssertType<float>();
                return str! + 'f';
            
            case NBTValueType.Double:
                AssertType<double>();
                return str! + 'd';
            
            case NBTValueType.String:
                AssertType<string>();
                return SerializeString();
            
            case NBTValueType.List:
                var enumerable = AssertType<IEnumerable<NBTValue>>();
                return SerializeArray(enumerable, SerializeValue);
                
            case NBTValueType.Compound:
                var compound = AssertType<NBTCompound>();
                return Serialize(compound);
            
            case NBTValueType.ByteArray:
                var byteArray = AssertType<IEnumerable<sbyte>>();
                return SerializeArray(byteArray, prefix: "B");
                
            case NBTValueType.IntArray:
                var intArray = AssertType<IEnumerable<int>>();
                return SerializeArray(intArray, prefix: "I");
                
            case NBTValueType.LongArray:
                var longArray = AssertType<IEnumerable<long>>();
                return SerializeArray(longArray, prefix: "L");

            case NBTValueType.Json:
                var richText = AssertType<JsonText>();
                return $"'{richText.ToString().Escape('\'')}'";

            case NBTValueType.Object:
                return SerializeString();
                    
            default: throw new ArgumentOutOfRangeException();
        }
        
        T AssertType<T>() {
            var expectedType = typeof(T);
            var type = value.Value.GetType();
            if (!expectedType.IsAssignableFrom(type)) {
                throw new ArgumentException($"Expected {expectedType} but got {type}");
            }
            return (T) value.Value;
        }
        
        string SerializeArray<T>(IEnumerable<T> items, Func<T, string>? serialize = null, string? prefix = null) {
            serialize ??= item => item.ToString();
            var serialized = items.Select(serialize);
            return prefix == null ? 
                $"[{string.Join(',', serialized)}]" : 
                $"[{prefix};{string.Join(',', serialized)}]";
        }

        string SerializeString() => $"\"{str!.Escape('"')}\"";
    }
}