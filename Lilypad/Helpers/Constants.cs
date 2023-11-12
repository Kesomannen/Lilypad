namespace Lilypad.Helpers; 

public static class Constants {
    static readonly HashSet<int> _values = new();
    
    static Objective? _objective;
    
    public static ScoreVariable Get(Resource resource, int value) {
        return Get(resource.Datapack, value);
    }
    
    public static ScoreVariable Get(Datapack datapack, int value) {
        _objective ??= datapack.Objectives.GetOrCreate("constants");
        var variable = new ScoreVariable($"#{value}", _objective);
        if (_values.Add(value)) {
            datapack.GetInstallFunction()
                .Scoreboard(variable.Objective).Set(variable.Selector, value);
        }
        return variable;
    }
}