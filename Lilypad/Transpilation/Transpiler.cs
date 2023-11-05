using Newtonsoft.Json.Linq;

namespace Lilypad; 

internal static class Transpiler {
    static Datapack _datapack = null!;
    static TranspilationOptions _options;
    
    static string Root => _options.OutputPath;
    
    public static void Transpile(Datapack datapack, in TranspilationOptions options) {
        _datapack = datapack;
        _options = options;
        
        if (Directory.Exists(Root)) {
            if (options.Overwrite) {
                Console.WriteLine($"Overwriting {Root}");
                Directory.Delete(Root, true);
            } else {
                throw new Exception($"Directory {Root} already exists. Enable overwrite to delete it.");
            }
        }
        Directory.CreateDirectory(Root);
        
        WriteMetadata();
        
        WriteResources(_datapack.Functions, "functions", "mcfunction");
        WriteResourcesJson(_datapack.Advancements, "advancements");
        WriteResourcesJson(_datapack.Recipes, "recipes");
        WriteResourcesJson(_datapack.LootTables, "loot_tables");
        WriteResourcesJson(_datapack.ItemModifiers, "item_modifiers");
        
        WriteResourcesJson(_datapack.Predicates, "predicates");

        WriteResourcesJson(_datapack.FunctionTags, "tags/functions");
        WriteResourcesJson(_datapack.ItemTags, "tags/items");
        
        Console.WriteLine($"Transpiled to {Root} successfully");
    }

    static void WriteMetadata() {
        var pack = new JObject {
            ["pack"] = new JObject {
                ["pack_format"] = _datapack.PackFormat,
                ["description"] = _datapack.Description
            }
        };
        
        File.WriteAllText(GetAbsolute("pack.mcmeta"), Serialize(pack));
    }
    
    static void WriteResourcesJson<T>(IEnumerable<T> resources, string path) where T : Resource {
        WriteResources(resources, path, "json", Serialize);
    }
    
    static void WriteResources<T>(
        IEnumerable<T> resources,
        string path,
        string fileExtension, 
        Func<T, string>? getContent = null
    ) where T : Resource {
        getContent ??= resource => resource.ToString();
        
        foreach (var resource in resources) {
            WriteResource(resource, path, getContent(resource), fileExtension);
        }
    }

    static readonly List<string> _steps = new();
    
    static void WriteResource(Resource resource, string path, string content, string fileExtension) {
        _steps.Clear();
        
        _steps.Add("data");
        _steps.Add(resource.Namespace);
        _steps.AddRange(path.Split("/"));
        _steps.AddRange(resource.Name.Split("/"));

        path = $"{GetAbsolute(_steps.ToArray())}.{fileExtension}";
        var parent = Path.GetDirectoryName(path);
        Directory.CreateDirectory(parent!);
        File.WriteAllText(path, content);
    }
    
    static string GetAbsolute(params string[] path) {
        return Path.Combine(Root, Path.Combine(path));
    }
    
    static string Serialize(object value) {
        return Json.Serialize(value, _options.PrettyPrint);
    }
}