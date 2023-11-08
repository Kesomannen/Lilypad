using Lilypad;

namespace Lilypad.Helpers; 

public static class CurrentTick {
    const string PlayerName = "#current_tick";
    
    static ScoreVariable? _variable;
    
    public static ScoreVariable Get(Datapack datapack) {
        if (_variable != null) {
            return _variable.Value;
        }
        
        _variable = new ScoreVariable(PlayerName, datapack.Objectives.GetOrCreate("current_tick"));

        datapack.GetInstallFunction().SetVariable(_variable, 0);
        
        datapack.Functions.Create(f => {
            f.Scoreboard(_variable.Value.Objective).Add(PlayerName, 1);
        }).SetTick();
        
        return _variable.Value;
    }
}