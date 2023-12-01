using System.Text;

namespace Lilypad; 

public class BlockState : Dictionary<string, object?> {
    public new BlockState Add(string key, object? value) {
        this[key] = value;
        return this;
    }
    
    public string Serialize() {
        if (Count == 0) {
            return string.Empty;
        }
        
        var builder = new StringBuilder();
        builder.Append('[');
        var first = true;
        foreach (var (key, value) in this) {
            if (value == null) continue;

            if (!first) {
                builder.Append(',');
            }

            first = false;
            builder.Append(key);
            builder.Append('=');
            builder.Append(Json.Serialize(value));
        }
        builder.Append(']');
        
        return builder.ToString();
    }

    public override string ToString() {
        return Serialize();
    }
}