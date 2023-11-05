using Lilypad.Advancements;
using Lilypad.ItemModifiers;
using Lilypad.Loot;
using Lilypad.Predicates;
using Lilypad.Recipes;
using Lilypad.Scoreboards;

namespace Lilypad;

public class Datapack {
    public int PackFormat { get; set; } = 18;
    public string Description { get; set; } = string.Empty;

    public ResourceCollection<Function> Functions { get; }
    public ResourceCollection<Advancement> Advancements { get; }
    public ResourceCollection<Recipe> Recipes { get; }
    public ResourceCollection<LootTable> LootTables { get; }
    public ResourceCollection<ItemModifier> ItemModifiers { get; }
    
    public ResourceCollection<DataResource<Predicate>> Predicates { get; }

    public ResourceCollection<Tag<Function>> FunctionTags { get; }
    public ResourceCollection<Tag<Item>> ItemTags { get; }
    
    public ResourceCollection<Objective> Objectives { get; }
    
    public Datapack() {
        Functions = new ResourceCollection<Function>(this);
        Advancements = new ResourceCollection<Advancement>(this);
        Recipes = new ResourceCollection<Recipe>(this);
        LootTables = new ResourceCollection<LootTable>(this);
        ItemModifiers = new ResourceCollection<ItemModifier>(this);
        
        Predicates = new ResourceCollection<DataResource<Predicate>>(this);
        
        FunctionTags = new ResourceCollection<Tag<Function>>(this);
        ItemTags = new ResourceCollection<Tag<Item>>(this);
        
        Objectives = new ResourceCollection<Objective>(this);
    }
    
    public void Transpile(in TranspilationOptions options) {
        Transpiler.Transpile(this, options);
    }
    
    public void Transpile(string path) {
        Transpile(new TranspilationOptions(path));
    }
}