using Lilypad.Text;

namespace Lilypad.Helpers; 

internal static class Logging {
    const string Prefix = "<green>Lilypad</green> |";
    
    public static void Log(this Function function, string message, Function? source = null) {
        if (!CheckLoggingLevel(function, LoggingLevel.Info)) return;
        source ??= function;
        function.Tellraw("@a", $"{Prefix} {message} {GetSource(source)}");
    }
    
    public static void LogWarning(this Function function, string message, Function? source = null) {
        if (!CheckLoggingLevel(function, LoggingLevel.Warning)) return;
        source ??= function;
        function.Tellraw("@a", $"{Prefix} <yellow>Warning: {message}</yellow> {GetSource(source)}");
    }
    
    public static void LogError(this Function function, string message, Function? source = null) {
        if (!CheckLoggingLevel(function, LoggingLevel.Error)) return;
        source ??= function;
        function.Tellraw("@a", $"{Prefix} <red>Error: {message}</red> {GetSource(source)}");
    }
    
    static string GetSource(Resource function) {
        return $"<u><gray><copy {function.Name}>[{function.Namespace}:{function.Name}]";
    }
    
    static bool CheckLoggingLevel(Resource function, LoggingLevel level) {
        return function.Datapack.LoggingLevels.HasFlag(level);
    }
}

[Flags]
public enum LoggingLevel {
    None = 0,
    Info = 1,
    Warning = 2,
    Error = 4,
    All = Info | Warning | Error
}