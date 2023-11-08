using Lilypad;
using Random = Lilypad.Extensions.Random;

namespace Lilypad.Helpers; 

public static class Scheduling {
    const string ObjectiveName = "scheduling";
    const int MaxChannels = 100000;
    
    static readonly HashSet<int> _channels = new();
    
    public static int GetOpenChannel() {
        for (var i = 0; i < MaxChannels; i++) {
            if (_channels.Add(i)) return i;
        }
        throw new Exception("No open scheduling channels");
    }

    public static Function DelayedCall(this Function function, float seconds, Action<Function> build, int? channel = null) {
        return DelayedCallInternal(function, seconds, e => e.Run(build), channel);
    }
    
    public static Function DelayedCall(this Function function, float seconds, Function toCall, int? channel = null) {
        return DelayedCallInternal(function, seconds, e => e.Run(toCall), channel);
    }
    
    static Function DelayedCallInternal(this Function function, float seconds, Action<ExecuteCommand> execute, int? channel = null) {
        var datapack = function.Datapack;
        
        channel ??= GetOpenChannel();
        var objective = datapack.Objectives.GetOrCreate($"{ObjectiveName}{channel}");
        
        var currentTick = CurrentTick.Get(datapack);
        var executingTick = new ScoreVariable("@s", objective);

        function.SetVariable(executingTick, currentTick)
            .Operation(executingTick, Operation.Add, (int)seconds * 20);
        
        var result = datapack.Functions.Create(Names.Get($"{function.Name}/delayed_call{channel}_"), f => {
            var condition = Condition.Score(currentTick, Comparison.GreaterThanOrEqual, executingTick);
            execute(f.Execute().As("@e").If(condition).At("@s"));
        }, function.Namespace);

        function.Schedule(seconds, result);
        return result;
    }
}