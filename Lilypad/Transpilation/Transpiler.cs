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
        WriteResourcesJson(_datapack.Predicates, "predicates");
        WriteResourcesJson(_datapack.FunctionTags, "tags/functions");
        
        Console.WriteLine($"Transpiled to {Root} successfully");
    }

    static void WriteMetadata() {
        var pack = new JObject {
            ["pack"] = new JObject {
                ["pack_format"] = _datapack.PackFormat,
                ["description"] = _datapack.Description
            }
        };
        
        Write(Serialize(pack), "pack.mcmeta");
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

    static void WriteResource(Resource resource, string path, string content, string fileExtension) {
        var steps = path.Split("/");
        var dir = OpenDirectory(steps.Prepend(resource.Namespace).Prepend("data").ToArray());
        
        path = Path.Combine(dir, $"{resource.Name}.{fileExtension}");
        Write(content, path);
    }
    
    static void Write(string content, params string[] path) {
        File.WriteAllText(GetPath(path), content);
    }
    
    static string OpenDirectory(params string[] path) {
        var directory = GetPath(path);
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
        }
        return directory;
    }
    
    static string GetPath(params string[] path) {
        return Path.Combine(Root, Path.Combine(path));
    }
    
    static string Serialize(object value) {
        return Json.Serialize(value, _options.PrettyPrint);
    }
}