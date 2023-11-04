using Lilypad.Text;

namespace Lilypad.Scoreboards;

public class ObjectiveCommand : GeneratedCommand<ObjectiveCommand> {
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
    
    public Targetted Select(Argument<Selector> target) {
        return new Targetted(this, target);
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
        Argument<Objective> sourceObjective
    ) {
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

    public class Targetted {
        readonly ObjectiveCommand _command;
        readonly Argument<Selector> _target;
        
        public Targetted(ObjectiveCommand command, Argument<Selector> target) {
            _command = command;
            _target = target;
        }
        
        public ObjectiveCommand Set(int score) => _command.Set(_target, score);
        public ObjectiveCommand Add(int score) => _command.Add(_target, score);
        public ObjectiveCommand Remove(int score) => _command.Remove(_target, score);
        public ObjectiveCommand Reset() => _command.Reset(_target);
        public ObjectiveCommand Enable() => _command.Enable(_target);
        public ObjectiveCommand Get() => _command.Get(_target);
        public ObjectiveCommand Operation(
            EnumReference<Operation> operation, 
            Argument<Selector> source, 
            Argument<Objective> sourceObjective
        ) => _command.Operation(_target, operation, source, sourceObjective);
    }
}

public enum ObjectiveRenderType {
    Hearts,
    Integer
}