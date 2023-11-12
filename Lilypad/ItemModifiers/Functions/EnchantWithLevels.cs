namespace Lilypad; 

/// <summary>
/// Enchants the item.
/// </summary>
/// <remarks>A book converts to an enchanted book when enchanted.</remarks>
public class EnchantWithLevels : ItemFunction {
    /// <summary>
    /// Whether treasure enchantments are allowed.
    /// </summary>
    public bool Treasure;
    
    /// <summary>
    /// Roughly equivalent to using an enchantment table at that level.
    /// </summary>
    public NumberProvider Levels = 1;
}