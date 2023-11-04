using Newtonsoft.Json;

namespace Lilypad; 

public class Tag<T> : Resource {
    [JsonProperty] 
    List<TagValue> Values { get; } = new();
    
    public bool? Replace { get; set; }

    protected override string Prefix => "#";
    
    public void Add(string resourceLocation, bool required = true) {
        if (Values.Any(x => x.Location == resourceLocation)) {
            return;
        }
        
        Values.Add(new TagValue {
            Location = resourceLocation,
            Required = required
        });
    }
    
    public Tag(string name, Datapack datapack) : base(name, datapack) { }
}

public static class TagExtensions {
    public static void Add<T>(this Tag<T> tag, Reference<T> resource, bool required = true) where T : Resource {
        tag.Add(resource.Location, required);
    }
    
    public static void Add<T>(this Tag<T> tag, EnumReference<T> enumReference, bool required = true) where T : struct, Enum {
        tag.Add(enumReference.ToString(), required);
    }
}