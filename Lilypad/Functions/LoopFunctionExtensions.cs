using Lilypad.Helpers;

namespace Lilypad; 

public static class LoopFunctionExtensions {
    static int _tempIndex;
    
    public static Function While(this Function function, Condition[] conditions, Action<Function> build, bool initialCheck = true) {
        var whileFunction = function.Datapack.Functions.Create(Names.Get($"{function.Name}/loop/"), build, function.Namespace);
        whileFunction.If(conditions, f => f.Call(whileFunction));
        if (initialCheck) {
            function.If(conditions, f => f.Call(whileFunction));
        } else {
            function.Call(whileFunction);
        }
        return function;
    }
    
    public static Function While(this Function function, Condition condition, Action<Function> build) {
        return function.While(new[] { condition }, build);
    }
    
    public static Function DoWhile(this Function function, Condition[] conditions, Action<Function> build) {
        return function.While(conditions, build, false);
    }
    
    public static Function DoWhile(this Function function, Condition condition, Action<Function> build) {
        return function.DoWhile(new[] { condition }, build);
    }
    
    public static Function For(
        this Function function, 
        Action<Function> start, 
        Condition condition, 
        Action<Function> increment, 
        Action<Function> build
    ) {
        start(function);
        return function.While(
            condition, 
            f => {
                build(f);
                increment(f);
            }
        );
    }
    
    public static Function For(this Function function, int start, int end, Action<Function, ScoreVariable> build) {
        var variable = Temp.Get(function, $"#for{_tempIndex++}");
        return function.For(
            f => f.SetVariable(variable, start), 
            Condition.Score(variable, (start, end - 1)), 
            f => f.Operation(variable, Operation.Add, 1),
            f => build(f, variable)
        );
    }
}