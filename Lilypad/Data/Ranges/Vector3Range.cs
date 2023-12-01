namespace Lilypad;

public class Vector3Range : Range<Vector3?> {
    public Vector3Range() { }
    public Vector3Range(Vector3? min, Vector3? max) : base(min, max) { }

    public static implicit operator Vector3Range(Vector3? value) => new(value, value);
    public static implicit operator Vector3Range((Vector3? min, Vector3? max) value) => new(value.min, value.max);
    public static implicit operator Vector3Range(string input) => Parse(input);

    public static Vector3Range Parse(string input) => Parse<Vector3Range>(input, Parse);

    static bool Parse(string? input, out Vector3? value) {
        if (Vector3.TryParse(input, out var result)) {
            value = result;
            return true;
        }
        
        value = null;
        return false;
    }
}