using Lilypad.Recipes;
using Lilypad;
using Lilypad.Helpers;
using Lilypad.ItemModifiers;
using Lilypad.Text;

namespace Lilypad;

/// <summary>
/// Represents a datapack.
/// </summary>
public class Datapack {
    /// <summary>
    /// Pack version. Defaults to <see cref="NewestPackFormat"/>.
    /// </summary>
    /// <remarks>If this number does not match the current required number, the data pack displays a warning and requires additional confirmation to load the pack.</remarks>
    /// <seealso href="https://minecraft.fandom.com/wiki/Data_pack#Pack_format"/>
    public int PackFormat { get; set; } = NewestPackFormat;
    
    /// <summary>
    /// The description of this datapack.
    /// </summary>
    /// <remarks>
    /// Appears when hovering over the data pack's name in the list given by the <c>/datapack list</c> command, or when viewing the pack in the Create World screen.
    /// </remarks>
    public JsonText Description { get; set; } = JsonText.Empty;

    /// <summary>
    /// The functions in this datapack.
    /// </summary>
    public ResourceCollection<Function> Functions { get; }
    
    /// <summary>
    /// The advancements in this datapack.
    /// </summary>
    public ResourceCollection<Advancement> Advancements { get; }
    
    /// <summary>
    /// The recipes in this datapack.
    /// </summary>
    public ResourceCollection<Recipe> Recipes { get; }
    
    /// <summary>
    /// The loot tables in this datapack.
    /// </summary>
    public ResourceCollection<LootTable> LootTables { get; }
    
    /// <summary>
    /// The item modifiers in this datapack.
    /// </summary>
    public ResourceCollection<ItemModifier> ItemModifiers { get; }
    
    /// <summary>
    /// The predicates in this datapack.
    /// </summary>
    public ResourceCollection<DataResource<Predicate>> Predicates { get; }

    /// <summary>
    /// The function tags in this datapack.
    /// </summary>
    public ResourceCollection<Tag<Function>> FunctionTags { get; }
    
    /// <summary>
    /// The item tags in this datapack.
    /// </summary>
    public ResourceCollection<Tag<Item>> ItemTags { get; }
    
    /// <summary>
    /// A collection of objectives in this datapack.
    /// </summary>
    public ResourceCollection<Objective> Objectives { get; }

    /// <summary>
    /// The namespace where resources are created by default.
    /// </summary>
    public string DefaultNamespace { get; }
    
    /// <summary>
    /// Which logging levels to include in the output.
    /// </summary>
    public LoggingLevel LoggingLevels { get; init; } = LoggingLevel.All;
    
    public const string DefaultDefaultNamespace = "generated";
    public const int NewestPackFormat = 18;

    /// <summary>
    /// Creates a new datapack.
    /// </summary>
    /// <param name="defaultNamespace">The namespace where resources are created by default. Defaults to "generated"</param>
    /// <remarks>Also initializes <see cref="Culture"/>.</remarks>
    public Datapack(string defaultNamespace = DefaultDefaultNamespace) {
        Culture.Initialize();
        DefaultNamespace = defaultNamespace;
        
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
    
    /// <summary>
    /// Transpiles this datapack with the given options.
    /// </summary>
    /// <seealso cref="Transpiler.Transpile"/>
    public void Transpile(in TranspilationOptions options) {
        Transpiler.Transpile(this, options);
    }
    
    /// <summary>
    /// Transpiles this datapack to the given path.
    /// </summary>
    /// <seealso cref="Transpiler.Transpile"/>
    public void Transpile(string path) {
        Transpile(new TranspilationOptions(path));
    }
}