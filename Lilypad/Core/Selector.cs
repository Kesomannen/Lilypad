using System.Text;
using Lilypad.Predicates;
using Lilypad.Scoreboards;

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
    
    static readonly StringBuilder _builder = new();
    
    public Selector Gamemode(EnumReference<Gamemode> gamemode) {
        return AddJson("gamemode", gamemode);
    }
    
    public Selector Predicate(Reference<DataResource<Predicate>> predicate) {
        return Add("predicate", predicate);
    }
    
    public Selector Scores(params (Argument<Objective> Objective, Argument<Range<int>> Range)[] scores) {
        _builder.Clear();
        _builder.Append('{');
        
        for (var i = 0; i < scores.Length; i++) {
            var (objective, range) = scores[i];
            _builder.Append($"{objective}={range}");
            
            if (i < scores.Length - 1) {
                _builder.Append(',');
            }
        }
        
        _builder.Append('}');
        return Add("scores", _builder.ToString());
    }
    
    public Selector Tag(string tag) {
        return Add("tag", tag);
    }
    
    public Selector Type(EnumReference<Entity> type) {
        return AddJson("type", type);
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
        _builder.Clear();
        _builder.Append($"@{_name}");

        if (_arguments.Count <= 0) return _builder.ToString();
        
        _builder.Append('[');
            
        var first = true;
        foreach (var (key, value) in _arguments) {
            if (!first) {
                _builder.Append(',');
            }
            _builder.Append($"{key}={value}");
            first = false;
        }
            
        _builder.Append(']');

        return _builder.ToString();
    }
}