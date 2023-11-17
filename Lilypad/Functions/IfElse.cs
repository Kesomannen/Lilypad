namespace Lilypad; 

/// <summary>
/// Abstracts the creation of an if-else chain.
/// </summary>
/// <example>
/// <code>
/// function.If(Condition.Biome("jungle"), f => {
///     f.Say("You are in a jungle!");
/// }).ElseIf(Condition.Biome("desert"), f => {
///     f.Say("You are in a desert!");
/// }).Else(f => {
///     f.Say("You are somewhere else!");
/// });
/// </code>
/// </example>
public class IfElse {
    readonly List<Condition> _allConditions = new();
    readonly Function _function;
    
    /// <summary>
    /// Creates a new if-else chain in the function.
    /// </summary>
    /// <param name="conditions">All must pass for the branch to execute.</param>
    /// <param name="build">Builder function for the if branch. Will be executed immediately.</param>
    public IfElse(Function function, Condition[] conditions, Action<Function> build) {
        _function = function;
        CreateBranch(conditions, build);
    }
    
    /// <summary>
    /// Creates an else if branch.
    /// </summary>
    /// <param name="condition">Must pass for the branch to execute.</param>
    /// <param name="build">Builder function for the branch. Will be executed immediately.</param>
    public IfElse ElseIf(Condition condition, Action<Function> build) {
        return ElseIf(new[] { condition }, build);
    }
    
    /// <summary>
    /// Creates an else if branch.
    /// </summary>
    /// <param name="conditions">All must pass for the branch to execute.</param>
    /// <param name="build">Builder function for the branch. Will be executed immediately.</param>
    public IfElse ElseIf(Condition[] conditions, Action<Function> build) {
        CreateBranch(conditions, build);
        return this;
    }
    
    /// <summary>
    /// Creates an else branch.
    /// </summary>
    /// <param name="build">Builder function for the branch. Will be executed immediately.</param>
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
}