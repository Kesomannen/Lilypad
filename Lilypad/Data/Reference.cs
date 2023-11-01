using Newtonsoft.Json;

namespace Lilypad; 

public readonly struct Reference<T> : ISerializeInner where T : Resource {
    public string Location { get; }
    
    public Reference(string location) {
        Location = location;
    }
    
    public static implicit operator Reference<T>(string location) {
        return new Reference<T>(location);
    }

    public static implicit operator Reference<T>(T resource) {
        return new Reference<T>(resource.Location);
    }

    public static implicit operator Reference<T>(Tag<T> tag) {
        return new Reference<T>(tag.Location);
    }

    public override string ToString() => Location;

    object ISerializeInner.SerializedData => Location;
}