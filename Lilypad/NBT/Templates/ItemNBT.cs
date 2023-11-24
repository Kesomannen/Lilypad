using Lilypad.Text;

namespace Lilypad; 

public class ItemNBT : NBTTemplate {
    public EnumReference<Item> Item;
    public sbyte Count = 1;

    public bool? Unbreakable;
    public HideFlags? HideFlags;
    public List<(EnumReference<Enchantment>? Enchantment, byte? Level)>? Enchantments;
    public List<AttributeModifierNBT>? AttributeModifiers;
    
    public JsonText? Name;
    public List<JsonText>? Lore;

    public NBT? Tag;

    public static List<(EnumReference<Enchantment>? Enchantment, byte? Level)> VisualOnlyEnchants { get; } = new() { (null, null) };

    public ItemNBT(EnumReference<Item> item) {
        Item = item;
    }

    public override NBT ToNBT() {
        return ToItemNBT();
    }

    public NBT ToItemNBT() {
        return new NBT {
            ["id"] = Item,
            ["Count"] = Count
        }.Add(GetTag());
    }

    public NBT ToEntityNBT() {
        return new NBT {
            ["Item"] = new NBT {
                ["id"] = Item,
                ["Count"] = Count,
                ["tag"] = GetTag()
            }
        };
    }

    NBT GetTag() {
        return new NBT {
            ["Unbreakable"] = Unbreakable,
            ["HideFlags"] = HideFlags,
            ["Enchantments"] = Enchantments?.Select(t => new NBT {
                ["id"] = t.Enchantment,
                ["lvl"] = t.Level
            }),
            ["AttributeModifiers"] = AttributeModifiers,
            ["display"] = new NBT {
                ["Name"] = Name,
                ["Lore"] = Lore
            }
        }.Add(Tag);
    }
}