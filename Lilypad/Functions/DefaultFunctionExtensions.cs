using Lilypad.Extensions;
using Lilypad;
using Lilypad.Helpers;
using Lilypad.ItemModifiers;

namespace Lilypad; 

/// <summary>
/// Provides function extension methods for common minecraft commands.
/// </summary>
public static class DefaultFunctionExtensions {
    /// <summary>
    /// Creates a new execute command in the function.
    /// </summary>
    /// <seealso cref="ExecuteCommand"/>
    public static ExecuteCommand Execute(this Function function) {
        return new ExecuteCommand(function);
    }
    
    /// <summary>
    /// Creates a new if-branch in the function.
    /// </summary>
    /// <param name="conditions">All of these have to pass for the branch to execute.</param>
    /// <param name="build">Builder function for the created branch. Will be executed immediately.</param>
    /// <returns>The created <see cref="IfElse"/> instance, which can be used for more advanced branching.</returns>
    public static IfElse If(this Function function, Condition[] conditions, Action<Function> build) {
        return new IfElse(function, conditions, build);
    }
    
    /// <summary>
    /// Creates a new if-branch in the function.
    /// </summary>
    /// <param name="condition">Has to pass for the branch to execute</param>
    /// <param name="build">Builder function for the created branch. Will be executed immediately.</param>
    /// <returns>The created <see cref="IfElse"/> instance, which can be used for more advanced branching.</returns>
    public static IfElse If(this Function function, Condition condition, Action<Function> build) {
        return function.If(new[] { condition }, build);
    }

    /// <summary>
    /// Gives an effect.
    /// </summary>
    /// <param name="selector">Specifies the target(s).</param>
    /// <param name="effect">Specifies the effect to be added.</param>
    /// <param name="seconds">
    /// Defaults to infinity.
    /// Specifies the effect's duration in seconds (or in gameticks for instant_damage, instant_health, and saturation).
    /// Must be between 0 and 1000000 (inclusive).
    /// </param>
    /// <param name="amplifier">
    /// Specifies the number of additional levels to add to the effect.
    /// Note that the first tier of a status effect (e.g. Regeneration I) is 0.
    /// </param>
    /// <param name="invisible">
    /// Specifies whether the particles and the HUD indicator of the effect should be hidden.
    /// </param>
    public static Function Effect(
        this Function function,
        Argument<Selector> selector,
        EnumReference<Effect> effect,
        int seconds,
        byte amplifier,
        bool invisible = true
    ) {
        Assert.IsTrue(seconds is >= 0 and <= 1000000, "Effect duration must be between 0 and 1000000 (inclusive).");
        return function.Add($"effect give {selector} {effect} {seconds} {amplifier} {invisible.ToString().ToLower()}");
    }
    
    /// <summary>
    /// Gives an infinite effect.
    /// </summary>
    /// <param name="selector">Specifies the target(s).</param>
    /// <param name="effect">Specifies the effect to be added.</param>
    /// <param name="amplifier">
    /// Specifies the number of additional levels to add to the effect.
    /// Note that the first tier of a status effect (e.g. Regeneration I) is 0.
    /// </param>
    /// <param name="invisible">
    /// Specifies whether the particles and the HUD indicator of the effect should be hidden.
    /// </param>
    public static Function Effect(
        this Function function, 
        Argument<Selector> selector,
        EnumReference<Effect> effect, 
        byte amplifier, 
        bool invisible = true
    ) {
        return function.Add($"effect give {selector} {effect} infinite {amplifier} {invisible.ToString().ToLower()}");
    }
    
    /// <summary>
    /// Clears effect(s).
    /// </summary>
    /// <param name="effect">The effect to clear. If left unset, clears all effects.</param>
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
    
    public static Function ModifyItem(
        this Function function,
        ItemSource target, 
        Slot slot,
        Reference<ItemModifier> modifier
    ) {
        return function.Add($"item modify {target} {slot} {modifier}");
    }

    public static Function ModifyItem(
        this Function function,
        ItemSource target,
        Slot slot,
        params ItemFunction[] functions
    ) {
        return function.ModifyItem(target, slot, function.Datapack.ItemModifiers.Create().AddFunctions(functions));
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
    
    public static Function Teleport(this Function function, Argument<Selector> selector, Vector3 destination) {
        return function.Add($"tp {selector} {destination}");
    }
    
    public static Function Teleport(this Function function, Vector3 destination) {
        return function.Teleport(Selector.Self, destination);
    }
    
    public static Function Teleport(this Function function, Argument<Selector> selector, Argument<Selector> destination) {
        return function.Add($"tp {selector} {destination}");
    }
    
    public static Function Schedule(this Function function, float seconds, Reference<Function> functionRef) {
        return function.Add($"schedule function {functionRef} {seconds * 20:0}t");
    }
    
    public static Function Gamerule(this Function function, string gamerule, object value) {
        return function.Add($"gamerule {gamerule} {value}");
    }
    
    public static Function Comment(this Function function, string comment) {
        return function.Add($"# {comment}");
    }
    
    public static Function SetExperience(this Function function, Argument<Selector> selector, int experience, EnumReference<ExperienceType> type) {
        return function.Add($"xp set {selector} {experience} {(type == ExperienceType.Levels ? "levels" : "points")}");
    }
    
    public static Function AddExperience(this Function function, Argument<Selector> selector, int experience, EnumReference<ExperienceType> type) {
        return function.Add($"xp add {selector} {experience} {(type == ExperienceType.Levels ? "levels" : "points")}");
    }
    
    public static Function SetBlock(this Function function, Vector3 position, EnumReference<Block> block) {
        return function.Add($"setblock {position} {block}");
    }
}

public enum ExperienceType {
    Points,
    Levels
}