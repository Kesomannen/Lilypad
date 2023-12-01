using Lilypad.Helpers;

namespace Lilypad;

/// <summary>
/// Represents a minecraft NBT compound.
/// You can add values of any type, but types that are not supported by default will be
/// serialized as strings. You can override this behaviour by implementing
/// <see cref="ISerializeInnerNBT"/> or <see cref="ISerializeInner"/>. The default
/// minecraft types (the ones supported by Lilypad) can be found at https://minecraft.wiki/w/NBT_format#Data_types.
/// </summary>
public class NBT : Dictionary<string, object?>, ISerializeInnerJson {
    public static readonly NBT Empty = new();
    
    public string Serialize() {
        return NBTSerializer.Serialize(this);
    }

    public override string ToString() {
        return Serialize();
    }
    
    /// <summary>
    /// Adds all of the keys and values from <paramref name="other"/> to this compound.
    /// If they have duplicate keys, the value from <paramref name="other"/> is kept.
    /// </summary>
    public NBT Add(NBT? other) {
        if (other == null) return this;
        
        foreach (var (key, value) in other) {
            this[key] = value;
        }
        return this;
    }

    /// <summary>
    /// Creates a new NBT compound with the specified keys and values.
    /// Null values are ignored when this compound is serialized, but null keys are not allowed.
    /// </summary>
    public static NBT From(params (string, object?)[] pairs) {
        var compound = new NBT();
        foreach (var (key, value) in pairs) {
            Assert.NotNull(key, nameof(key));
            compound[key] = value;
        }
        return compound;
    }
    
    /// <summary>
    /// Creates a new NBT compound with all of the values from <paramref name="a"/> and <paramref name="b"/>.
    /// If they have duplicate keys, the value from <paramref name="b"/> is kept.
    /// </summary>
    public static NBT Combine(NBT a, NBT b) {
        Assert.NotNull(a, nameof(a));
        Assert.NotNull(b, nameof(b));
        
        var compound = new NBT();
        foreach (var (key, value) in a) {
            compound[key] = value;
        }
        foreach (var (key, value) in b) {
            compound[key] = value;
        }
        return compound;
    }
    
    /// <inheritdoc cref="Combine"/>
    public static NBT operator +(NBT a, NBT b) => Combine(a, b);
    
    /// <inheritdoc cref="From"/>
    public static implicit operator NBT((string, object?)[] pairs) => From(pairs);
    
    /// <inheritdoc cref="From"/>
    public static implicit operator NBT((string, object?) pair) => From(pair);

    object ISerializeInnerJson.SerializedData => Serialize();
}