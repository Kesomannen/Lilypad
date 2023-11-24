using Lilypad.ItemModifiers;

namespace Lilypad; 

public class LootEntry : LootEntryProvider {
    public List<ItemFunction>? Functions;
    
    /// <summary>
    /// Determines how often the this entry is chosen out of all the entries in the <see cref="LootPool"/>.
    /// </summary>
    public int? Weight;
    
    /// <summary>
    /// Modifies <see cref="Weight"/> based on the luck attribute of the <see cref="LootContextEntity.Killer"/>
    /// in the <see cref="LootContext.Entity"/> context, or <see cref="LootContextEntity.This"/> in all other contexts.
    /// </summary>
    public int? Quality;
}