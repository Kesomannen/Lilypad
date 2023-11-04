namespace Lilypad.Scoreboards; 

public static class TagFunctionExtensions {
    public static Function AddTag(this Function function, Argument<Selector> target, string tag) {
        return function.Add($"tag {target} add {tag}");
    }
    
    public static Function AddTag(this Function function, string tag) {
        return function.AddTag(Selector.Self, tag);
    }
    
    public static Function RemoveTag(this Function function, Argument<Selector> target, string tag) {
        return function.Add($"tag {target} remove {tag}");
    }
    
    public static Function RemoveTag(this Function function, string tag) {
        return function.RemoveTag(Selector.Self, tag);
    }
}