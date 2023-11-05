namespace Lilypad.Advancements;

public class Advancement : Resource {
    public string[][]? Requirements;
    public bool? SendsTelemetryEvent;
    public Reference<Advancement>? Parent;
    public Display? Display;

    public Rewards Rewards = new();
    public Dictionary<string, Criterion> Criteria = new();
    
    internal Advancement(string name, Datapack datapack) : base(name, datapack) { }
    
    public Advancement SetRequirements(params Criterion[][] requirements) {
        Requirements = requirements.Select(array => array.Select(criterion => criterion.Name).ToArray()).ToArray();
        return this;
    }

    public Advancement AddRewardFunction(Function function) {
        var func = (Rewards.Function ??= Datapack.Functions.Create($"{Name}_reward")).Resource;
        if (func == null) {
            throw new InvalidOperationException("Reward function is null");
        }
        func.Call(function);
        return this;
    }
    
    public Function CreateRewardFunction(Action<Function> build) {
        var name = Names.Get($"{Name}_reward");
        var function = Datapack.Functions.Create(name, build, Namespace);
        AddRewardFunction(function);
        return function;
    }
    
    public Function OnComplete(Action<Function> build) {
        return CreateRewardFunction(f => {
            f.Advancement("revoke").Only(this);
            build(f);
        });
    }
    
    public Advancement AddCriteria(params Criterion[] criteria) {
        foreach (var criterion in criteria) {
            if (Criteria.ContainsKey(criterion.Name)) {
                throw new ArgumentException($"A criterion with the name '{criterion.Name}' already exists");
            }
        
            Criteria.Add(criterion.Name, criterion);
        }
        return this;
    }

    public Advancement SetDisplay(Display display) {
        Display = display;
        return this;
    }
    
    public Advancement SetParent(Advancement parent) {
        Parent = parent;
        return this;
    }
}