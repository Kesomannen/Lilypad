using Newtonsoft.Json.Linq;

namespace Lilypad; 

internal static class Transpiler {
    static Datapack _datapack = null!;
    static string _path = null!;
    
    public static void Transpile(Datapack datapack, string path) {
        _datapack = datapack;
        _path = path;
        
        if (Directory.Exists(_path)) {
            Directory.Delete(_path, true);
        }
        Directory.CreateDirectory(_path);
        
        WriteMetadata();
        
        WriteResources(_datapack.Functions, "functions", "mcfunction");
        WriteResourcesJson(_datapack.Predicates, "predicates");
        WriteResourcesJson(_datapack.FunctionTags, "tags/functions");
        
        Console.WriteLine($"Transpiled to {_path} successfully");
    }

    static void WriteMetadata() {
        var pack = new JObject {
            ["pack"] = new JObject {
                ["pack_format"] = _datapack.PackFormat,
                ["description"] = _datapack.Description
            }
        };
        
        Write(Json.Serialize(pack), "pack.mcmeta");
    }
    
    static void WriteResourcesJson<T>(IEnumerable<T> resources, string path) where T : Resource {
        WriteResources(resources, path, "json", Json.Serialize);
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
        return Path.Combine(_path, Path.Combine(path));
    }
}