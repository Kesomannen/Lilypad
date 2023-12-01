namespace Lilypad.Recipes; 

public class CampfireRecipe : CookingRecipe {
    public override string Type => "campfire_cooking";

    public CampfireRecipe(string result, double experience, params RecipeItem[] ingredients) 
        : base(result, experience, ingredients) { }
}