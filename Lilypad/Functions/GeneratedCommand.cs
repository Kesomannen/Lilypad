namespace Lilypad;

public abstract class GeneratedCommand<TSelf> where TSelf : GeneratedCommand<TSelf> {
    protected Function Function { get; }
    
    protected abstract string BaseCommand { get; }

    readonly List<string> _lines = new();
    
    protected GeneratedCommand(Function function) {
        Function = function;
    }
    
    protected TSelf Add(string component) {
        if (_lines.Count == 0) {
            return AddLine(component);
        }
        _lines[^1] += $" {component}";
        return (TSelf)this;
    }
    
    protected TSelf AddLine(string command) {
        _lines.Add($"{BaseCommand} {command}");
        return (TSelf)this;
    }
    
    public override string ToString() => string.Join('\n', _lines);
}