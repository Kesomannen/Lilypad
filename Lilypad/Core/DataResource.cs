namespace Lilypad; 

/// <summary>
/// Represents a resource holding an inner data type.
/// </summary>
public class DataResource<T> : Resource, ISerializeInner {
    /// <summary>
    /// The inner data held by this resource.
    /// </summary>
    public T? Data { get; set; }
    
    object ISerializeInner.SerializedData => Data;

    internal DataResource(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) { }
}