using Lilypad.Extensions;

namespace Lilypad; 

public class AdvancementCommand {
    readonly Selector _selector;
    readonly EnumReference<AdvancementAction> _action;
    readonly Function _function;
    
    public AdvancementCommand(
        Function function, 
        Selector selector,
        EnumReference<AdvancementAction> action
    ) {
        _function = function;
        _selector = selector;
        _action = action;
    }
    
    AdvancementCommand AddCommand(string command) {
        _function.Add($"advancement {_action} {_selector} {command}");
        return this;
    }

    public AdvancementCommand Everything() {
        return AddCommand("everything");
    }

    public AdvancementCommand Only(Advancement advancement, string? criterion = null) {
        return AddCommand($"only {advancement} {criterion.ToStringOrEmpty()}");
    }
    
    public AdvancementCommand From(Advancement advancement) {
        return AddCommand($"from {advancement}");
    }

    public AdvancementCommand Through(Advancement advancement) {
        return AddCommand($"through {advancement}");
    }

    public AdvancementCommand Until(Advancement advancement) {
        return AddCommand($"until {advancement}");
    }
}

public enum AdvancementAction {
    Grant,
    Revoke
}