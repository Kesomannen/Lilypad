using Lilypad.Extensions;

namespace Lilypad.Helpers; 

public static class Scheduling {
    static int _channelIndex;
    
    public static Function DelayedCall(this Function function, float seconds, Action<Function> build, string? channel = null) {
        return DelayedCallInternal(function, seconds, e => e.Run(build), channel);
    }
    
    public static Function DelayedCall(this Function function, Function toCall, float seconds, string? channel = null) {
        return DelayedCallInternal(function, seconds, e => e.Run(toCall), channel);
    }
    
    static Function DelayedCallInternal(this Function function, float seconds, Action<ExecuteCommand> execute, string? channel = null) {
        var datapack = function.Datapack;

        channel ??= _channelIndex++.ToString();

        var objective = datapack.Objectives.GetOrCreate($"delayed_{channel}");
        
        var currentTick = CurrentTick.Get(datapack);
        var executingTick = new ScoreVariable("@s", objective);
        function.Evaluate(executingTick, $"{currentTick} + {seconds.ToTicks()}", currentTick);
        
        var result = datapack.Functions.Create(Names.Get($"{function.Name}/delayed/{channel}_"), f => {
            var condition = Condition.Score(currentTick, Comparison.Equal, executingTick);
            execute(f.Execute().As("@e").If(condition).At("@s"));
        }, function.Namespace);

        function.Schedule(seconds, result);
        return result;
    }
}