using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Lilypad.Helpers;

namespace Lilypad; 

/// <summary>
/// Represents a directory of resources in a <see cref="Datapack"/>.
/// The members can be in different namespaces, but must be the same type (or a subclass) of <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of resources in this collection. Must be a subclass of <see cref="Resource"/>.</typeparam>
public class ResourceCollection<T> : IEnumerable<T> where T : Resource {
    readonly Datapack _datapack;
    readonly List<T> _members = new();
    
    /// <summary>
    /// Resources in this collection.
    /// </summary>
    public IReadOnlyList<T> Members => _members;
    
    internal ResourceCollection(Datapack datapack) {
        _datapack = datapack;
    }

    /// <summary>
    /// Creates and adds a new resource to this collection.
    /// </summary>
    /// <param name="name">Must be unique within the namespace. Defaults to an auto-generated name.</param>
    /// <param name="namespace">Defaults to the datapack's default namespace.</param>
    /// <returns>The created resource.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <typeparamref name="T"/> is abstract.
    /// Commonly happens with <see cref="Predicate"/>,
    /// where you should use <see cref="ResourceCollectionExtensions.Add{T}"/> instead,
    /// which wrappes the predicate in a <see cref="DataResource{T}"/> before adding.
    /// </exception>
    /// <exception cref="Exception">Thrown if the resource could not be created for an unknown reason.</exception>
    public T Create(string? name = null, string? @namespace = null) {
        if (typeof(T).IsAbstract) {
            throw new InvalidOperationException($"Cannot create resource of abstract type {typeof(T)}");
        }
        
        name ??= Names.Get<T>();
        @namespace ??= _datapack.DefaultNamespace;
        
        Assert.IsFalse(_members.Any(value => value.Name == name), $"Resource with name {name} already exists in namespace {@namespace}");
        
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
        
        _members.Add(resource);
        return resource;
    }
    
    /// <summary>
    /// Tries to get a resource with the given name.
    /// </summary>
    public bool TryGet(string name, [NotNullWhen(true)] out T? value) {
        return (value = Get(name)) is not null;
    }
    
    /// <summary>
    /// Gets a resource with the given name, or null if it doesn't exist.
    /// </summary>
    public T? Get(string name) {
        return _members.FirstOrDefault(x => x.Name == name);
    }
    
    /// <summary>
    /// Gets a resource or creates it if it doesn't exist.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    /// <param name="namespace">
    /// Defaults to the datapack's default namespace.
    /// The resource isn't required to be in this namespace, but will be created here if needed.
    /// </param>
    public T GetOrCreate(string name, string? @namespace = null) {
        return TryGet(name, out var value) ? value : Create(name, @namespace);
    }
    
    /// <summary>
    /// Removes a resource from this collection.
    /// </summary>
    /// <returns>True if the resource was found and removed.</returns>
    public bool Remove(T value) {
        return _members.Remove(value);
    }

    public IEnumerator<T> GetEnumerator() => _members.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public static class ResourceCollectionExtensions {
    /// <param name="build">Is immediately called on the created function.</param>
    /// <inheritdoc cref="ResourceCollection{T}.Create"/>
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
    
    /// <inheritdoc cref="Create(ResourceCollection{Function},string,System.Action{Function},string)"/>
    public static Function Create(
        this ResourceCollection<Function> collection,
        Action<Function> build,
        string? @namespace = null
    ) {
        return collection.Create(null, build, @namespace);
    }

    /// <summary>
    /// Wrappes data in a <see cref="DataResource{T}"/> and adds it to the collection.
    /// Use instead of <see cref="ResourceCollection{T}.Create"/> when the collection is a <see cref="DataResource{T}"/>.
    /// </summary>
    /// <param name="name">Must be unique within the namespace. Defaults to an auto-generated name.</param>
    /// <param name="namespace">Defaults to the datapack's default namespace.</param>
    /// <returns>The newly created <see cref="DataResource{T}"/>.</returns>
    /// <exception cref="Exception">Thrown if the resource could not be created for an unknown reason.</exception>
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