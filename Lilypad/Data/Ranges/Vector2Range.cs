namespace Lilypad;

public class Vector2Range : Range<Vector2?> {
    public Vector2Range() { }
    public Vector2Range(Vector2? min, Vector2? max) : base(min, max) { }

    public static implicit operator Vector2Range(Vector2? value) => new(value, value);
    public static implicit operator Vector2Range((Vector2? min, Vector2? max) value) => new(value.min, value.max);
    public static implicit operator Vector2Range(string input) => Parse(input);

    public static Vector2Range Parse(string input) => Parse<Vector2Range>(input, Parse);

    static bool Parse(string? input, out Vector2? value) {
        if (Vector2.TryParse(input, out var result)) {
            value = result;
            return true;
        }
        
        value = null;
        return false;
    }
}