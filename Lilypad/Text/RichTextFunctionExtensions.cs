namespace Lilypad.Text; 

public static class RichTextFunctionExtensions {
    public static Function Tellraw(this Function function, Argument<Selector> selector, RichText message) {
        return function.Add($"tellraw {selector} {message}");
    }
    
    public static Function Tellraw(this Function function, RichText message) {
        return function.Tellraw(Selector.Self, message);
    }
    
    public static Function ClearTitle(this Function function, Argument<Selector> selector) {
        return function.Add($"title {selector} clear");
    }
    
    public static Function ResetTitle(this Function function, Argument<Selector> selector) {
        return function.Add($"title {selector} reset");
    }

    public static Function Title(this Function function, Argument<Selector> selector, EnumReference<TitleType> type, RichText message) {
        return function.Add($"title {selector} {type} {message}");
    }
    
    public static Function Title(this Function function, Argument<Selector> selector, RichText title, RichText subtitle) {
        function.Title(selector, TitleType.Title, title);
        return function.Title(selector, TitleType.Subtitle, subtitle);
    }
    
    public static Function TitleTimes(this Function function, Argument<Selector> selector, float fadeIn, float stay, float fadeOut) {
        return function.Add($"title {selector} times {fadeIn * 20:0} {stay * 20:0} {fadeOut * 20:0}");
    }
}

public enum TitleType {
    Title,
    Subtitle,
    Actionbar
}