using Lilypad.Helpers;
using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// Base class for members in a datapack with a resource location,
/// such as functions and advancements.
/// </summary>
public abstract class Resource {
    /// <summary>
    /// The name of this resource, should be in snake_case. Must be unique within the namespace.
    /// Forward slashes are turned into directories when transpiled.
    /// </summary>
    /// <remarks>Can include: <list type="bullet">
    /// <item>Lowercase letters <c>(a-z)</c></item>
    /// <item>Numbers <c>(0-9)</c></item>
    /// <item>Underscores <c>(_)</c></item>
    /// <item>Hyphens <c>(-)</c></item>
    /// <item>Dots <c>(.)</c></item>
    /// </list></remarks>
    [JsonIgnore]
    public string Name { get; }
    
    /// <summary>
    /// The namespace of this resource, should be in snake_case.
    /// </summary>
    /// <inheritdoc cref="Name"/>
    [JsonIgnore]
    public string Namespace { get; }

    /// <summary>
    /// Resource location. Also known as namespaced ID or resource identifier.
    /// </summary>
    [JsonIgnore]
    public virtual string Location => $"{Namespace}:{Name}";
    
    /// <summary>
    /// The datapack this resource belongs to.
    /// </summary>
    [JsonIgnore]
    public Datapack Datapack { get; }

    protected Resource(string name, string @namespace, Datapack datapack) {
        Assert.NotNull(datapack, nameof(datapack));
        
        Assert.IsTrue(ValidateName(name), $"Invalid resource name: {name}. Names must only contain lowercase letters, numbers, underscores, hyphens, periods and slashes.");
        Assert.IsTrue(ValidateNamespace(@namespace), $"Invalid resource namespace: {@namespace}. Namespaces must only contain lowercase letters, numbers, underscores, hyphens and periods.");

        Name = name;
        Namespace = @namespace;
        Datapack = datapack;
    }

    static bool ValidateName(string name) {
        return name.All(c => char.IsLetterOrDigit(c) || c is '_' or '-' or '.' or '/');
    }

    static bool ValidateNamespace(string @namespace) {
        return @namespace.All(c => char.IsLetterOrDigit(c) || c is '_' or '-' or '.');
    }
    
    /// <returns>The resource location of this resource.</returns>
    public override string ToString() => Location;
}