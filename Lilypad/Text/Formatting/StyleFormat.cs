namespace Lilypad.Text; 

public struct StyleFormat : ITextFormat {
    public EnumReference<TextStyle> Style;
    public bool Active;
    
    public StyleFormat(EnumReference<TextStyle> style, bool active = true) {
        Style = style;
        Active = active;
    }
    
    public string Name => Style.ToString();
    public object Value => Active;
}