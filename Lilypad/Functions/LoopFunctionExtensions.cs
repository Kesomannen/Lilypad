using Lilypad.Helpers;

namespace Lilypad; 

/// <summary>
/// Provides function extension methods for creating loops.
/// </summary>
public static class LoopFunctionExtensions {
    static int _tempIndex;
    
    /// <summary>
    /// Creates a while loop.
    /// </summary>
    /// <param name="conditions">The condition(s) that have to pass for the loop to continue.</param>
    /// <param name="build">Builder method for the loop function (body.</param>
    /// <param name="initialCheck">
    /// Whether or not to check the condition(s) before starting the loop.
    /// If set to false, the loop behaves like a do-while loop.
    /// </param>
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
    
    /// <inheritdoc cref="While(Lilypad.Function,Lilypad.Condition[],System.Action{Lilypad.Function},bool)"/>
    public static Function While(this Function function, Condition condition, Action<Function> build, bool initialCheck = true) {
        return function.While(new[] { condition }, build, initialCheck);
    }
    
    /// <summary>
    /// Creates a do-while loop. Equivalent to <see cref="While(Lilypad.Function,Lilypad.Condition[],System.Action{Lilypad.Function},bool)"/> with initialCheck set to false.
    /// </summary>
    /// <param name="conditions">The condition(s) that have to pass for the loop to continue.</param>
    /// <param name="build">Builder method for the loop function (body).</param>
    public static Function DoWhile(this Function function, Condition[] conditions, Action<Function> build) {
        return function.While(conditions, build, false);
    }
    
    /// <inheritdoc cref="DoWhile(Lilypad.Function,Lilypad.Condition[],System.Action{Lilypad.Function})"/>
    public static Function DoWhile(this Function function, Condition condition, Action<Function> build) {
        return function.DoWhile(new[] { condition }, build);
    }
    
    /// <summary>
    /// Creates a for loop.
    /// </summary>
    /// <param name="init">Builder method for the start of the loop (the first segment of a standard for loop).</param>
    /// <param name="condition">The condition to check every iteration (second segment).</param>
    /// <param name="increment">Builder method for the incrementor (third segment).</param>
    /// <param name="build">Builder method for the loop function (body).</param>
    public static Function For(
        this Function function, 
        Action<Function> init, 
        Condition condition, 
        Action<Function> increment, 
        Action<Function> build
    ) {
        init(function);
        return function.While(
            condition, 
            f => {
                build(f);
                increment(f);
            }
        );
    }
    
    /// <summary>
    /// Creates a for loop between two values.
    /// </summary>
    /// <param name="start">Start value, inclusive.</param>
    /// <param name="end">End value, exclusive.</param>
    /// <param name="build">
    /// Builder method for the loop function (body).
    /// The second parameter contains the counter (commonly named i).
    /// </param>
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