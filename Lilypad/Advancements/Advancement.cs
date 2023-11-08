namespace Lilypad;

public class Advancement : Resource {
    public string[][]? Requirements;
    public bool? SendsTelemetryEvent;
    public Reference<Advancement>? Parent;
    public Display? Display;

    public Rewards Rewards = new();
    
    Dictionary<string, Criterion> Criteria { get; } = new();
    
    internal Advancement(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) { }
    
    public Advancement SetRequirements(params Criterion[][] requirements) {
        Requirements = requirements.Select(array => array.Select(criterion => criterion.Name).ToArray()).ToArray();
        return this;
    }

    public Function GetRewardFunction() {
        return (Rewards.Function ??= Datapack.Functions.Create($"{Name}/reward")).Resource!;
    }
    
    public Function CreateRewardFunction(Action<Function> build) {
        var name = Names.Get($"{Name}/reward/");
        var function = Datapack.Functions.Create(name, build, Namespace);
        GetRewardFunction().Call(function);
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
    
    public Advancement SetParent(Reference<Advancement> parent) {
        Parent = parent;
        return this;
    }
    
    public Advancement SetSendsTelemetryEvent(bool sendsTelemetryEvent) {
        SendsTelemetryEvent = sendsTelemetryEvent;
        return this;
    }
    
    public Advancement SetRewards(Rewards rewards) {
        Rewards = rewards;
        return this;
    }
}