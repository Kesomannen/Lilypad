namespace Lilypad;

public class DoubleRange : Range<double?> {
    public DoubleRange() { }
    public DoubleRange(double? min, double? max) : base(min, max) { }

    public static implicit operator DoubleRange(double? value) => new(value, value);
    public static implicit operator DoubleRange((double? min, double? max) value) => new(value.min, value.max);
    public static implicit operator DoubleRange(string input) => Parse(input);

    public static DoubleRange Parse(string input) => Parse<DoubleRange>(input, Parse);

    static bool Parse(string? input, out double? value) {
        if (double.TryParse(input, out var result)) {
            value = result;
            return true;
        }
        
        value = null;
        return false;
    }
}