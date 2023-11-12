using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// Loot entry that drops a single item stack.
/// </summary>
public class ItemEntry : LootEntry {
    /// <summary>
    /// The item to be generated.
    /// </summary>
    [JsonProperty("name")]
    public EnumReference<Item> Item;
}