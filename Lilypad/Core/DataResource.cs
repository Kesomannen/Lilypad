namespace Lilypad; 

public class DataResource<T> : Resource, ISerializeInner {
    public T? Data { get; set; }
    
    object ISerializeInner.SerializedData => Data;

    internal DataResource(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) { }
}