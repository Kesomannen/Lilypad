namespace Lilypad.Scoreboards; 

public static class ScoreboardFunctionExtensions {
    public static ObjectiveCommand Scoreboard(this Function function, Argument<Objective> objective) {
        return function.AddAndReturn(new ObjectiveCommand(function, objective));
    }
    
    public static Function ClearScoreboard(this Function function, DisplaySlotArgument displaySlot) {
        return function.Add($"scoreboard objectives setdisplay {displaySlot}");
    }
    
    public static Function ResetScores(this Function function, Argument<Selector> target) {
        return function.Add($"scoreboard players reset {target}");
    }
}