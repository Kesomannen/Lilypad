namespace Lilypad.Text; 

public struct TextTag {
    public string Name;
    public object Value;
    
    public TextTag(string name, object value) {
        Name = name;
        Value = value;
    }
    
    public static implicit operator TextTag((string name, object value) tuple) {
        return new TextTag(tuple.name, tuple.value);
    }
}