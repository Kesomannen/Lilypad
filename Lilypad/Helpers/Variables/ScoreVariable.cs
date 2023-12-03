namespace Lilypad.Helpers; 

public struct ScoreVariable : IVariable, IEquatable<ScoreVariable> {
    public Argument<Selector> Selector;
    public Reference<Objective> Objective;
    
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
        return IVariable.GetName("Variable", "Score", Objective, Selector);
    }
    
    public static ScoreVariable Self(Reference<Objective> objective) {
        return new ScoreVariable(Lilypad.Selector.Self, objective);
    }

    public bool Equals(ScoreVariable other) {
        return Selector.Equals(other.Selector) && Objective.Equals(other.Objective);
    }
    
    public override bool Equals(object? obj) {
        return obj is ScoreVariable other && Equals(other);
    }
    
    public override int GetHashCode() {
        return HashCode.Combine(Selector, Objective);
    }
    
    public static bool operator ==(ScoreVariable left, ScoreVariable right) {
        return left.Equals(right);
    }
    
    public static bool operator !=(ScoreVariable left, ScoreVariable right) {
        return !left.Equals(right);
    }
}