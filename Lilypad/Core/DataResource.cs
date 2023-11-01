namespace Lilypad; 

public class DataResource<T> : Resource, ISerializeInner {
    public T? Data { get; set; }

    public DataResource(string name, Datapack datapack) : base(name, datapack) { }
    
    object ISerializeInner.SerializedData => Data;
}