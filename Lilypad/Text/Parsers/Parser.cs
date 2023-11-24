namespace Lilypad.Text; 

public abstract class Parser {
    protected static void AssertArgumentCount(string[] arguments, int count) {
        if (arguments.Length != count) {
            throw new ArgumentException($"Expected {count} arguments, got {arguments.Length} instead. Arguments: {string.Join(", ", arguments)}");
        }
    }
    
    protected static void AssertArgumentCount(string[] arguments, int min, int max) {
        if (arguments.Length < min || arguments.Length > max) {
            throw new ArgumentException($"Expected between {min} and {max} arguments, got {arguments.Length} instead. Arguments: {string.Join(", ", arguments)}");
        }
    }
    
    protected static IEnumerable<TextTag> GetState(params TextTag[] tags) => tags;
}