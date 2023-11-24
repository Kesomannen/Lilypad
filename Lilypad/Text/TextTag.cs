namespace Lilypad.Text; 

/// <summary>
/// A tag in a JSON text component.
/// </summary>
public readonly struct TextTag {
    public readonly string Name;
    public readonly object Value;
    
    public TextTag(string name, object value) {
        Name = name;
        Value = value;
    }
    
    public static implicit operator TextTag((string name, object value) tuple) {
        return new TextTag(tuple.name, tuple.value);
    }
}