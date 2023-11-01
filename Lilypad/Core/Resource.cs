using Newtonsoft.Json;

namespace Lilypad; 

public class Resource {
    bool _accessed;
    
    string _name;
    string _namespace = Namespaces.Default;
    
    [JsonIgnore]
    public string Name {
        get => _name;
        set => _name = CheckAccessed(value);
    }
    
    [JsonIgnore]
    public string Namespace {
        get => _namespace;
        set => _namespace = CheckAccessed(value);
    }

    [JsonIgnore]
    public string Location {
        get {
            _accessed = true;
            return $"{Prefix}{Namespace}:{Name}";
        }
    }
    
    [JsonIgnore]
    public Datapack Datapack { get; }
    
    protected virtual string Prefix => string.Empty;

    public Resource(string name, Datapack datapack) {
        _name = name;
        Datapack = datapack;
    }

    public override string ToString() => Location;

    T CheckAccessed<T>(T value) {
        if (_accessed) {
            throw new InvalidOperationException("Cannot change namespace after resource has been accessed.");
        }
        return value;
    }
}