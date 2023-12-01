namespace Lilypad.Recipes; 

public class ShapelessCraftingRecipe : RecipeData {
    public List<List<RecipeItem>> Ingredients = new();
    public (EnumReference<Item> Item, int? Count) Result;
    
    public override string Type => "crafting_shapeless";
    
    public ShapelessCraftingRecipe(EnumReference<Item> result, int count, params RecipeItem[][] ingredients) {
        if (ingredients.Length is < 1 or > 9) {
            throw new ArgumentException("Shapeless crafting recipe must have between 1 and 9 ingredients");
        }
        foreach (var ingredient in ingredients) {
            Ingredients.Add(ingredient.ToList());
        }
        Result = (result, count);
    }
}