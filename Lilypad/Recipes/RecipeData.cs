using Lilypad.Extensions;

namespace Lilypad.Recipes; 

public abstract class RecipeData {
    public string? Group { get; set; }
    
    public virtual string Type => GetType().Name.Replace("Recipe", "").ToSnakeCase();
}

public struct RecipeItem {
    public EnumReference<Item>? Item;
    public Reference<Tag<Item>>? Tag;

    public static implicit operator RecipeItem(string item) => new() { Item = item };
    public static implicit operator RecipeItem(Item item) => new() { Item = item };
    public static implicit operator RecipeItem(EnumReference<Item> item) => new() { Item = item };
    public static implicit operator RecipeItem(Reference<Tag<Item>> tag) => new() { Tag = tag };
}