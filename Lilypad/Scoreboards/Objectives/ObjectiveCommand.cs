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
    
    public ObjectiveCommand SetRenderType(EnumReference<RenderType> renderType) {
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
        Operation operation, 
        Argument<Selector> source, 
        Reference<Objective>? sourceObjective = null
    ) {
        sourceObjective ??= _objective;
        return AddCommand($"players operation {target} {_objective} {operation} {source} {sourceObjective}");
    }
    
    public ObjectiveCommand Operation(
        Argument<Selector> target,
        Operation operation,
        int value
    ) {
        switch (operation.Type) {
            case OperationType.Assign:
                return Set(target, value);
            
            case OperationType.Add:
                return Add(target, value);
            
            case OperationType.Subtract:
                return Remove(target, value);

            case OperationType.Multiply:
            case OperationType.Divide:
            case OperationType.Modulo:
            case OperationType.Min:
            case OperationType.Max:
            case OperationType.Swap:
            default: {
                var constant = Constants.Get(_function.Datapack, value);
                return Operation(target, operation, constant.Selector, constant.Objective);
            }
        }
    }
}

public enum RenderType {
    Hearts,
    Integer
}