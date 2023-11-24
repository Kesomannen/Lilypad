namespace Lilypad.Helpers; 

public static class RaycastHelper {
    public const double DefaultStepSize = 0.5;
    
    public static Function RaycastForward(
        this Function function,
        double maxDistance, 
        Selector selector,
        Action<Function> builder, 
        double stepSize = DefaultStepSize,
        bool visualize = false
    ) {
        var iterations = (int) Math.Ceiling(maxDistance / stepSize);
        var ignoreTag = Names.Get("raycast_ignore");
        function.AddTag(ignoreTag);
        
        return function.Raycast(
            (0d, 0d, stepSize).AsLocal(), 
            iterations, 
            selector.NotTag(ignoreTag).Distance((0, stepSize / 2)),
            f => {
                function.Execute().As(Selector.Entites.Tag(ignoreTag).Single()).Run(f => f.RemoveTag(ignoreTag));
                builder(f);
            }, 
            visualize
        );
    }
    
    public static Function Raycast(this Function function, Vector3 step, int maxIterations, Argument<Selector> selector, Action<Function> builder, bool visualize = false) {
        var counter = Temp.Get(function, Names.Get("#raycast"));
        function.SetVariable(counter, 0);
        var raycast = function.Datapack.Functions.Create(Names.Get($"{function.Name}/loop/"), function.Namespace);
        raycast.Add(f => {
            if (visualize) {
                f.Particle(Particle.ElectricSpark, Vector3.Here, Vector3.Zero, 0, 1, viewers: "@s");
            }

            f.Operation(counter, Operation.Add, 1);
            f.If(Condition.Score(counter, (0, maxIterations)), f => {
                f.Execute().As(selector).At("@s").Run(builder);
                f.Execute().Unless(Condition.Entity(selector)).Positioned(step).Run(raycast);
            });
        });
        function.Execute().Anchored(Anchor.Eyes).Positioned(step).Run(raycast);
        return raycast;
    }
}