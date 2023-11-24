using Lilypad.Helpers;
using Lilypad.Text;

namespace Lilypad;

public class ObjectiveCommand {
    readonly Reference<Objective> _objective;
    readonly Function _function;
    
    public ObjectiveCommand(Function function, Reference<Objective> objective) {
        _objective = objective;
        _function = function;
    }
    
    ObjectiveCommand AddCommand(string command) {
        _function.Add($"scoreboard {command}");
        return this;
    }
    
    public ObjectiveCommand SetDisplay(DisplaySlotArgument slot) {
        return AddCommand($"objectives setdisplay {slot} {_objective}");
    }
    
    public ObjectiveCommand SetDisplayName(JsonText displayName) {
        return AddCommand($"objectives modify {_objective} displayname {displayName}");
    }
    
    public ObjectiveCommand SetRenderType(EnumReference<ObjectiveRenderType> renderType) {
        return AddCommand($"objectives modify {_objective} rendertype {renderType}");
    }

    public ObjectiveCommand Get(Argument<Selector> target) {
        return AddCommand($"players get {target} {_objective}");
    }
    
    public ObjectiveCommand Set(Argument<Selector> target, int score) {
        return AddCommand($"players set {target} {_objective} {score}");
    }
    
    public ObjectiveCommand Add(Argument<Selector> target, int score) {
        return AddCommand($"players add {target} {_objective} {score}");
    }
    
    public ObjectiveCommand Remove(Argument<Selector> target, int score) {
        return AddCommand($"players remove {target} {_objective} {score}");
    }

    public ObjectiveCommand Reset(Argument<Selector> target) {
        return AddCommand($"players reset {target} {_objective}");
    }

    public ObjectiveCommand Enable(Argument<Selector> target) {
        return AddCommand($"players enable {target} {_objective}");
    }
    
    public ObjectiveCommand Operation(
        Argument<Selector> target, 
        EnumReference<Operation> operation, 
        Argument<Selector> source, 
        Reference<Objective>? sourceObjective = null
    ) {
        sourceObjective ??= _objective;
        var op = operation.Value switch {
            Lilypad.Operation.Add => "+=",
            Lilypad.Operation.Subtract => "-=",
            Lilypad.Operation.Multiply => "*=",
            Lilypad.Operation.Divide => "/=",
            Lilypad.Operation.Modulo => "%=",
            Lilypad.Operation.Assign => "=",
            Lilypad.Operation.Min => "<",
            Lilypad.Operation.Max => ">",
            Lilypad.Operation.Swap => "><",
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
        };
        
        return AddCommand($"players operation {target} {_objective} {op} {source} {sourceObjective}");
    }
    
    public ObjectiveCommand Operation(
        Argument<Selector> target,
        EnumReference<Operation> operation,
        int value
    ) {
        if (operation.Value == Lilypad.Operation.Assign) {
            return Set(target, value);
        }
        
        var variable = Constants.Get(_function.Datapack, value);
        return Operation(target, operation, variable.Selector, variable.Objective);
    }
}

public enum ObjectiveRenderType {
    Hearts,
    Integer
}