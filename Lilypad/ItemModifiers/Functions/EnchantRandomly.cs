namespace Lilypad.ItemModifiers; 

/// <summary>
/// Enchants the item with one randomly-selected enchantment.
/// </summary>
/// <remarks>
/// The power of the enchantment, if applicable, is random.
/// <br/><br/>A book converts to an enchanted book when enchanted.
/// </remarks>
public class EnchantRandomly : ItemFunction {
    /// <summary>
    /// Enchantments to choose from.
    /// </summary>
    /// <remarks>If omitted, all discoverable enchantments (currently only Soul speed and Swift Sneak are undiscoverable) applicable to the item are possible.</remarks>
    public List<EnumReference<Enchantment>>? Enchantments;
}