namespace Lilypad; 

public class Execute {
    readonly Function _function;

    string _text = "execute";
    
    internal Execute(Function function) {
        _function = function;
    }
    
    public Execute As(Argument<Selector> selector) => Add($"as {selector}");
    public Execute At(Argument<Selector> selector) => Add($"at {selector}");

    public void Run(Action<Function> build) {
        var function = _function.Datapack.Functions.Create(build);
        Add($"run function {function.Location}");
    }
    
    Execute Add(string command) {
        _text += $" {command}";
        return this;
    }

    public override string ToString() => _text;
}