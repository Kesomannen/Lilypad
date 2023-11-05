using Lilypad.Extensions;
using Lilypad.NBT;

namespace Lilypad; 

public static class BasicFunctionExtensions {
    public static ExecuteCommand Execute(this Function function) {
        return new ExecuteCommand(function);
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
    
    public static Function Kill(this Function function, Argument<Selector> selector) {
        return function.Add($"kill {selector}");
    }
    
    public static Function Kill(this Function function) {
        return function.Kill(Selector.Self);
    }

    public static Function Give(
        this Function function,
        Argument<Selector> selector,
        EnumReference<Item> item,
        NBTCompound? nbt = null,
        int count = 1
    ) {
        return function.Add($"give {selector} {item}{nbt.ToStringOrEmpty()} {count}");
    }
}