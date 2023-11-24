namespace Lilypad; 

public interface ICustomNBTSerializer {
    string? Serialize();
}

public interface ISerializeInnerNBT : ICustomNBTSerializer {
    object? SerializedData { get; }

    string? ICustomNBTSerializer.Serialize() {
        return NBTSerializer.SerializeValue(SerializedData);
    }
}