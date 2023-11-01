namespace Lilypad.Predicates; 

public class BlockProperties {
    internal Dictionary<string, object> Properties { get; } = new();
    
    BlockProperties Add(string key, object value) {
        Properties.Add(key, value);
        return this;
    }
}