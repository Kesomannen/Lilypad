namespace Lilypad; 

/// <summary>
/// Represents a predicate resource (a .json file in the <c>predicates</c> folder).
/// Can include multiple predicates.
/// </summary>
public class PredicateResource : Resource, ISerializeInner {
    public List<Predicate> Predicates = new();
    
    internal PredicateResource(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) { }
    
    public PredicateResource Add(Predicate predicate) {
        Predicates.Add(predicate);
        return this;
    }
    
    public object SerializedData => Predicates;
}