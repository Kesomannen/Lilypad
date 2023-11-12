using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// Provides another loot table as a loot entry.
/// </summary>
public class LootTableEntry : LootEntry {
    /// <summary>
    /// The loot table to be used.
    /// </summary>
    [JsonProperty("name")]
    public Reference<LootTable> LootTable;
}