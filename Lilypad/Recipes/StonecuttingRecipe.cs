namespace Lilypad.Recipes; 

public class StonecuttingRecipe : RecipeData {
    public List<RecipeItem> Ingredients = new();
    public EnumReference<Item> Result;
    public int Count;

    public StonecuttingRecipe(EnumReference<Item> result, int count, params RecipeItem[] ingredients) {
        Result = result;
        Count = count;
        Ingredients.AddRange(ingredients);
    }
}