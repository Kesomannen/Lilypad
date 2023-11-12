using Lilypad;

namespace Lilypad.Helpers; 

public static class Temp {
    static Objective? _objective;
    
    public static ScoreVariable Get(Resource resource, int index = 0) {
        return Get(resource.Datapack, index);
    }
    
    public static ScoreVariable Get(Datapack datapack, int index = 0) {
        return Get(datapack, $"#temp{index}");
    }

    public static ScoreVariable Get(Resource resource, string name) {
        return Get(resource.Datapack, name);
    }

    public static ScoreVariable Get(Datapack datapack, string name) {
        return new ScoreVariable(name, GetObjective(datapack));
    }
    
    public static ScoreVariable CopyTemp(this Function function, IVariable variable, string name) {
        var copy = Get(function, name);
        function.SetVariable(copy, variable);
        return copy;
    }
    
    public static ScoreVariable CopyTemp(this Function function, IVariable variable, int index = 0) {
        return function.CopyTemp(variable, $"#temp{index}");
    }
    
    public static ScoreVariable CopyToScore(this Function function, IVariable variable, string name) {
        if (variable is ScoreVariable scoreVariable) {
            return scoreVariable;
        }
        return function.CopyTemp(variable, name);
    }
    
    public static ScoreVariable CopyToScore(this Function function, IVariable variable, int index = 0) {
        return function.CopyToScore(variable, $"#temp{index}");
    }

    static Objective GetObjective(Datapack datapack) {
        return _objective ??= datapack.Objectives.GetOrCreate("temp");
    }
}