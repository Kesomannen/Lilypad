namespace Lilypad; 

public class DataResource<T> : Resource, ISerializeInner {
    public T? Data { get; set; }

    internal DataResource(string name, Datapack datapack) : base(name, datapack) { }
    
    object ISerializeInner.SerializedData => Data;
}