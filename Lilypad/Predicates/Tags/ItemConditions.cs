using Lilypad;
using Newtonsoft.Json;

namespace Lilypad; 

public class ItemConditions {
    public IntRange? Count;
    public IntRange? Durability;
    public List<EnchantmentPredicate>? Enchantments;
    public List<EnchantmentPredicate>? StoredEnchantments;
    public List<EnumReference<Item>>? Items;
    public NBT? Nbt;
    public EnumReference<Potion>? Potion;
    public Reference<Tag<Item>>? Tag;
}

public struct EnchantmentPredicate {
    [JsonProperty("enchantment")]
    public EnumReference<Enchantment> Type;
    public IntRange Level;
}