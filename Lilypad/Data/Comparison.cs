namespace Lilypad; 

public readonly struct Comparison {
    public readonly ComparisonType Type;

    public Comparison(string type) {
        Type = type switch {
            ">" => ComparisonType.GreaterThan,
            ">=" => ComparisonType.GreaterThanOrEqual,
            "<" => ComparisonType.LessThan,
            "<=" => ComparisonType.LessThanOrEqual,
            "==" => ComparisonType.Equal,
            _ => ((EnumReference<ComparisonType>)type).Value
        };
    }

    public Comparison(ComparisonType type) {
        Type = type;
    }

    public override string ToString() {
        return Type switch {
            ComparisonType.GreaterThan => ">",
            ComparisonType.GreaterThanOrEqual => ">=",
            ComparisonType.LessThan => "<",
            ComparisonType.LessThanOrEqual => "<=",
            ComparisonType.Equal => "=",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public static implicit operator Comparison(string value) => new(value);
    public static implicit operator Comparison(ComparisonType value) => new(value);
}

public enum ComparisonType {
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Equal
}