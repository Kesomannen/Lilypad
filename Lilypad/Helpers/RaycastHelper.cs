namespace Lilypad.Helpers;

public static class RaycastHelper {
    public static Raycast RaycastEntity(
        this Function function,
        Selector hitSelector, 
        Action<Function, Selector> hitBuilder,
        bool ignoreSelf = true,
        EntityRaycastOptions options = EntityRaycastOptions.AsAt
    ) {
        return function.DoRaycast(Raycast.Entity(function, hitSelector, hitBuilder, ignoreSelf, options));
    }

    public static Raycast DoRaycast(this Function function, Raycast raycast) {
        function.Call(raycast.Function);
        return raycast;
    }
}

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

    public static Raycast Entity(
        Resource resource,
        Selector hitSelector, 
        Action<Function, Selector> hitBuilder, 
        bool ignoreSelf = true,
        EntityRaycastOptions options = EntityRaycastOptions.AsAt
    ) {
        var raycast = new Raycast(resource);

        var tag = Names.Get("raycast_ignore");
        raycast.SetupBuilder = f => f.AddTag(tag);
        raycast.CleanupBuilder = f => f.RemoveTag(tag);
        
        if (ignoreSelf) {
            hitSelector.NotTag(tag);
        }
        
        raycast.LoopBuilder = (f, loop) => {
            Selector raycasterSelector;
            var condition = Condition.Entity(hitSelector);
            
            var execute = f.Execute();
            if (options.HasFlag(EntityRaycastOptions.As)) {
                raycasterSelector = Selector.Entites.Tag(tag).Sort(Sort.Nearest).Single();
                execute.As(hitSelector);
                
                if (options.HasFlag(EntityRaycastOptions.At)) {
                    execute.At("@s");
                }
            } else if (options.HasFlag(EntityRaycastOptions.At)) {
                raycasterSelector = Selector.Self;
                execute.At(hitSelector);
            } else {
                raycasterSelector = Selector.Self;
                execute.If(condition);
            }
            
            execute.Run(f => hitBuilder(f, raycasterSelector));
            f.Execute().Unless(condition).Positioned(raycast.Step).Run(loop);
        };
        return raycast;
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

[Flags]
public enum EntityRaycastOptions {
    None = 0,
    At = 1,
    As = 2,
    AsAt = At | As
}