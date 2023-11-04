namespace Lilypad; 

public class ExecuteCommand : CommandGenerator<ExecuteCommand> {
    protected override string BaseCommand => "execute";
    
    public ExecuteCommand(Function function) : base(function) { }
    
    public ExecuteCommand As(Argument<Selector> selector) => AddSegment($"as {selector}");
    public ExecuteCommand At(Argument<Selector> selector) => AddSegment($"at {selector}");

    public ExecuteCommand Run(Action<Function> build) {
        var name = Names.Get($"{Function.Name}_branch");
        var function = Function.Datapack.Functions
            .Create(name, build, Function.Namespace);
        return AddSegment($"run function {function.Location}", true);
    }
}