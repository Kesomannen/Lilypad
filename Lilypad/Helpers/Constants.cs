using Lilypad.Scoreboards;

namespace Lilypad.Helpers; 

public static class Constants {
    static readonly HashSet<int> _values = new();
    
    public static void Get(Datapack datapack, int value, out string playerName, out string objectiveName) {
        objectiveName = "constants";
        playerName = $"#{value}";
        
        if (!_values.Add(value)) return;
        var objective = datapack.Objectives.GetOrCreate(objectiveName);
        datapack.GetInstallFunction()
            .Scoreboard(objective).Set(playerName, value);
    }

    public static ObjectiveCommand Operation(
        this ObjectiveCommand command,
        Argument<Selector> target,
        EnumReference<Operation> operation,
        int value
    ) {
        Get(command.Function.Datapack, value, out var playerName, out var objectiveName);
        return command.Operation(target, operation, playerName, objectiveName);
    }
}