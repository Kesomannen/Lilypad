using Newtonsoft.Json;

namespace Lilypad; 

public readonly struct Reference<T> : ISerializeInner where T : Resource {
    public readonly string Location;

    [JsonIgnore] 
    public readonly T? Resource;
    
    [JsonIgnore]
    public readonly Tag<T>? Tag;

    public Reference(string location) {
        Location = location;
    }

    public Reference(T resource) {
        Location = resource.Location;
        Resource = resource;
    }
    
    public Reference(Tag<T> tag) {
        Location = tag.Location;
        Tag = tag;
    }
    
    public static Reference<T> Minecraft(string name) {
        return new Reference<T>($"minecraft:{name}");
    }
    
    public static implicit operator Reference<T>(string location) {
        return new Reference<T>(location);
    }

    public static implicit operator Reference<T>(T resource) {
        return new Reference<T>(resource);
    }

    public static implicit operator Reference<T>(Tag<T> tag) {
        return new Reference<T>(tag);
    }

    public override string ToString() => Location;

    object ISerializeInner.SerializedData => Location;
}