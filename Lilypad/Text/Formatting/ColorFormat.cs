namespace Lilypad.Text; 

public struct ColorFormat : ITextFormat {
    public EnumReference<TextColor> Color;

    public ColorFormat(EnumReference<TextColor> color) {
        Color = color;
    }
    
    public string Name => "color";
    public object Value => Color.ToString();
}