namespace Lilypad.Text; 

public struct RawTextContent : ITextContent {
    public string Text;

    public RawTextContent(string text) {
        Text = text;
    }
    
    public string Name => "text";
    public object Value => Text;
}