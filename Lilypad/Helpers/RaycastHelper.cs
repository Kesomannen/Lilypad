namespace Lilypad.Helpers;

public static class RaycastHelper {
    public static Raycast RaycastEntity(this Function function, Selector hitSelector, Action<Function> hitBuilder) {
        return function.DoRaycast(Raycast.Entity(function, hitSelector, hitBuilder));
    }

    public static Raycast DoRaycast(this Function function, Raycast raycast) {
        function.Call(raycast.Function);
        return raycast;
    }
}

public class Raycast {
    public int MaxSteps { get; set; }
    public Vector3 Step { get; set; }
    public Anchor Anchor { get; set; } = Anchor.Eyes;

    public Action<Function>? SetupBuilder { get; set; }
    public Action<Function>? CleanupBuilder { get; set; }
    public Action<Function, Reference<Function>>? LoopBuilder { get; set; }
    
    public List<Condition> Conditions { get; } = new();
    public ScoreVariable Counter { get; }

    public Function Function { get; }
    
    public Raycast(Resource resource) {
        var dir = Names.Get($"{resource.Name}/raycast");
        Function = resource.Datapack.Functions.Create(Names.Get($"{dir}/setup"), resource.Namespace);
        
        Counter = Temp.Get(Function, Names.Get("#raycast"));
        Conditions.Add(Condition.InRange(Counter, (0, MaxSteps)));

        Function.Add(f => {
            SetupBuilder?.Invoke(f);
            f.SetVariable(Counter, 0);

            f.Execute().Anchored(Anchor).Run(f => {
                f.Increment(Counter);
                f.If(Conditions.ToArray(), g => {
                    LoopBuilder?.Invoke(g, f);
                });
            });
            
            CleanupBuilder?.Invoke(f);
        });
    }

    public static Raycast Entity(
        Resource resource,
        Selector hitSelector, 
        Action<Function> hitBuilder, 
        bool ignoreSelf = true
    ) {
        var raycast = new Raycast(resource);

        if (ignoreSelf) {
            var tag = Names.Get("raycast_ignore");
            
            raycast.SetupBuilder = f => f.AddTag(tag);
            raycast.CleanupBuilder = f => f.RemoveTag(tag);
            hitSelector.NotTag(tag);
        }
        
        raycast.LoopBuilder = (f, loop) => {
            f.Execute().As(hitSelector).At("@s").Run(hitBuilder);
            f.Execute().Unless(Condition.Entity(hitSelector)).Positioned(raycast.Step).Run(loop);
        };
        return raycast;
    }
    
    public Raycast SetStepForward(double stepDistance) {
        Step = Vector3.Forward * stepDistance;
        return this;
    }
    
    public Raycast SetStepForward(double stepDistance, double maxDistance) {
        MaxSteps = (int)Math.Ceiling(maxDistance / stepDistance);
        return SetStepForward(stepDistance);
    }

    public Raycast SetBlockedBy(EnumReference<Block> block, bool invert = false) {
        var condition = Condition.Block(block);
        if (invert) {
            condition = condition.Not();
        }
        Conditions.Add(condition);
        return this;
    }
    
    public Raycast SetBlockedByNonAir() {
        return SetBlockedBy(Block.Air, invert: true);
    }
}