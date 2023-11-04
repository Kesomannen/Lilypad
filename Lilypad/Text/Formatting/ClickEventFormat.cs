namespace Lilypad.Text; 

public struct ClickEventFormat : ITextFormat {
    public ClickEvent ClickEvent;
    
    public string Name => "clickEvent";
    public object Value => ClickEvent;
}

public struct ClickEvent {
    public EnumReference<ClickAction> Action;
    public string Value;
}

public enum ClickAction {
    OpenUrl,
    RunCommand,
    SuggestCommand,
    ChangePage,
    CopyToClipboard
}