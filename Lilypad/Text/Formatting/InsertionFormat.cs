namespace Lilypad.Text; 

public struct InsertionFormat : ITextFormat {
    public string Text;
    
    public string Name => "insert";
    public object Value => Text;
}