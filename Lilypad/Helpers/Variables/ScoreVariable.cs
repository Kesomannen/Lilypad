using Lilypad.Extensions;
using Lilypad;

namespace Lilypad.Helpers; 

public readonly struct ScoreVariable : IVariable {
    public readonly Argument<Selector> Selector;
    public readonly Reference<Objective> Objective;
    
    public ScoreVariable(Argument<Selector> selector, Reference<Objective> objective) {
        Selector = selector;
        Objective = objective;
    }

    public void Get(Function function) { 
        function.Scoreboard(Objective).Get(Selector);
    }

    public ExecuteCommand Set(ExecuteCommand execute) {
        return execute.Store(Selector, Objective);
    }
    
    public override string ToString() {
        return $"Variable-Score-{Objective}-{Selector}".Replace(" ", "$");
    }
}