namespace Lilypad; 

public static class BasicFunctionExtensions {
    public static Execute Execute(this Function function) {
        return function.AddAndReturn(new Execute(function));
    }

    public static Function Effect(
        this Function function,
        Argument<Selector> selector,
        EnumReference<Effect> effect,
        int seconds,
        byte amplifier,
        bool invisible = true
    ) {
        return function.Add($"effect give {selector} {effect} {seconds} {amplifier} {(invisible ? "true" : "false")}");
    }
    
    public static Function Effect(
        this Function function, 
        Effect effect, 
        int seconds, 
        byte amplifier, 
        bool invisible = true
    ) {
        return function.Effect(Selector.Self, effect, seconds, amplifier, invisible);
    }
    
    public static Function Gamemode(
        this Function function,
        Argument<Selector> selector,
        EnumReference<Gamemode> gamemode
    ) {
        return function.Add($"gamemode {gamemode} {selector}");
    }

    public static Function Gamemode(this Function function, EnumReference<Gamemode> gamemode) {
        return function.Gamemode(Selector.Self, gamemode);
    }

    public static Function Call(this Function function, Reference<Function> functionRef) {
        return function.Add($"function {functionRef}");
    }

    public static Function Say(this Function function, string message) {
        return function.Add($"say {message}");
    }
}