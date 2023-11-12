namespace Lilypad; 

public class ItemModifier : Resource {
    public List<ItemFunction> Functions = new();
    public List<Predicate>? Predicates;
    
    internal ItemModifier(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) { }
}