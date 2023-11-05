using Lilypad.Extensions;

namespace Lilypad.Advancements; 

public class AdvancementCommand : CommandGenerator<AdvancementCommand> {
    readonly Selector _selector;
    readonly EnumReference<AdvancementAction> _action;

    protected override string BaseCommand => $"advancement {_action} {_selector}";
    
    public AdvancementCommand(
        Function function, 
        Selector selector,
        EnumReference<AdvancementAction> action
    ) : base(function) {
        _selector = selector;
        _action = action;
    }

    public AdvancementCommand Everything() => AddLine("everything");
    
    public AdvancementCommand Only(Advancement advancement, string? criterion = null) {
        return AddLine($"only {advancement} {criterion.ToStringOrEmpty()}");
    }
    
    public AdvancementCommand From(Advancement advancement) => AddLine($"from {advancement}");
    public AdvancementCommand Through(Advancement advancement) => AddLine($"through {advancement}");
    public AdvancementCommand Until(Advancement advancement) => AddLine($"until {advancement}");
}

public enum AdvancementAction {
    Grant,
    Revoke
}