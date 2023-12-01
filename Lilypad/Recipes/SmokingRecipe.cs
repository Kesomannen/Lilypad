namespace Lilypad.Recipes; 

public class SmokingRecipe : CookingRecipe { 
    public SmokingRecipe(string result, double experience, params RecipeItem[] ingredients) 
        : base(result, experience, ingredients) { }
}