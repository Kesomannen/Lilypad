namespace Lilypad.Recipes; 

public class Recipe : Resource, ISerializeInner {
    public RecipeData? Data { get; private set; }
    
    internal Recipe(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) { }
    
    public Recipe SetData(RecipeData data) {
        Data = data;
        return this;
    }
    
    object? ISerializeInner.SerializedData => Data;
}