namespace Lilypad.ItemModifiers; 

/// <summary>
/// Adds or changes NBT data of the item. 
/// </summary>
public class SetNbt : ItemFunction {
    /// <summary>
    /// The NBT to merge onto the item's <c>tag</c> tag.
    /// </summary>
    public NBTCompound Tag;

    public SetNbt(NBTCompound tag) {
        Tag = tag;
    }
}