namespace Lilypad.Recipes; 

public class ShapedCraftingRecipe : RecipeData {
    public char[] Pattern;
    public Dictionary<char, List<RecipeItem>> Key = new();
    public (EnumReference<Item> Item, int? Count) Result;
    
    public override string Type => "crafting_shaped";
    
    public ShapedCraftingRecipe(char[] pattern, EnumReference<Item> result, int count, params (char, RecipeItem[])[] key) {
        Pattern = pattern;
        if (pattern.Length is not (4 or 9)) {
            throw new ArgumentException("Shaped crafting recipe pattern must be 2x2 or 3x3");
        }
        foreach (var (keyChar, item) in key) {
            Key.Add(keyChar, item.ToList());
        }
        foreach (var c in pattern) {
            if (!Key.ContainsKey(c)) {
                throw new ArgumentException($"Shaped crafting recipe pattern contains key '{c}' which is not defined in the key");
            }
        }
        Result = (result, count);
    }
    
    public ShapedCraftingRecipe(char[] pattern, EnumReference<Item> result, params (char, RecipeItem[])[] key)
        : this(pattern, result, 1, key) { }
}