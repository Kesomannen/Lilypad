namespace Lilypad.Recipes; 

public class SmithingRecipe : RecipeData {
    public RecipeItem Base;
    public RecipeItem Addition;
    public RecipeItem Result;
    public RecipeItem Template;
    
    public SmithingRecipe(RecipeItem baseItem, RecipeItem addition, RecipeItem result, RecipeItem template) {
        Base = baseItem;
        Addition = addition;
        Result = result;
        Template = template;
    }
}