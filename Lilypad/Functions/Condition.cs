using Lilypad.Helpers;
using Lilypad;

namespace Lilypad; 

public static class Condition {
    public static string Biome(Vector3 pos, EnumReference<Biome> biome) {
        return $"biome {biome} {pos}";
    }

    public static string Biome(EnumReference<Biome> biome) {
        return Biome(Vector3.Here, biome);
    }
    
    public static string Block(Vector3 pos, EnumReference<Block> block) {
        return $"block {pos} {block}";
    }
    
    public static string Block(EnumReference<Block> block) {
        return Block(Vector3.Here, block);
    }
    
    public static string Blocks(Vector3 start, Vector3 end, Vector3 destination, EnumReference<BlocksComparison> comparison) {
        return $"blocks {start} {end} {destination} {comparison}";
    }
    
    public static string Data(DataSource source, NBTPath path) {
        return $"data {source} {path}";
    }
    
    public static string Dimension(EnumReference<Dimension> dimension) {
        return $"dimension {dimension}";
    }
    
    public static string Entity(Argument<Selector> selector) {
        return $"entity {selector}";
    }
    
    public static string Loaded(Vector3 pos) {
        return $"loaded {pos}";
    }
    
    public static string Predicate(Reference<DataResource<Predicate>> predicate) {
        return $"predicate {predicate}";
    }
    
    public static string Score(
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

    public static string Score(
        ScoreVariable target,
        EnumReference<Comparison> comparison,
        ScoreVariable variable
    ) {
        return Score(target.Selector, target.Objective, comparison, variable.Selector, variable.Objective);
    }

    public static string Score(
        Argument<Selector> target,
        Objective score,
        EnumReference<Comparison> comparison,
        int value
    ) {
        return Score(new ScoreVariable(target, score), comparison, Constants.Get(score.Datapack, value));
    }

    public static string Tag(string tag) {
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