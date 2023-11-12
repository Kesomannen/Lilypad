using Lilypad;

namespace Lilypad.Helpers;

public static class VariableFunctionExtensions {
    public static Function SetVariable(this Function function, IVariable variable, Action<Function> get) {
        variable.Set(function.Execute()).RunStore(get);
        return function;
    }
    
    public static Function SetVariable(this Function function, IVariable variable, IVariable value) {
        if (variable is ScoreVariable scoreVariable && value is ScoreVariable scoreValue) {
            function.Scoreboard(scoreVariable.Objective)
                .Operation(scoreVariable.Selector, "assign", scoreValue.Selector, scoreValue.Objective);
            
            return function;
        }

        return function.SetVariable(variable, value.Get);
    }
    
    public static Function SetVariable(this Function function, IVariable variable, int value) {
        if (variable is ScoreVariable scoreVariable) {
            function.Scoreboard(scoreVariable.Objective)
                .Operation(scoreVariable.Selector, "assign", value);
            
            return function;
        }
        
        return function.SetVariable(variable, Constants.Get(function.Datapack, value));
    }
    
    public static Function Operation(
        this Function function, 
        IVariable variable, 
        EnumReference<Operation> operation,
        IVariable value
    ) {
        if (variable is ScoreVariable scoreVariable && value is ScoreVariable scoreValue) {
            function.Scoreboard(scoreVariable.Objective)
                .Operation(scoreVariable.Selector, operation, scoreValue.Selector, scoreValue.Objective);
            return function;
        }
        
        var temp1 = Temp.Get(function, "#op0");
        var temp2 = Temp.Get(function, "#op1");
        function.SetVariable(temp1, variable);
        function.SetVariable(temp2, value);
        function.Scoreboard(temp1.Objective)
            .Operation(temp1.Selector, operation, temp2.Selector, temp2.Objective);
        return function.SetVariable(variable, temp1);
    }
    
    public static Function Operation(
        this Function function, 
        IVariable variable, 
        EnumReference<Operation> operation,
        int value
    ) {
        return function.Operation(variable, operation, Constants.Get(function, value));
    }
}