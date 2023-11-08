using Lilypad.Text;
using Newtonsoft.Json;

namespace Lilypad;

public readonly struct NBTValue {
    public readonly object Value;
    public readonly NBTValueType Type;

    public NBTValue(object value, NBTValueType type) {
        Value = value;
        Type = type;
    }

    public static NBTValue FromObject(object obj) {
        return obj switch {
            sbyte => new NBTValue(obj, NBTValueType.Byte),
            bool => new NBTValue(obj, NBTValueType.Boolean),
            short => new NBTValue(obj, NBTValueType.Short),
            int => new NBTValue(obj, NBTValueType.Int),
            long => new NBTValue(obj, NBTValueType.Long),
            float => new NBTValue(obj, NBTValueType.Float),
            double => new NBTValue(obj, NBTValueType.Double),
            string => new NBTValue(obj, NBTValueType.String),
            RichText => new NBTValue(obj, NBTValueType.Json),
            NBTCompound => new NBTValue(obj, NBTValueType.Compound),
            Enum => new NBTValue((int) obj, NBTValueType.Int),
            IEnumerable<byte> => new NBTValue(obj, NBTValueType.ByteArray),
            IEnumerable<int> => new NBTValue(obj, NBTValueType.IntArray),
            IEnumerable<long> => new NBTValue(obj, NBTValueType.LongArray),
            IEnumerable<NBTValue> => new NBTValue(obj, NBTValueType.List),
            IEnumerable<object> => new NBTValue(obj, NBTValueType.List),
            _ => new NBTValue(obj, NBTValueType.Object)
        };
    }
    
    public static NBTValue FromObjects(params object[] objs) {
        return new NBTValue(objs.Select(FromObject), NBTValueType.List);
    }
    
    public static implicit operator NBTValue(sbyte value) => FromObject(value);
    public static implicit operator NBTValue(bool value) => FromObject(value);
    public static implicit operator NBTValue(short value) => FromObject(value);
    public static implicit operator NBTValue(int value) => FromObject(value);
    public static implicit operator NBTValue(long value) => FromObject(value);
    public static implicit operator NBTValue(float value) => FromObject(value);
    public static implicit operator NBTValue(double value) => FromObject(value);
    public static implicit operator NBTValue(string value) => FromObject(value);
    public static implicit operator NBTValue(RichText value) => FromObject(value);
    public static implicit operator NBTValue(NBTCompound value) => FromObject(value);
    public static implicit operator NBTValue(byte[] value) => FromObject(value);
    public static implicit operator NBTValue(int[] value) => FromObject(value);
    public static implicit operator NBTValue(long[] value) => FromObject(value);
    public static implicit operator NBTValue(NBTValue[] value) => FromObject(value);
    public static implicit operator NBTValue(object[] value) => FromObjects(value);
    public static implicit operator NBTValue(Enum value) => FromObject(value);
}

public enum NBTValueType {
    Byte,
    Boolean,
    Short,
    Int,
    Long,
    Float,
    Double,
    String,
    Json,
    List,
    Compound,
    ByteArray,
    IntArray,
    LongArray,
    Object
}