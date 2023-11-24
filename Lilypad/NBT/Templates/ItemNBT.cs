namespace Lilypad.Templates; 

public class ItemNBT : NBTTemplate {
    public EnumReference<Item> Item;
    public byte Count = 1;

    public bool? Unbreakable;
    public HideFlags? HideFlags;
    public List<(Enchantment, byte)>? Enchantments;

    public NBTCompound? Tag;

    public override NBTCompound ToNBT() {
        return new NBTCompound {
            ["id"] = Item,
            ["Count"] = Count,
            ["Unbreakable"] = Unbreakable,
            ["HideFlage"] = HideFlags,
            ["Enchantments"] = Enchantments?.Select(t => new NBTCompound {
                ["id"] = t.Item1,
                ["lvl"] = t.Item2
            }).ToArray()
        };
    }
}