using Lilypad.Text;

namespace Lilypad.ItemModifiers; 

/// <summary>
/// Adds or changes the item's custom name.
/// </summary>
public class SetName : ItemFunction {
    /// <summary>
    /// The name to set.
    /// </summary>
    public JsonText Name;
    
    /// <summary>
    /// Specifies the entity to act as @s when referenced in <see cref="Name"/>.
    /// </summary>
    public EnumReference<LootContextEntity> Entity;

    public SetName(JsonText name) {
        Name = name;
    }
}