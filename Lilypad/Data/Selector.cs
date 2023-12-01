using System.Text;
using Lilypad.Extensions;
using Lilypad.Helpers;

namespace Lilypad; 

/// <summary>
/// Represents a target selector.
/// </summary>
public class Selector {
    readonly char _name;
    
    readonly List<(string, string)> _arguments = new();

    public Selector(char name) { _name = name; }
    
    /// <summary>
    /// Selects every player, alive or not.
    /// </summary>
    public static Selector All => new('a');
    
    /// <summary>
    /// Selects all alive entities (including players) in loaded chunks.
    /// </summary>
    public static Selector Entites => new('e');
    
    /// <summary>
    /// Selects the nearest player from the command's execution.
    /// </summary>
    /// <remarks>If there are multiple nearest players, caused by them being precisely the same distance away, the player who most recently joined the server is selected.</remarks>
    public static Selector Nearest => new('p');
    
    /// <summary>
    /// Selects a random player.
    /// </summary>
    public static Selector Random => new('r');
    
    /// <summary>
    /// Selects the entity (alive or not) that executed the command.
    /// </summary>
    /// <remarks>It does not select anything if the command was run by a command block or server console.</remarks>
    public static Selector Self => new('s');
    
    public static Selector Predicates(params PredicateResource[] predicates) {
        return predicates.Aggregate(Entites, (current, predicate) => current.Predicate(predicate));
    }
    
    public static Selector Predicates(Resource resource, params Predicate[] predicates) {
        return Predicates(predicates.Select(p => resource.Datapack.Predicates.Add(p)).ToArray());
    }
    
    static readonly StringBuilder _builder = new();
    
    public Selector Gamemode(EnumReference<Gamemode> gamemode) {
        return Add("gamemode", gamemode);
    }
    
    public Selector NotGamemode(EnumReference<Gamemode> gamemode) {
        return Add("gamemode", $"!{gamemode}", true);
    }
    
    public Selector Predicate(Reference<PredicateResource> predicate) {
        return Add("predicate", predicate, true);
    }
    
    public Selector NotPredicate(Reference<PredicateResource> predicate) {
        return Add("predicate", $"!{predicate}", true);
    }
    
    public Selector Scores(params (Reference<Objective>, IntRange)[] scores) {
        return AddCompound("scores", scores);
    }
    
    public Selector Tag(string tag) {
        return Add("tag", tag, true);
    }
    
    public Selector NotTag(string tag) {
        return Add("tag", $"!{tag}", true);
    }

    public Selector AnyTag() {
        return Add("tag", "!", true);
    }
    
    public Selector NoTag() {
        return Add("tag", string.Empty, true);
    }
    
    public Selector Team(string team) {
        return Add("team", team);
    }
    
    public Selector NotTeam(string team) {
        return Add("team", $"!{team}", true);
    }
    
    public Selector AnyTeam() {
        return Add("team", "!", true);
    }
    
    public Selector NoTeam() {
        return Add("team", string.Empty);
    }
    
    public Selector Type(EnumReference<Entity> type) {
        return Add("type", type);
    }
    
    public Selector NotType(EnumReference<Entity> type) {
        return Add("type", $"!{type}", true);
    }
    
    public Selector Limit(int limit) {
        return Add("limit", limit);
    }
    
    public Selector Single() => Limit(1);
    
    public Selector Sort(EnumReference<Sort> sort) {
        return Add("sort", sort);
    }

    public Selector Nbt(NBT nbt) {
        return Add("nbt", nbt, true);
    }
    
    public Selector NotNbt(NBT nbt) {
        return Add("nbt", $"!{nbt}", true);
    }
    
    public Selector Position(Vector3 position) {
        return AddVector(position);
    }
    
    public Selector Distance(DoubleRange distance) {
        return Add("distance", distance);
    }

    public Selector Volume(Vector3 center, Vector3 size) {
        return Between(center - size / 2, center + size / 2);
    }

    public Selector Between(Vector3Range range) {
        Assert.IsTrue(range is { Min: { }, Max: { } }, "Range must have both min and max values.");
        return Between(range.Min!.Value, range.Max!.Value);
    }

    public Selector Between(Vector3 min, Vector3 max) {
        return Position(min).Volume(max - min);
    }

    public Selector Volume(Vector3 delta) {
        return AddVector(delta, "d");
    }
    
    public Selector Pitch(DoubleRange pitch) {
        return Add("x_rotation", pitch);
    }
    
    public Selector Yaw(DoubleRange yaw) {
        return Add("y_rotation", yaw);
    }

    public Selector Rotation(Vector2Range rotation) {
        
        return Pitch((rotation.Min?.X.Value, rotation.Max?.X.Value)).Yaw((rotation.Min?.Y.Value, rotation.Max?.Y.Value));
    }
    
    public Selector Level(IntRange level) {
        return Add("level", level);
    }
    
    public Selector Name(string name) {
        return Add("name", name.Quote());
    }
    
    public Selector NotName(string name) {
        return Add("name", $"!{name.Quote()}", true);
    }

    public Selector Advancements(params (Reference<Advancement>, bool)[] advancements) {
        return AddCompound("advancements", advancements, converter2: boolean => boolean.ToString().ToLower());
    }

    Selector AddVector(Vector3 vector, string? prefix = null, bool allowDuplicates = false) {
        prefix ??= string.Empty;
        return AddVectorComponent($"{prefix}x", vector.X, allowDuplicates)
            .AddVectorComponent($"{prefix}y", vector.Y, allowDuplicates)
            .AddVectorComponent($"{prefix}z", vector.Z, allowDuplicates);
    }
    
    Selector AddVectorComponent(string key, VectorComponent component, bool allowDuplicates = false) {
        Assert.IsTrue(component.Space == Space.World, "Position in selector must be in world space.");
        return Add(key, component, allowDuplicates);
    }
    
    Selector AddJson(string key, object value, bool allowDuplicates = false) {
        return Add(key, Json.Serialize(value), allowDuplicates);
    }
    
    Selector Add(string key, object value, bool allowDuplicates = false) {
        return Add(key, value.ToString()!, allowDuplicates);
    }
    
    Selector Add(string key, string value, bool allowDuplicates = false) {
        if (!allowDuplicates) {
            foreach (var (k, _) in _arguments) {
                if (k == key) {
                    throw new ArgumentException($"Selector key '{key}' doesn't allow duplicates, but one was added.");
                }
            }
        }
        _arguments.Add((key, value));
        return this;
    }

    Selector AddCompound<T1, T2>(
        string key,
        IReadOnlyList<(T1, T2)> items,
        Func<T1, string>? converter1 = null,
        Func<T2, string>? converter2 = null,
        bool allowDuplicates = false
    ) {
        _builder.Clear();
        _builder.Append('{');
        
        converter1 ??= item => item!.ToString()!;
        converter2 ??= item => item!.ToString()!;
        
        for (var i = 0; i < items.Count; i++) {
            var item = items[i];
            _builder.Append($"{converter1(item.Item1)}={converter2(item.Item2)}");
            
            if (i < items.Count - 1) {
                _builder.Append(',');
            }
        }
        
        _builder.Append('}');
        return Add(key, _builder.ToString(), allowDuplicates);
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

public enum Sort {
    Arbitrary,
    Furthest,
    Nearest,
    Random
}