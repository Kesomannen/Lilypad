using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// Loot entry that generates all item in an item tag, or multiple loot entries. 
/// </summary>
/// <remarks>Each entry generates a single item in the item tag.</remarks>
public class TagEntry : LootEntry {
    /// <summary>
    /// The item tag to query.
    /// </summary>
    [JsonProperty("name")]
    public Reference<Tag<Item>> Tag;
    
    /// <summary>
    /// If set to true, provides one loot entry per item in the tag with the same weight and quality, and each entry generates one item.
    /// <br/><br/>If false, provides a single loot entry that generates all items (each with count of 1) in the tag.
    /// </summary>
    public bool Expand;
}