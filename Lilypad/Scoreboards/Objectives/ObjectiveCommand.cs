using Lilypad.Text;

namespace Lilypad.Scoreboards;

public class ObjectiveCommand : CommandGenerator<ObjectiveCommand> {
    protected override string BaseCommand => "scoreboard";
    
    readonly Argument<Objective> _objective;
    
    public ObjectiveCommand(Function function, Argument<Objective> objective) : base(function) {
        _objective = objective;
    }
    
    public ObjectiveCommand SetDisplay(DisplaySlotArgument slot) {
        return AddLine($"objectives setdisplay {slot} {_objective}");
    }
    
    public ObjectiveCommand SetDisplayName(RichText displayName) {
        return AddLine($"objectives modify {_objective} displayname {displayName}");
    }
    
    public ObjectiveCommand SetRenderType(EnumReference<ObjectiveRenderType> renderType) {
        return AddLine($"objectives modify {_objective} rendertype {renderType}");
    }

    public ObjectiveCommand Get(Argument<Selector> target) {
        return AddLine($"players get {target} {_objective}");
    }
    
    public ObjectiveCommand Set(Argument<Selector> target, int score) {
        return AddLine($"players set {target} {_objective} {score}");
    }
    
    public ObjectiveCommand Add(Argument<Selector> target, int score) {
        return AddLine($"players add {target} {_objective} {score}");
    }
    
    public ObjectiveCommand Remove(Argument<Selector> target, int score) {
        return AddLine($"players remove {target} {_objective} {score}");
    }

    public ObjectiveCommand Reset(Argument<Selector> target) {
        return AddLine($"players reset {target} {_objective}");
    }

    public ObjectiveCommand Enable(Argument<Selector> target) {
        return AddLine($"players enable {target} {_objective}");
    }
    
    public ObjectiveCommand Operation(
        Argument<Selector> target, 
        EnumReference<Operation> operation, 
        Argument<Selector> source, 
        Argument<Objective>? sourceObjective = null
    ) {
        sourceObjective ??= _objective;
        var op = operation.Value switch {
            Scoreboards.Operation.Add => "+=",
            Scoreboards.Operation.Subtract => "-=",
            Scoreboards.Operation.Multiply => "*=",
            Scoreboards.Operation.Divide => "/=",
            Scoreboards.Operation.Modulo => "%=",
            Scoreboards.Operation.Assign => "=",
            Scoreboards.Operation.Min => "<",
            Scoreboards.Operation.Max => ">",
            Scoreboards.Operation.Swap => "><",
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
        };
        
        return AddLine($"players operation {target} {_objective} {op} {source} {sourceObjective}");
    }
}

public enum ObjectiveRenderType {
    Hearts,
    Integer
}