namespace Lilypad; 

public static class AdvancementFunctionExtensions {
    public static AdvancementCommand Advancement(
        this Function function, 
        Selector selector,
        EnumReference<AdvancementAction> action
    ) => new(function, selector, action);
    
    public static AdvancementCommand Advancement(
        this Function function, 
        EnumReference<AdvancementAction> action
    ) => new(function, Selector.Self, action);
}