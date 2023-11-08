using System.Text;

namespace Lilypad; 

public class IfElse {
    readonly List<string> _allConditions = new();
    readonly Function _function;
    
    static readonly StringBuilder _builder = new();
    
    public IfElse(Function function, string[] conditions, Action<Function> build) {
        _function = function;
        CreateBranch(conditions, build);
    }
    
    public IfElse ElseIf(string condition, Action<Function> build) {
        return ElseIf(new[] { condition }, build);
    }
    
    public IfElse ElseIf(string[] conditions, Action<Function> build) {
        CreateBranch(conditions, build);
        return this;
    }
    
    public IfElse Else(Action<Function> build) {
        CreateBranch(Array.Empty<string>(), build);
        return this;
    }

    void CreateBranch(string[] conditions, Action<Function> build) {
        _builder.Clear();
        _builder.Append("execute ");
        
        _builder.Append(string.Join(" ", conditions.Select(c => $"if {c}")));
        _builder.Append(string.Join(" ", _allConditions.Select(c => $"unless {c}")));
        
        _allConditions.AddRange(conditions);
        _builder.Append($" run function {CreateFunction(build).Location}");
        _function.Add(_builder.ToString());
    }

    Function CreateFunction(Action<Function> build) {
        return _function.Datapack.Functions.Create(Names.Get($"{_function.Name}/branch/"), build, _function.Namespace);
    }
}