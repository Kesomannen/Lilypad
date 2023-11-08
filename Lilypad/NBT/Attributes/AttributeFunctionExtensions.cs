namespace Lilypad; 

public static class AttributeFunctionExtensions {
    public static AttributeCommand Attribute(this Function function, Argument<Selector> selector, string attribute) {
        return new AttributeCommand(function, selector, attribute);
    }
    
    public static AttributeCommand Attribute(this Function function, string attribute) {
        return function.Attribute("@s", attribute);
    }
}