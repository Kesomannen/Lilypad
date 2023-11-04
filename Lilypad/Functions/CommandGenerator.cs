namespace Lilypad;

public abstract class CommandGenerator<TSelf> where TSelf : CommandGenerator<TSelf> {
    public Function Function { get; }
    
    protected abstract string BaseCommand { get; }

    readonly List<string> _lines = new();
    
    protected CommandGenerator(Function function) {
        Function = function;
    }
    
    protected TSelf AddSegment(string component, bool finish = false) {
        if (_lines.Count == 0) {
            return AddLine(component, finish);
        }
        _lines[^1] += $" {component}";
        if (finish) Finish();
        return (TSelf)this;
    }
    
    protected TSelf AddLine(string command, bool finish = true) {
        _lines.Add($"{BaseCommand} {command}");
        if (finish) Finish();
        return (TSelf)this;
    }
    
    public TSelf Finish() {
        Function.Add(_lines[^1]);
        return (TSelf)this;
    }
}