namespace Lilypad.Text; 

public struct RawTextContent : ITextContent {
    public string Text;
    
    public string Name => "text";
    public object Value => Text;
}