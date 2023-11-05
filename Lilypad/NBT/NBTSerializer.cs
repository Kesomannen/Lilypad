using System.Text;
using Lilypad.Text;

namespace Lilypad.NBT; 

public static class NBTSerializer {
    public static string Serialize(NBTCompound compound) {
        var builder = new StringBuilder();
        builder.Append('{');
        
        var first = true;
        foreach (var (name, tag) in compound) {
            if (!first) {
                builder.Append(',');
                first = false;
            }
            
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
                return str!;
            
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
                return $"\"{str}\"";
            
            case NBTValueType.List:
                var enumerable = AssertType<IEnumerable<NBTValue>>(true);
                return SerializeArray(enumerable);
                
            case NBTValueType.Compound:
                var compound = AssertType<NBTCompound>();
                return Serialize(compound);
            
            case NBTValueType.ByteArray:
                var byteArray = AssertType<IEnumerable<sbyte>>();
                return SerializeArray(byteArray, "B");
                
            case NBTValueType.IntArray:
                var intArray = AssertType<IEnumerable<int>>();
                return SerializeArray(intArray, "I");
                
            case NBTValueType.LongArray:
                var longArray = AssertType<IEnumerable<long>>();
                return SerializeArray(longArray, "L");

            case NBTValueType.Json:
                var richText = AssertType<RichText>();
                return $"'{richText}'";
            
            default: throw new ArgumentOutOfRangeException();
        }
        
        T AssertType<T>(bool derived = false) {
            var expectedType = typeof(T);
            var type = value.Value.GetType();
            if (derived ? !expectedType.IsAssignableFrom(type) : type != expectedType) {
                throw new ArgumentException($"Expected {expectedType} but got {type}");
            }
            return (T) value.Value;
        }
        
        string SerializeArray<T>(IEnumerable<T> items, string? prefix = null) {
            return prefix == null ? 
                $"[{string.Join(',', items)}]" : 
                $"[{prefix};{string.Join(',', items)}]";
        }
    }
}