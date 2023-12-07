namespace Lilypad.Helpers; 

public class EntityRaycast : Raycast {
    public Selector HitSelector { get; set; }
    public bool IgnoreSelf { get; set; } = true;
    public bool CancelOnHit { get; set; } = true;
    public SelectorOptions SelectorOptions { get; set; } = SelectorOptions.AsAt;
    public Action<Function, Selector>? HitBuilder { get; set; }

    public const string IgnoreTag = "raycast_ignore";
    
    public EntityRaycast(Resource resource) : base(resource) {
        HitSelector = Selector.Entites;
        
        SetupBuilder += f => f.AddTag(IgnoreTag);
        CleanupBuilder += f => f.RemoveTag(IgnoreTag);

        LoopBuilder = (f, loop) => {
            if (IgnoreSelf) {
                HitSelector.NotTag(IgnoreTag);
            }
            
            Selector raycasterSelector;
            var hitCondition = Condition.Entity(HitSelector);
            
            var execute = f.Execute();
            if (SelectorOptions.HasFlag(SelectorOptions.As)) {
                raycasterSelector = Selector.Entites.Tag(IgnoreTag).Sort(Sort.Nearest).Single();
                execute.As(HitSelector);
                
                if (SelectorOptions.HasFlag(SelectorOptions.At)) {
                    execute.At("@s");
                }
            } else if (SelectorOptions.HasFlag(SelectorOptions.At)) {
                raycasterSelector = Selector.Self;
                execute.At(HitSelector);
            } else {
                raycasterSelector = Selector.Self;
                execute.If(hitCondition);
            }
            
            execute.Run(f => HitBuilder?.Invoke(f, raycasterSelector));
            
            execute = f.Execute();
            if (CancelOnHit) {
                execute.Unless(hitCondition);
            }
            f.Execute().Positioned(Step).Run(loop);
        };
    }
    
    public EntityRaycast OnHit(Action<Function, Selector> builder) {
        HitBuilder += builder;
        return this;
    }
    
    public EntityRaycast Hit(Selector selector) {
        HitSelector = selector;
        return this;
    }

    public EntityRaycast SetDetectDistance(double distance) {
        HitSelector.Distance((0, distance));
        return this;
    }
    
    public EntityRaycast SetIgnoreSelf(bool ignore = true) {
        IgnoreSelf = ignore;
        return this;
    }
    
    public EntityRaycast SetCancelOnHit(bool cancel = true) {
        CancelOnHit = cancel;
        return this;
    }
    
    public EntityRaycast SetSelectorOptions(SelectorOptions options) {
        SelectorOptions = options;
        return this;
    }
}