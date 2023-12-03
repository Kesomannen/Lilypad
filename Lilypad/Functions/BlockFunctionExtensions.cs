using Lilypad.Extensions;
using Lilypad.Helpers;

namespace Lilypad; 

public static class BlockFunctionExtensions {
    public static Function SetBlock(this Function function, Vector3 position, BlockData block) {
        return function.Add($"setblock {position} {block}");
    }
    
    public static Function Fill(
        this Function function,
        Vector3 from,
        Vector3 to,
        BlockData block,
        EnumReference<FillMode>? mode = null,
        BlockData? replaceFilter = null
    ) {
        return function.Add($"fill {from} {to} {block} {mode.ToStringOrEmpty()} {replaceFilter.ToStringOrEmpty()}");
    }
    
    public static Function Fill(
        this Function function,
        Vector3Range range,
        BlockData block,
        EnumReference<FillMode>? mode = null,
        BlockData? replaceFilter = null
    ) {
        Assert.IsFinite(range, nameof(range));
        return function.Add($"fill {range.Min} {range.Max} {block} {mode.ToStringOrEmpty()} {replaceFilter.ToStringOrEmpty()}");
    }
    
    public static CloneCommand.ILevel0 Clone(this Function function) {
        return new CloneCommand(function);
    }
}

public enum FillMode {
    /// <summary>
    /// Replaces all blocks (including air) in the fill region with the specified block,
    /// dropping the existing blocks (including those that are unchanged) and block contents
    /// as entities as if they had been mined with an unenchanted diamond shovel or pickaxe.
    /// </summary>
    /// <remarks>
    /// Blocks that can be mined only with shears, such as vines, do not drop; neither do liquids.
    /// </remarks>
    Destroy,
    
    /// <summary>
    /// Replaces only the blocks on the outer edge of the fill region with the specified block.
    /// Inner blocks are changed to air, dropping their contents as entities but not themselves.
    /// If the fill region has no inner blocks (because it is smaller than three blocks in at
    /// least one dimension), acts like replace.
    /// </summary>
    Hollow,
    
    /// <summary>
    /// Replaces only the air blocks in the fill region with the specified block.
    /// </summary>
    Keep,
    
    /// <summary>
    /// Replaces only the blocks on the outer edge of the fill region with the specified block.
    /// Inner blocks are not affected. If the fill region has no inner blocks (because it is
    /// smaller than three blocks in at least one dimension), acts like replace.
    /// </summary>
    Outline,
    
    /// <summary>
    /// Replaces all blocks (including air) in the fill region with the specified block, without
    /// dropping blocks or block contents as entities. Optionally, instead of specifying a data
    /// tag for the replacing block, a block predicate may be specified to limit which
    /// blocks are replaced.
    /// </summary>
    Replace
}