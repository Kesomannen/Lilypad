using Lilypad.Helpers;

namespace Lilypad; 

public struct Condition {
    public string Value;
    public bool Invert;
    
    public Condition(string value, bool invert = false) {
        Value = value;
        Invert = invert;
    }
    
    public Condition Not() {
        return new Condition(Value, !Invert);
    }

    public override string ToString() {
        return $"{(Invert ? "unless" : "if")} {Value}";
    }

    public static implicit operator Condition(string value) => new(value);
    
    public static Condition Biome(Vector3 pos, EnumReference<Biome> biome) {
        return $"biome {biome} {pos}";
    }

    public static Condition Biome(EnumReference<Biome> biome) {
        return Biome(Vector3.Here, biome);
    }
    
    public static Condition Block(Vector3 pos, EnumReference<Block> block) {
        return $"block {pos} {block}";
    }
    
    public static Condition Block(EnumReference<Block> block) {
        return Block(Vector3.Here, block);
    }
    
    public static Condition Blocks(Vector3 start, Vector3 end, Vector3 destination, EnumReference<BlocksComparison> comparison) {
        return $"blocks {start} {end} {destination} {comparison}";
    }
    
    public static Condition Data(DataSource source, NBTPath path) {
        return $"data {source} {path}";
    }
    
    public static Condition Dimension(EnumReference<Dimension> dimension) {
        return $"dimension {dimension}";
    }
    
    public static Condition Entity(Argument<Selector> selector) {
        return $"entity {selector}";
    }
    
    public static Condition Loaded(Vector3 pos) {
        return $"loaded {pos}";
    }
    
    public static Condition Predicate(Reference<DataResource<Predicate>> predicate) {
        return $"predicate {predicate}";
    }
    
    public static Condition Score(
        Argument<Selector> target, 
        Reference<Objective> score, 
        EnumReference<Comparison> comparison,
        Argument<Selector> source, 
        Reference<Objective> sourceScore
    ) {
        var op = comparison.Value switch {
            Comparison.GreaterThan => ">",
            Comparison.GreaterThanOrEqual => ">=",
            Comparison.LessThan => "<",
            Comparison.LessThanOrEqual => "<=",
            Comparison.Equal => "=",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        return $"score {target} {score} {op} {source} {sourceScore}";
    }

    public static Condition Score(
        ScoreVariable target,
        EnumReference<Comparison> comparison,
        ScoreVariable variable
    ) {
        return Score(target.Selector, target.Objective, comparison, variable.Selector, variable.Objective);
    }

    public static Condition Score(
        Argument<Selector> target,
        Reference<Objective> score,
        Range<int> range
    ) {
        return $"score {target} {score} matches {range}";
    }
    
    public static Condition Score(ScoreVariable variable, Range<int> range) {
        return Score(variable.Selector, variable.Objective, range);
    }
    
    public static Condition Tag(string tag) {
        return Entity(Selector.Self.Tag(tag));
    }
}

public enum BlocksComparison {
    All,
    Masked
}

public enum Comparison {
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Equal
}