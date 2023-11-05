using Lilypad.Text;

namespace Lilypad.NBT;

public readonly struct NBTValue {
    public readonly object Value;
    public readonly NBTValueType Type;

    NBTValue(object value, NBTValueType type) {
        Value = value;
        Type = type;
    }
    
    public static implicit operator NBTValue(sbyte value) => new(value, NBTValueType.Byte);
    public static implicit operator NBTValue(bool value) => new(value, NBTValueType.Boolean);
    public static implicit operator NBTValue(short value) => new(value, NBTValueType.Short);
    public static implicit operator NBTValue(int value) => new(value, NBTValueType.Int);
    public static implicit operator NBTValue(long value) => new(value, NBTValueType.Long);
    public static implicit operator NBTValue(float value) => new(value, NBTValueType.Float);
    public static implicit operator NBTValue(double value) => new(value, NBTValueType.Double);
    public static implicit operator NBTValue(string value) => new(value, NBTValueType.String);
    public static implicit operator NBTValue(NBTCompound value) => new(value, NBTValueType.Compound);
    public static implicit operator NBTValue((string, NBTValue) pair) => NBTCompound.From(pair);
    public static implicit operator NBTValue((string, NBTValue)[] pairs) => NBTCompound.From(pairs);
    public static implicit operator NBTValue(NBTValue[] values) => new(values, NBTValueType.List);
    public static implicit operator NBTValue(byte[] value) => new(value, NBTValueType.ByteArray);
    public static implicit operator NBTValue(int[] value) => new(value, NBTValueType.IntArray);
    public static implicit operator NBTValue(long[] value) => new(value, NBTValueType.LongArray);
    public static implicit operator NBTValue(RichText value) => new(value, NBTValueType.Json);
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
    LongArray
}