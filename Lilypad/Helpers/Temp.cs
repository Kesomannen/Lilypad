namespace Lilypad.Helpers; 

public static class Temp {
    static Objective? _objective;

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

    static Objective GetObjective(Datapack datapack) {
        return _objective ??= datapack.Objectives.GetOrCreate("temp");
    }
}