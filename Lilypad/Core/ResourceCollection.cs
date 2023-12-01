using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Lilypad.Helpers;
using Lilypad.Recipes;

namespace Lilypad; 

/// <summary>
/// Represents a directory of resources in a <see cref="Datapack"/>.
/// The members can be in different namespaces, but must be the same type (or a subclass) of <typeparamref name="T"/>.
/// <br/><br/>You can create new resources in the collection with <see cref="Create"/>.
/// To query existing resources, use <see cref="Get"/>, <see cref="TryGet"/> or <see cref="GetOrCreate"/>.
/// </summary>
/// <remarks>
/// Uses reflection to find the constructor for <typeparamref name="T"/>, since Resource
/// constructors are internal (to ensure they are always in a datapack with a valid name and namespace).
/// </remarks>
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
    /// <exception cref="InvalidOperationException"><typeparamref name="T"/> is abstract.</exception>
    /// <exception cref="Exception">The resource could not be created for an unknown reason.</exception>
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
    /// The search isn't restricted to this namespace.
    /// Although if a new resource is created, it will be placed in this namespace.
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
    /// Adds a predicate to this collection.
    /// </summary>
    /// <param name="name">Must be unique within the namespace. Defaults to an auto-generated name.</param>
    /// <param name="predicate">The predicate to add.</param>
    /// <param name="namespace">Defaults to the datapack's default namespace.</param>
    /// <returns>The predicate, wrapped in a <see cref="PredicateResource"/>.</returns>
    /// <exception cref="Exception">The resource could not be created for an unknown reason.</exception>
    public static PredicateResource Add(
        this ResourceCollection<PredicateResource> collection,
        string? name,
        Predicate predicate,
        string? @namespace = null
    ) {
        var resource = collection.Create(name, @namespace);
        resource.Add(predicate);
        return resource;
    }
    
    /// <inheritdoc cref="Add(ResourceCollection{PredicateResource},string,Predicate,string)"/>
    public static PredicateResource Add(
        this ResourceCollection<PredicateResource> collection,
        Predicate predicate,
        string? @namespace = null
    ) {
        return collection.Add(null, predicate, @namespace);
    }
    
    public static Recipe Add(
        this ResourceCollection<Recipe> collection,
        string? name,
        RecipeData recipeData,
        string? @namespace = null
    ) {
        return collection.Create(name, @namespace).SetData(recipeData);
    }
    
    public static Recipe Add(
        this ResourceCollection<Recipe> collection,
        RecipeData recipeData,
        string? @namespace = null
    ) {
        return collection.Add(null, recipeData, @namespace);
    }
}