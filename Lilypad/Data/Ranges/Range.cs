namespace Lilypad; 

/// <summary>
/// A range of values.
/// </summary>
public abstract class Range<T> {
    /// <summary>
    /// The minimum value, null if there is none.
    /// </summary>
    public T? Min;
    
    /// <summary>
    /// The maximum value, null if there is none.
    /// </summary>
    public T? Max;

    public Range() { }
    
    public Range(T? min, T? max) {
        Min = min;
        Max = max;
    }

    public override string ToString() {
        if (Min is null && Max is null) return "";
        if (Min is null) return $"..{Max}";
        if (Max is null) return $"{Min}..";
        return $"{Min}..{Max}";
    }

    protected static TRange Parse<TRange>(string input, Parser parser) where TRange : Range<T>, new() {
        var range = new TRange();
        if (string.IsNullOrWhiteSpace(input)) {
            return range;
        }
        if (parser(input, out var value)) {
            range.Min = range.Max = value;
            return range;
        }

        var segments = input.Split("..", StringSplitOptions.TrimEntries);
        switch (segments.Length) {
            case 2:
                var firstEmpty = string.IsNullOrWhiteSpace(segments[0]);
                var secondEmpty = string.IsNullOrWhiteSpace(segments[1]);
                if (firstEmpty && secondEmpty) {
                    throw new ArgumentException($"Invalid range: {input}");
                }
                
                if (!firstEmpty) {
                    if (!parser(segments[0], out var min)) {
                        throw new ArgumentException($"Invalid range: {input}");
                    }
                    range.Min = min;
                }
                
                if (!secondEmpty) {
                    if (!parser(segments[1], out var max)) {
                        throw new ArgumentException($"Invalid range: {input}");
                    }
                    range.Max = max;
                }
                return range;
            
            default:
                throw new ArgumentException($"Invalid range: {input}");
        }
    }
    
    protected delegate bool Parser(string? input, out T? value);
}