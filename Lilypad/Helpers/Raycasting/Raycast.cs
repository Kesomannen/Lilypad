namespace Lilypad.Helpers;

public class Raycast {
    public int MaxSteps { get; set; } = 10;
    public Vector3 Step { get; set; } = Vector3.Forward;
    public Anchor Anchor { get; set; } = Anchor.Eyes;

    public Action<Function>? SetupBuilder { get; set; }
    public Action<Function>? CleanupBuilder { get; set; }
    public Action<Function, Reference<Function>>? LoopBuilder { get; set; }
    public Action<Function>? MissBuilder { get; set; }
    
    public List<Condition> Conditions { get; } = new();
    public ScoreVariable Counter { get; }

    public Function Function { get; }
    
    public Raycast(Resource resource) {
        Function = resource.Datapack.Functions.Create(Names.Get($"{resource.Name}/raycast/"), resource.Namespace);
        
        Counter = Temp.Get(Function, Names.Get("#raycast"));
        Conditions.Add(Condition.Matches(Counter, (0, MaxSteps)));

        Function.Add(f => {
            SetupBuilder?.Invoke(f);
            f.SetVariable(Counter, 0);

            f.Execute().Anchored(Anchor).Positioned(Step).Run(f => {
                f.Increment(Counter);
                f.If(Conditions.ToArray(), g => {
                    LoopBuilder?.Invoke(g, f);
                }).Else(f => {
                    MissBuilder?.Invoke(f);
                });
            });
            
            CleanupBuilder?.Invoke(f);
        });
    }

    public Raycast StepForward(double stepDistance) {
        Step = Vector3.Forward * stepDistance;
        return this;
    }
    
    public Raycast StepForward(double stepDistance, double maxDistance) {
        MaxSteps = (int)Math.Ceiling(maxDistance / stepDistance);
        return StepForward(stepDistance);
    }

    public Raycast BlockedBy(EnumReference<Block> block, bool invert = false) {
        var condition = Condition.Block(block);
        if (!invert) {
            condition = condition.Not();
        }
        Conditions.Add(condition);
        return this;
    }
    
    public Raycast BlockedBySolids() {
        return BlockedBy(Block.Air, invert: true);
    }
    
    public Raycast AddSetup(Action<Function> builder) {
        SetupBuilder += builder;
        return this;
    }
    
    public Raycast OnCleanup(Action<Function> builder) {
        CleanupBuilder += builder;
        return this;
    }
    
    public Raycast OnLoop(Action<Function, Reference<Function>> builder) {
        LoopBuilder += builder;
        return this;
    }
    
    public Raycast OnMiss(Action<Function> builder) {
        MissBuilder += builder;
        return this;
    }
    
    public Raycast Visualize(EnumReference<Particle> particle) {
        OnLoop((f, _) => f.Particle(particle, Vector3.Here, Vector3.Zero, 0, 1));
        return this;
    }
    
    public Raycast Anchored(Anchor anchor) {
        Anchor = anchor;
        return this;
    }
}