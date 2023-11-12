namespace Lilypad; 

/// <summary>
/// Loot entry that generates block-specific drops. 
/// </summary>
public class DynamicEntry : LootEntry {
    public string Name;

    public DynamicEntry(string name) {
        Name = name;
    }
}