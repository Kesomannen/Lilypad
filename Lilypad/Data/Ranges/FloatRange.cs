namespace Lilypad;

public class FloatRange : Range<float?> {
    public FloatRange() { }
    public FloatRange(float? min, float? max) : base(min, max) { }

    public static implicit operator FloatRange(float? value) => new(value, value);
    public static implicit operator FloatRange((float? min, float? max) value) => new(value.min, value.max);
    public static implicit operator FloatRange(string input) => Parse(input);

    public static FloatRange Parse(string input) => Parse<FloatRange>(input, Parse);

    static bool Parse(string? input, out float? value) {
        if (float.TryParse(input, out var result)) {
            value = result;
            return true;
        }
        
        value = null;
        return false;
    }
}