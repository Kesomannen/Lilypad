namespace Lilypad.ItemModifiers; 

public class ItemModifier : Resource, ISerializeInner {
    public List<ItemFunction> Functions = new();

    internal ItemModifier(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) { }

    public ItemModifier AddFunctions(params ItemFunction[] function) {
        Functions.AddRange(function);
        return this;
    }

    public object? SerializedData => Functions;
}