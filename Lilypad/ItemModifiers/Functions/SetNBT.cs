namespace Lilypad.ItemModifiers; 

/// <summary>
/// Adds or changes NBT data of the item. 
/// </summary>
public class SetNBT : ItemFunction {
    /// <summary>
    /// The NBT to merge onto the item's <c>tag</c> tag.
    /// </summary>
    public NBT Tag;

    public override string FunctionName => "set_nbt";

    public SetNBT(NBT tag) {
        Tag = tag;
    }
}