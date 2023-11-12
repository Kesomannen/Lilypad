using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// A reference to a resource or resource tag.
/// </summary>
public readonly struct Reference<T> : ISerializeInner where T : Resource {
    /// <summary>
    /// The <see cref="Resource.Location"/> this reference points to.
    /// </summary>
    public readonly string Location;

    /// <summary>
    /// The resource at <see cref="Location"/>. Only set if the reference was created from a resource.
    /// </summary>
    [JsonIgnore] 
    public readonly T? Resource;
    
    /// <summary>
    /// The tag at <see cref="Location"/>. Only set if the reference was created from a tag.
    /// </summary>
    [JsonIgnore]
    public readonly Tag<T>? Tag;

    /// <summary>
    /// Creates a new reference to the given <paramref name="location"/>.
    /// </summary>
    /// <remarks>Makes no guarantees a resource at <paramref name="location"/> actually exists.</remarks>
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
    
    /// <summary>
    /// Creates a reference to a resource in the minecraft namespace.
    /// </summary>
    public static Reference<T> Minecraft(string name) {
        return new Reference<T>($"minecraft:{name}");
    }
    
    public static implicit operator Reference<T>(string location) => new(location);
    public static implicit operator Reference<T>(T resource) => new(resource);
    public static implicit operator Reference<T>(Tag<T> tag) => new(tag);

    public override string ToString() => Location;

    object ISerializeInner.SerializedData => Location;
}