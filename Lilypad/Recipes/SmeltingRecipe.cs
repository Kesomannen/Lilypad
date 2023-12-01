namespace Lilypad.Recipes; 

public class SmeltingRecipe : CookingRecipe { 
    public SmeltingRecipe(string result, double experience, params RecipeItem[] ingredients) 
        : base(result, experience, ingredients) { }
}