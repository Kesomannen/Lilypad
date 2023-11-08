using Lilypad;

namespace Lilypad.Helpers; 

public static class Temp {
    static Objective? _objective;
    
    public static ScoreVariable Get(Datapack datapack, int index = 0) {
        return Get(datapack, $"#temp{index}");
    }
    
    public static ScoreVariable Get(Datapack datapack, string name) {
        return new ScoreVariable(name, GetObjective(datapack));
    }
    
    static Objective GetObjective(Datapack datapack) {
        return _objective ??= datapack.Objectives.GetOrCreate("temp");
    }
}