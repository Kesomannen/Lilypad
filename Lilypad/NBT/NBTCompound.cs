using System.Collections;

namespace Lilypad.NBT;

public class NBTCompound : IEnumerable<KeyValuePair<string, NBTValue>> {
    readonly Dictionary<string, NBTValue> _pairs = new();
    
    public NBTValue this[string name] {
        get => _pairs[name];
        set => _pairs[name] = value;
    }

    public string Serialize() {
        return NBTSerializer.Serialize(this);
    }

    public override string ToString() {
        return Serialize();
    }

    public IEnumerator<KeyValuePair<string, NBTValue>> GetEnumerator() {
        return _pairs.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
    
    public static NBTCompound From(params (string, NBTValue)[] pairs) {
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
    
    public static implicit operator NBTCompound((string, NBTValue)[] pairs) => From(pairs);
    public static implicit operator NBTCompound((string, NBTValue) pair) => From(pair);
}