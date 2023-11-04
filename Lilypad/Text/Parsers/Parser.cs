namespace Lilypad.Text; 

public abstract class Parser {
    protected static void AssertArgumentCount(string[] arguments, int count) {
        if (arguments.Length != count) {
            throw new ArgumentException($"Expected {count} arguments, got {arguments.Length}.");
        }
    }
    
    protected static IEnumerable<TextTag> GetState(params TextTag[] tags) => tags;
}