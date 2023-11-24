namespace Lilypad;

public class NBT : Dictionary<string, object?>, ISerializeInnerJson {
    public static readonly NBT Empty = new();
    
    public string Serialize() {
        return NBTSerializer.Serialize(this);
    }

    public override string ToString() {
        return Serialize();
    }
    
    public NBT Add(NBT? other) {
        if (other == null) return this;
        
        foreach (var (key, value) in other) {
            this[key] = value;
        }
        return this;
    }

    public static NBT From(params (string, object?)[] pairs) {
        var compound = new NBT();
        foreach (var (key, value) in pairs) {
            compound[key] = value;
        }
        return compound;
    }
    
    public static NBT Combine(NBT a, NBT b) {
        var compound = new NBT();
        foreach (var (key, value) in a) {
            compound[key] = value;
        }
        foreach (var (key, value) in b) {
            compound[key] = value;
        }
        return compound;
    }
    
    public static NBT operator +(NBT a, NBT b) => Combine(a, b);
    
    public static implicit operator NBT((string, object?)[] pairs) => From(pairs);
    public static implicit operator NBT((string, object?) pair) => From(pair);

    public object? SerializedData => Serialize();
}