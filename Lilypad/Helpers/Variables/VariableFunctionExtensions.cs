namespace Lilypad.Helpers;

public static class VariableFunctionExtensions {
    public static Function SetVariable(this Function function, IVariable variable, Action<Function> get) {
        variable.Set(function.Execute()).RunStore(get);
        return function;
    }
    
    public static Function SetVariable(this Function function, IVariable variable, IVariable value) {
        if (Equals(variable, value)) return function;
        
        if (variable is ScoreVariable scoreVariable && value is ScoreVariable scoreValue) {
            function.Scoreboard(scoreVariable.Objective)
                .Operation(scoreVariable.Selector, OperationType.Assign, scoreValue.Selector, scoreValue.Objective);
            
            return function;
        }

        return function.SetVariable(variable, value.Get);
    }
    
    public static Function SetVariable(this Function function, IVariable variable, int value) {
        return Operation(function, variable, OperationType.Assign, value);
    }
    
    public static Function Operation(
        this Function function, 
        IVariable variable, 
        Operation operation,
        IVariable other
    ) {
        var op0 = function.ToScore(variable, "#op0");
        var op1 = function.ToScore(other, "#op1");

        function.Scoreboard(op0.Objective)
            .Operation(op0.Selector, operation, op1.Selector, op1.Objective);
        
        return SetVariable(function, variable, op0);
    }
    
    public static Function Operation(
        this Function function, 
        IVariable variable, 
        Operation operation,
        int value
    ) {
        if (variable is ScoreVariable score) {
            function.Scoreboard(score.Objective)
                .Operation(score.Selector, operation, value);
            
            return function;
        }

        return function.Operation(variable, operation, Constants.Get(function, value));
    }
    
    public static Function Increment(this Function function, IVariable variable) {
        return function.Operation(variable, OperationType.Add, 1);
    }
}