using Lilypad;
using Newtonsoft.Json;

namespace Lilypad; 

public class ItemConditions {
    public Range<int>? Count;
    public Range<int>? Durability;
    public List<EnchantmentPredicate>? Enchantments;
    public List<EnchantmentPredicate>? StoredEnchantments;
    public List<EnumReference<Item>>? Items;
    public NBTCompound? Nbt;
    //public Reference<Potion> Potion;
    public Reference<Tag<Item>>? Tag;
}

public struct EnchantmentPredicate {
    [JsonProperty("enchantment")]
    public EnumReference<Enchantment> Type;
    public Range<int> Level;
}