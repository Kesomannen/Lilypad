namespace Lilypad.Text; 

public struct StyleFormat : ITextFormat {
    public EnumReference<TextStyle> Style;
    public bool Active;
    
    public string Name => Style.ToString();
    public object Value => Active;
}