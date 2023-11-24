namespace Lilypad.ItemModifiers; 

/// <summary>
/// Limits the count of every item stack.
/// </summary>
public class LimitCount : ItemFunction {
    /// <summary>
    /// Limits the count between a minimum and maximum value.
    /// </summary>
    public Range<NumberProvider> Limit;
    
    public LimitCount(Range<NumberProvider> limit) {
        Limit = limit;
    }
}