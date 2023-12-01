using Lilypad.Extensions;

namespace Lilypad.Text; 

/// <summary>
/// Provides function extension methods for JSON text commands.
/// </summary>
public static class JsonTextFunctionExtensions {
    public static Function Tellraw(this Function function, Argument<Selector> selector, JsonText message) {
        return function.Add($"tellraw {selector} {message}");
    }
    
    public static Function Tellraw(this Function function, JsonText message) {
        return function.Tellraw(Selector.Self, message);
    }
    
    public static Function ClearTitle(this Function function, Argument<Selector> selector) {
        return function.Add($"title {selector} clear");
    }
    
    public static Function ResetTitle(this Function function, Argument<Selector> selector) {
        return function.Add($"title {selector} reset");
    }

    public static Function Title(this Function function, Argument<Selector> selector, EnumReference<TitleType> type, JsonText message) {
        return function.Add($"title {selector} {type} {message}");
    }
    
    public static Function Title(this Function function, Argument<Selector> selector, JsonText title, JsonText subtitle) {
        function.Title(selector, TitleType.Title, title);
        return function.Title(selector, TitleType.Subtitle, subtitle);
    }
    
    public static Function TitleTimes(this Function function, Argument<Selector> selector, float fadeIn, float stay, float fadeOut) {
        return function.Add($"title {selector} times {fadeIn.ToTicks()} {stay.ToTicks()} {fadeOut.ToTicks()}");
    }
}

public enum TitleType {
    Title,
    Subtitle,
    Actionbar
}