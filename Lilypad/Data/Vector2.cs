namespace Lilypad; 

public struct Vector2 {
    public VectorComponent X, Y;
    
    public override string ToString() {
        return $"{X} {Y}";
    }
    
    public static bool TryParse(string str, out Vector2 vector2) {
        vector2 = default;
        var components = str.Split(' ');
        if (components.Length != 2) {
            return false;
        }
        
        if (!VectorComponent.TryParse(components[0], out vector2.X)) return false;
        if (!VectorComponent.TryParse(components[1], out vector2.Y)) return false;
        return true;
    }
    
    public static implicit operator Vector2((double, double) tuple) => new() {
        X = tuple.Item1, Y = tuple.Item2
    };
    
    public static implicit operator Vector2(string str) {
        if (!TryParse(str, out var rotation)) {
            throw new FormatException($"'{str}' is not a valid vector2.");
        }
        return rotation;
    }
}