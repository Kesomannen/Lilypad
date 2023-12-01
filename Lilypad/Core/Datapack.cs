using Lilypad.Recipes;
using Lilypad.Helpers;
using Lilypad.ItemModifiers;
using Lilypad.Text;

namespace Lilypad;

/// <summary>
/// Represents a datapack.
/// </summary>
public class Datapack {
    public DatapackMetadata Metadata { get; set; } = new();

    /// <inheritdoc cref="PackInformation.Description"/>
    public JsonText Description {
        get => Metadata.Pack.Description;
        set => Metadata.Pack.Description = value;
    }
    
    /// <inheritdoc cref="PackInformation.PackFormat"/>
    public int PackFormat {
        get => Metadata.Pack.PackFormat;
        set => Metadata.Pack.PackFormat = value;
    }

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
    public ResourceCollection<PredicateResource> Predicates { get; }

    /// <summary>
    /// The function tags in this datapack.
    /// </summary>
    public ResourceCollection<Tag<Function>> FunctionTags { get; }
    
    /// <summary>
    /// The item tags in this datapack.
    /// </summary>
    public ResourceCollection<Tag<Item>> ItemTags { get; }
    
    /// <summary>
    /// The block tags in this datapack.
    /// </summary>
    public ResourceCollection<Tag<Block>> BlockTags { get; }
    
    /// <summary>
    /// The entity tags in this datapack.
    /// </summary>
    public ResourceCollection<Tag<Entity>> EntityTags { get; }
    
    /// <summary>
    /// The fluid tags in this datapack.
    /// </summary>
    public ResourceCollection<Tag<Fluid>> FluidTags { get; }
    
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
        Predicates = new ResourceCollection<PredicateResource>(this);
        
        FunctionTags = new ResourceCollection<Tag<Function>>(this);
        ItemTags = new ResourceCollection<Tag<Item>>(this);
        BlockTags = new ResourceCollection<Tag<Block>>(this);
        EntityTags = new ResourceCollection<Tag<Entity>>(this);
        FluidTags = new ResourceCollection<Tag<Fluid>>(this);
        
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