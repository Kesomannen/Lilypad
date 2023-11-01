using Lilypad.Predicates;

namespace Lilypad; 

public class Selector {
    readonly char _name;
    
    readonly Dictionary<string, string> _arguments = new();

    public Selector(char name) { _name = name; }
    
    public static Selector All => new('a');
    public static Selector Entites => new('e');
    public static Selector Nearest => new('p');
    public static Selector Random => new('r');
    public static Selector Self => new('s');
    
    public Selector Gamemode(EnumReference<Gamemode> gamemode) {
        return AddJson("gamemode", gamemode);
    }
    
    public Selector Predicate(Reference<DataResource<Predicate>> predicate) {
        return Add("predicate", predicate);
    }

    Selector AddJson(string key, object value) {
        return Add(key, Json.Serialize(value));
    }
    
    Selector Add(string key, object value) {
        return Add(key, value.ToString()!);
    }
    
    Selector Add(string key, string value) {
        _arguments.Add(key, value);
        return this;
    }
    
    public override string ToString() {
        var arguments = string.Join(",", _arguments.Select(x => $"{x.Key}={x.Value}"));
        return $"@{_name}[{arguments}]";
    }
}