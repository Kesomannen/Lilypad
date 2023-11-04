namespace Lilypad.Text; 

public static class RichTextFunctionExtensions {
    public static Function Tellraw(this Function function, Argument<Selector> selector, RichText message) {
        return function.Add($"tellraw {selector} {message}");
    }
    
    public static Function Tellraw(this Function function, RichText message) {
        return function.Tellraw(Selector.Self, message);
    }
}