using Lilypad.Helpers;
using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// Represents an object in a datapack with a resource location.
/// </summary>
public abstract class Resource {
    /// <summary>
    /// The name of this resource, should be in snake_case. Must be unique within the namespace.
    /// Slashes are converted to directories.
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
    public string Location => GetLocation();
    
    /// <summary>
    /// The datapack this resource belongs to.
    /// </summary>
    [JsonIgnore]
    public Datapack Datapack { get; }

    protected Resource(string name, string @namespace, Datapack datapack) {
        Assert.NotNull(datapack, nameof(datapack));
        
        Assert.IsTrue(ValidateName(name), $"Invalid resource name: {name}. Names must only contain lowercase letters, numbers, underscores, hyphens, periods and slashes.");
        Assert.IsTrue(ValidateNamsepace(@namespace), $"Invalid resource namespace: {@namespace}. Namespaces must only contain lowercase letters, numbers, underscores, hyphens and periods.");

        Name = name;
        Namespace = @namespace;
        Datapack = datapack;
    }
    
    public static bool ValidateName(string name) {
        return name.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == '.' || c == '/');
    }
    
    public static bool ValidateNamsepace(string @namespace) {
        return @namespace.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == '.');
    }
    
    /// <returns>The resource location of this resource.</returns>
    public override string ToString() => Location;

    protected virtual string GetLocation() {
        return $"{Namespace}:{Name}";
    }
}