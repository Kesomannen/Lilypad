namespace Lilypad; 

public class IfElse {
    readonly List<Condition> _allConditions = new();
    readonly Function _function;
    
    public IfElse(Function function, Condition[] conditions, Action<Function> build) {
        _function = function;
        CreateBranch(conditions, build);
    }
    
    public IfElse ElseIf(Condition condition, Action<Function> build) {
        return ElseIf(new[] { condition }, build);
    }
    
    public IfElse ElseIf(Condition[] conditions, Action<Function> build) {
        CreateBranch(conditions, build);
        return this;
    }
    
    public IfElse Else(Action<Function> build) {
        CreateBranch(Array.Empty<Condition>(), build);
        return this;
    }

    void CreateBranch(Condition[] conditions, Action<Function> build) {
        var execute = _function.Execute();
        foreach (var condition in conditions) {
            execute.If(condition);
        }
        foreach (var condition in _allConditions) {
            execute.Unless(condition);
        }
        execute.Run(build);
        _allConditions.AddRange(conditions);
    }

    Function CreateFunction(Action<Function> build) {
        return _function.Datapack.Functions.Create(Names.Get($"{_function.Name}/branch/"), build, _function.Namespace);
    }
}