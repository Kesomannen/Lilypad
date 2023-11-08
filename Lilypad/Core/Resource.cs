using Newtonsoft.Json;

namespace Lilypad; 

public abstract class Resource {
    [JsonIgnore]
    public string Name { get; }
    
    [JsonIgnore]
    public string Namespace { get; }

    [JsonIgnore]
    public string Location => GetLocation();
    
    [JsonIgnore]
    public Datapack Datapack { get; }

    protected Resource(string name, string @namespace, Datapack datapack) {
        Name = name;
        Namespace = @namespace;
        Datapack = datapack;
    }

    public override string ToString() => Location;

    protected virtual string GetLocation() {
        return $"{Namespace}:{Name}";
    }
}