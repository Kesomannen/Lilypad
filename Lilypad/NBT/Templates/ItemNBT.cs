using Lilypad.Text;

namespace Lilypad; 

public class ItemNBT : NBTTemplate {
    public EnumReference<Item> Item;
    public sbyte Count = 1;

    public bool? Unbreakable;
    public HideFlags? HideFlags;
    public List<(EnumReference<Enchantment>?, short?)>? Enchantments;
    public List<AttributeNBT>? AttributeModifiers;
    
    public JsonText? Name;
    public List<JsonText>? Lore;

    public NBT? AdditionalTag;

    public static List<(EnumReference<Enchantment>?, short?)> VisualOnlyEnchants { get; } = new() { (null, null) };

    public ItemNBT(EnumReference<Item> item) {
        Item = item;
    }

    public override NBT ToNBT() {
        return ToItemNBT();
    }

    public NBT ToItemNBT() {
        return new NBT {
            ["id"] = Item,
            ["Count"] = Count,
            ["tag"] = GetTag()
        };
    }
    
    public NBT ToEntityNBT() {
        return new NBT { ["Item"] = ToItemNBT() };
    }

    public NBT GetTag() {
        return new NBT {
            ["Unbreakable"] = Unbreakable,
            ["HideFlags"] = HideFlags,
            ["Enchantments"] = Enchantments?.Select(tuple => new NBT {
                ["id"] = tuple.Item1,
                ["lvl"] = tuple.Item2
            }),
            ["AttributeModifiers"] = AttributeModifiers,
            ["display"] = new NBT {
                ["Name"] = Name,
                ["Lore"] = Lore
            }
        }.Add(AdditionalTag);
    }
}