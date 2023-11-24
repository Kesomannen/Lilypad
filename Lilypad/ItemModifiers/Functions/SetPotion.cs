using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// Sets the <c>Potion</c> tag of an item. 
/// </summary>
public class SetPotion : NBT {
    [JsonProperty("id")]
    public EnumReference<Potion> Potion;
}