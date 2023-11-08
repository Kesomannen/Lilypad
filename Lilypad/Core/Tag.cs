using Newtonsoft.Json;

namespace Lilypad; 

public class Tag<T> : Resource {
    [JsonProperty] 
    List<Value> Values { get; } = new();
    
    public bool? Replace { get; set; }
    
    internal Tag(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) { }
    
    public void Add(string resourceLocation, bool required = true) {
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
    public static void Add<T>(this Tag<T> tag, Reference<T> resource, bool required = true) where T : Resource {
        tag.Add(resource.Location, required);
    }
    
    public static void Add<T>(this Tag<T> tag, EnumReference<T> enumReference, bool required = true) where T : struct, Enum {
        tag.Add(enumReference.ToString(), required);
    }
}