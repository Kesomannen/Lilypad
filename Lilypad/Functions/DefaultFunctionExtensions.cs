using Lilypad.Extensions;
using Lilypad.ItemModifiers;
using Lilypad.NBT;

namespace Lilypad; 

public static class DefaultFunctionExtensions {
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
        return function.Add($"effect give {selector} {effect} {seconds} {amplifier} {invisible.ToString().ToLower()}");
    }
    
    public static Function Effect(
        this Function function, 
        Argument<Selector> selector,
        EnumReference<Effect> effect, 
        byte amplifier, 
        bool invisible = true
    ) {
        return function.Add($"effect give {selector} {effect} infinite {amplifier} {invisible.ToString().ToLower()}");
    }
    
    public static Function ClearEffect(this Function function, Argument<Selector> selector, EnumReference<Effect>? effect = null) {
        return function.Add($"effect clear {selector} {effect.ToStringOrEmpty()}");
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
    
    public static Function ModifyItem(this Function function, ItemSource source, Reference<ItemModifier> modifier) {
        return function.Add($"item modify {source} {modifier}");
    }
    
    public static Function ReplaceItem(
        this Function function,
        ItemSource target,
        Slot slot,
        EnumReference<Item> item,
        NBTCompound? nbt = null,
        int count = 1
    ) {
        return function.Add($"item replace {target} {slot} with {item}{nbt.ToStringOrEmpty()} {count}");
    }
    
    public static Function ReplaceItem(
        this Function function,
        ItemSource target,
        Slot slot,
        ItemSource source,
        Slot sourceSlot,
        Reference<ItemModifier>? modifier = null
    ) {
        return function.Add($"item replace {target} {slot} from {source} {sourceSlot}{modifier.ToStringOrEmpty()}");
    }
    
    public static Function Clear(this Function function, Argument<Selector> selector) {
        return function.Add($"clear {selector}");
    }
    
    public static Function Clear(this Function function) {
        return function.Clear(Selector.Self);
    }
    
    public static Function Teleport(this Function function, Argument<Selector> selector, Coordinate destination) {
        return function.Add($"tp {selector} {destination}");
    }
    
    public static Function Teleport(this Function function, Coordinate destination) {
        return function.Teleport(Selector.Self, destination);
    }
    
    public static Function Teleport(this Function function, Argument<Selector> selector, Argument<Selector> destination) {
        return function.Add($"tp {selector} {destination}");
    }
    
    public static Function Schedule(this Function function, float seconds, Reference<Function> functionRef) {
        return function.Add($"schedule function {functionRef} {(int)seconds * 20}t");
    }
}