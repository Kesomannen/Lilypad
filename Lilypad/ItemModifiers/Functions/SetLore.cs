using Lilypad.Text;

namespace Lilypad.ItemModifiers; 

/// <summary>
/// Adds or changes the item's lore. 
/// </summary>
public class SetLore : ItemFunction {
    /// <summary>
    /// List of lines to append or replace on the item's lore. 
    /// </summary>
    public List<JsonText> Lore = new();
    
    /// <summary>
    /// Specifies the entity to act as @s when referenced in the JSON text component.
    /// </summary>
    public LootContextEntity Entity;
    public bool? Replace;
}