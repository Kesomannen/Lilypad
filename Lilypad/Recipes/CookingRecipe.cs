using Lilypad.Extensions;
using Newtonsoft.Json;

namespace Lilypad.Recipes; 

public abstract class CookingRecipe : RecipeData {
    public List<RecipeItem> Ingredients = new();
    public EnumReference<Item> Result;
    public double Experience;
    [JsonIgnore]
    public float? CookingTime;

    [JsonProperty("cookingtime")]
    int? CookingTimeTicks => CookingTime?.ToTicks();
    
    public CookingRecipe(string result, double experience, params RecipeItem[] ingredients) {
        Result = result;
        Experience = experience;
        Ingredients.AddRange(ingredients);
    }
}