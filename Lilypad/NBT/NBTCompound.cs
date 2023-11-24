namespace Lilypad;

public class NBTCompound : Dictionary<string, object?>, ISerializeInner {
    public static readonly NBTCompound Empty = new();
    
    public string Serialize() {
        return NBTSerializer.Serialize(this);
    }

    public override string ToString() {
        return Serialize();
    }

    public static NBTCompound From(params (string, object?)[] pairs) {
        var compound = new NBTCompound();
        foreach (var (key, value) in pairs) {
            compound[key] = value;
        }
        return compound;
    }
    
    public static NBTCompound operator +(NBTCompound a, NBTCompound b) {
        var compound = new NBTCompound();
        foreach (var (key, value) in a) {
            compound[key] = value;
        }
        foreach (var (key, value) in b) {
            compound[key] = value;
        }
        return compound;
    }
    
    public static implicit operator NBTCompound((string, object?)[] pairs) => From(pairs);
    public static implicit operator NBTCompound((string, object?) pair) => From(pair);

    public object? SerializedData => Serialize();
}