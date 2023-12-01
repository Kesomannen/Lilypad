namespace Lilypad;

public class IntRange : Range<int?> {
    public IntRange() { }
    public IntRange(int? min, int? max) : base(min, max) { }

    public static implicit operator IntRange(int? value) => new(value, value);
    public static implicit operator IntRange((int? min, int? max) value) => new(value.min, value.max);
    public static implicit operator IntRange(string input) => Parse(input);

    public static IntRange Parse(string input) => Parse<IntRange>(input, Parse);

    static bool Parse(string? input, out int? value) {
        if (int.TryParse(input, out var result)) {
            value = result;
            return true;
        }
        
        value = null;
        return false;
    }
}