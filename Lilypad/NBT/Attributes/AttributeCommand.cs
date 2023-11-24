using Lilypad;

namespace Lilypad;

public class AttributeCommand {
    readonly Function _function;
    readonly Argument<Selector> _selector;
    readonly string _attribute;

    public AttributeCommand(Function function, Argument<Selector> selector, string attribute) {
        _function = function;
        _selector = selector;
        _attribute = attribute;
    }

    AttributeCommand AddCommand(string command) {
        _function.Add($"attribute {_selector} {_attribute} {command}");
        return this;
    }

    public AttributeCommand Get(double scale = 1) {
        return AddCommand($"get {scale}");
    }

    public AttributeCommand GetBase(double scale = 1) {
        return AddCommand($"base get {scale}");
    }

    public AttributeCommand GetModifier(Uuid uuid, double scale = 1) {
        return AddCommand($"get {uuid} {scale}");
    }

    public AttributeCommand SetBase(double value) {
        return AddCommand($"base set {value}");
    }

    public AttributeCommand AddModifier(double value, EnumReference<AttributeOperation> operation, Uuid? uuid = null, string? name = null) {
        name ??= _attribute;
        uuid ??= Uuid.New();
        return AddCommand($"modifier add {uuid} \"{name}\" {value} {operation}");
    }

    public AttributeCommand RemoveModifier(Uuid uuid) {
        return AddCommand($"modifier remove {uuid}");
    }
    
}