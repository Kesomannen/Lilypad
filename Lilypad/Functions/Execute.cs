namespace Lilypad; 

public class Execute : GeneratedCommand<Execute> {
    protected override string BaseCommand => "execute";
    
    public Execute(Function function) : base(function) { }
    
    public Execute As(Argument<Selector> selector) => Add($"as {selector}");
    public Execute At(Argument<Selector> selector) => Add($"at {selector}");

    public void Run(Action<Function> build) {
        var name = Names.Get($"{Function.Name}_branch");
        var function = Function.Datapack.Functions
            .Create(name, build, Function.Namespace);
        Add($"run function {function.Location}");
    }
}