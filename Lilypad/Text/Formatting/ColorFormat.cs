namespace Lilypad.Text; 

public struct ColorFormat : ITextFormat {
    public EnumReference<TextColor> Color;
    
    public string Name => "color";
    public object Value => Color.ToString();
}