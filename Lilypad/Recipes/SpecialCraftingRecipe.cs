using Newtonsoft.Json;

namespace Lilypad.Recipes; 

public class SpecialCraftingRecipe : RecipeData {
    [JsonIgnore]
    public EnumReference<SpecialRecipeType> SpecialRecipeType;
    
    public override string Type => $"crafting_special_{SpecialRecipeType}";
    
    public SpecialCraftingRecipe(EnumReference<SpecialRecipeType> type) {
        SpecialRecipeType = type;
    }
}