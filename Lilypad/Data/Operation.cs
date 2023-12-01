namespace Lilypad; 

public readonly struct Operation {
    public readonly OperationType Type;

    public Operation(string type) {
        Type = type switch {
            "+=" => OperationType.Add,
            "-=" => OperationType.Subtract,
            "*=" => OperationType.Multiply,
            "/=" => OperationType.Divide,
            "%=" => OperationType.Modulo,
            "=" => OperationType.Assign,
            "<" => OperationType.Min,
            ">" => OperationType.Max,
            "><" => OperationType.Swap,
            _ => ((EnumReference<OperationType>)type).Value
        };
    }

    public Operation(OperationType type) {
        Type = type;
    }

    public override string ToString() {
        return Type switch {
            OperationType.Add => "+=",
            OperationType.Subtract => "-=",
            OperationType.Multiply => "*=",
            OperationType.Divide => "/=",
            OperationType.Modulo => "%=",
            OperationType.Assign => "=",
            OperationType.Min => "<",
            OperationType.Max => ">",
            OperationType.Swap => "><",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public static implicit operator Operation(string value) => new(value);
    public static implicit operator Operation(OperationType value) => new(value);
}

public enum OperationType {
    Add,
    Subtract,
    Multiply,
    Divide,
    Modulo,
    Assign,
    Min,
    Max,
    Swap
}