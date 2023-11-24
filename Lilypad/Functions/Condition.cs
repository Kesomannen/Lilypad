using Lilypad.Helpers;

namespace Lilypad; 

/// <summary>
/// Represents a condition for an execute command.
/// </summary>
public struct Condition {
    /// <summary>
    /// String representation of the condition.
    /// </summary>
    public string Value;
    
    /// <summary>
    /// If true, the condition will use 'unless' instead of 'if'.
    /// </summary>
    public bool Invert;
    
    public Condition(string value, bool invert = false) {
        Value = value;
        Invert = invert;
    }
    
    /// <returns>An inverted copy of this condition</returns>
    public Condition Not() {
        return new Condition(Value, !Invert);
    }

    public override string ToString() {
        return $"{(Invert ? "unless" : "if")} {Value}";
    }

    public static implicit operator Condition(string value) => new(value);
    
    /// <summary>
    /// Checks for a specific biome in a given position. 
    /// </summary>
    public static Condition Biome(Vector3 pos, EnumReference<Biome> biome) {
        return $"biome {biome} {pos}";
    }

    /// <summary>
    /// Checks for a specific biome in the execution.
    /// </summary>
    public static Condition Biome(EnumReference<Biome> biome) {
        return Biome(Vector3.Here, biome);
    }
    
    /// <summary>
    /// Compares the block at a given position to a given block.
    /// </summary>
    public static Condition Block(Vector3 pos, EnumReference<Block> block) {
        return $"block {pos} {block}";
    }
    
    /// <summary>
    /// Checks if there is a block at the execution.
    /// </summary>
    public static Condition Block(EnumReference<Block> block) {
        return Block(Vector3.Here, block);
    }
    
    /// <summary>
    /// Compares the blocks in two equally sized volumes.
    /// </summary>
    /// <param name="start">One of any two diagonal corners of the source volume.</param>
    /// <param name="end">The other diagonal corner of the source volume.</param>
    /// <param name="destination">
    /// Position of the lower northwest corner of the destination volume.
    /// Assumed to be of the same size as the source volume.
    /// </param>
    /// <param name="comparison">Specifies the comparison mode.</param>
    public static Condition Blocks(Vector3 start, Vector3 end, Vector3 destination, EnumReference<BlocksComparison> comparison) {
        return $"blocks {start} {end} {destination} {comparison}";
    }

    /// <summary>
    /// Checks if the target data source has any data at the given path.
    /// </summary>
    public static Condition Data(DataSource source, NBTPath path) {
        return $"data {source} {path}";
    }
    
    /// <summary>
    /// Checks if the execution is in a matching dimension.
    /// </summary>
    public static Condition Dimension(EnumReference<Dimension> dimension) {
        return $"dimension {dimension}";
    }
    
    /// <summary>
    /// Checks if the executing entity matches the given selector.
    /// </summary>
    public static Condition Entity(Argument<Selector> selector) {
        return $"entity {selector}";
    }
    
    /// <summary>
    ///Checks if chunks at a given position is fully loaded (entity ticking).
    /// </summary>
    public static Condition Loaded(Vector3 pos) {
        return $"loaded {pos}";
    }
    
    /// <summary>
    /// Checks whether the predicate successes.
    /// </summary>
    public static Condition Predicate(Reference<DataResource<Predicate>> predicate) {
        return $"predicate {predicate}";
    }
    
    /// <summary>
    /// Compares scores between two tracked entities.
    /// </summary>
    /// <param name="target">The target selector.</param>
    /// <param name="score">The target score.</param>
    /// <param name="comparison">
    /// The comparison to use.
    /// Must be entered as Comparison.Equal or "Equal", rather than "==".</param>
    /// <param name="source">The other selector to compare with.</param>
    /// <param name="sourceScore">The other objective to compare with.</param>
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

    /// <summary>
    /// Compares two score variables.
    /// </summary>
    /// <seealso cref="Score(Lilypad.Argument{Lilypad.Selector},Lilypad.Reference{Lilypad.Objective},Lilypad.EnumReference{Lilypad.Comparison},Lilypad.Argument{Lilypad.Selector},Lilypad.Reference{Lilypad.Objective})"/>
    public static Condition Score(
        ScoreVariable target,
        EnumReference<Comparison> comparison,
        ScoreVariable variable
    ) {
        return Score(target.Selector, target.Objective, comparison, variable.Selector, variable.Objective);
    }

    /// <summary>
    /// Checks if a score is within a given range, inclusive.
    /// </summary>
    /// <seealso cref="Score(Lilypad.Argument{Lilypad.Selector},Lilypad.Reference{Lilypad.Objective},Lilypad.EnumReference{Lilypad.Comparison},Lilypad.Argument{Lilypad.Selector},Lilypad.Reference{Lilypad.Objective})"/>
    public static Condition Score(
        Argument<Selector> target,
        Reference<Objective> score,
        Range<int> range
    ) {
        return $"score {target} {score} matches {range}";
    }
    
    /// <summary>
    /// Checks if a score variable is within a given range, inclusive.
    /// </summary>
    /// <seealso cref="Score(Lilypad.Argument{Lilypad.Selector},Lilypad.Reference{Lilypad.Objective},Lilypad.EnumReference{Lilypad.Comparison},Lilypad.Argument{Lilypad.Selector},Lilypad.Reference{Lilypad.Objective})"/>
    public static Condition Score(ScoreVariable variable, Range<int> range) {
        return Score(variable.Selector, variable.Objective, range);
    }

    public static Condition Variable(
        Function function, 
        IVariable a, 
        EnumReference<Comparison> comparison, 
        IVariable b
    ) {
        return Score(function.ToScore(a, "#compare0"), comparison, function.ToScore(b, "#compare1"));
    }
    
    public static Condition Variable(
        Function function, 
        IVariable variable, 
        Range<int> range
    ) {
        return Score(function.ToScore(variable, "#compare0"), range);
    }
    
    /// <summary>
    /// Checks if the executing entity has a given tag.
    /// </summary>
    /// <seealso cref="Entity"/>
    public static Condition Tag(string tag) {
        return Entity(Selector.Self.Tag(tag));
    }
}

public enum BlocksComparison {
    /// <summary>
    /// Compare ALL blocks.
    /// </summary>
    All,
    
    /// <summary>
    /// Ignore air blocks.
    /// </summary>
    Masked
}

public enum Comparison {
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Equal
}