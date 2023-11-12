namespace Lilypad; 

/// <summary>
/// Copies block state properties provided by loot context to the item's <c>BlockStateTag</c> tag. 
/// </summary>
public class CopyState : ItemFunction {
    public EnumReference<Block> Block;
    public List<string> Properties = new();
}