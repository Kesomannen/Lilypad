using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Lilypad; 

public class ResourceCollection<T> : IEnumerable<T> where T : Resource {
    readonly Datapack _datapack;
    readonly List<T> _values = new();
    
    public IReadOnlyList<T> Values => _values;
    
    internal ResourceCollection(Datapack datapack) {
        _datapack = datapack;
    }

    public T Create(string? name = null, string? @namespace = null) {
        name ??= Names.Get<T>();
        @namespace ??= _datapack.DefaultNamespace;

        var instance = Activator.CreateInstance(
            typeof(T),
            BindingFlags.NonPublic | BindingFlags.Instance, 
            null,
            new object[] { name, @namespace, _datapack }, 
            null
        );
        if (instance is not T resource) {
            throw new Exception($"Failed to create resource of type {typeof(T)}");
        }
        
        _values.Add(resource);
        return resource;
    }
    
    public bool TryGet(string name, [NotNullWhen(true)] out T? value) {
        return (value = Get(name)) is not null;
    }
    
    public T? Get(string name) {
        return _values.FirstOrDefault(x => x.Name == name);
    }
    
    public T GetOrCreate(string name, string? @namespace = null) {
        return TryGet(name, out var value) ? value : Create(name, @namespace);
    }
    
    public bool Remove(T value) {
        return _values.Remove(value);
    }

    public IEnumerator<T> GetEnumerator() => _values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public static class ResourceCollectionExtensions {
    public static Function Create(
        this ResourceCollection<Function> collection,
        string? name,
        Action<Function> build,
        string? @namespace = null
    ) {
        var function = collection.Create(name, @namespace);
        build(function);
        return function;
    }
    
    public static Function Create(
        this ResourceCollection<Function> collection,
        Action<Function> build,
        string? @namespace = null
    ) {
        return collection.Create(null, build, @namespace);
    }

    public static DataResource<T> Add<T>(
        this ResourceCollection<DataResource<T>> collection, 
        T data, 
        string? name = null, 
        string? @namespace = null
    ) {
        var resource = collection.Create(name, @namespace);
        resource.Data = data;
        return resource;
    }
}