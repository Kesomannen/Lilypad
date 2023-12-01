namespace Lilypad.Recipes; 

public class BlastingRecipe : CookingRecipe { 
    public BlastingRecipe(string result, double experience, params RecipeItem[] ingredients) 
        : base(result, experience, ingredients) { }
}