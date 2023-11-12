using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// Represents a user-defined tag in a datapack.
/// </summary>
public class Tag<T> : Resource {
    [JsonProperty] 
    List<Value> Values { get; } = new();

    /// <summary>
    /// Defaults to <c>false</c>. Whether or not the contents of this tag should completely replace tag contents from different lower priority data packs with the same resource location.
    /// <br/>When <c>false</c> the tag's content is appended to the contents of the higher priority data packs, instead.
    /// </summary>
    public bool Replace { get; set; } = false;
    
    internal Tag(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) { }
    
    internal void Add(string resourceLocation, bool required = true) {
        if (Values.Any(x => x.Location == resourceLocation)) {
            return;
        }
        
        Values.Add(new Value {
            Location = resourceLocation,
            Required = required
        });
    }

    protected override string GetLocation() {
        return $"#{Namespace}:{Name}";
    }
    
    public struct Value {
        [JsonProperty("id")]
        public string Location;
        public bool Required;
    }
}

public static class TagExtensions {
    /// <summary>
    /// Adds a resource entry to this tag.
    /// </summary>
    /// <param name="resource">The resource to add. Can also be a tag with the same type.</param>
    /// <param name="required">Whether or not loading this tag should fail if this entry is not found. A tag that fails to load can still be referenced in any data pack and be (re)defined in other data packs.</param>
    public static void Add<T>(this Tag<T> tag, Reference<T> resource, bool required = true) where T : Resource {
        tag.Add(resource.Location, required);
    }
    
    /// <summary>
    /// Adds an enum entry to this tag. 
    /// </summary>
    /// <param name="required">Whether or not loading this tag should fail if this entry is not found. A tag that fails to load can still be referenced in any data pack and be (re)defined in other data packs.</param>
    /// <remarks>Used to add vanilla resources to tags, such as items or blocks.</remarks>
    public static void Add<T>(this Tag<T> tag, EnumReference<T> enumReference, bool required = true) where T : struct, Enum {
        tag.Add(enumReference.ToString(), required);
    }
}