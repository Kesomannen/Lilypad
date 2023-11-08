namespace Lilypad; 

public struct Vector3 {
    public VectorComponent X, Y, Z;
    
    public Space Space {
        set => X.Space = Y.Space = Z.Space = value;
    }

    public VectorComponent Get(Axis axis) {
        return axis switch {
            Axis.X => X,
            Axis.Y => Y,
            Axis.Z => Z,
            _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
        };
    }
    
    public override string ToString() {
        return $"{X} {Y} {Z}";
    }

    public static bool TryParse(string str, out Vector3 vector3) {
        vector3 = default;
        
        var components = str.Split(' ');
        if (components.Length != 3) {
            return false;
        }

        if (!VectorComponent.TryParse(components[0], out vector3.X)) return false;
        if (!VectorComponent.TryParse(components[1], out vector3.Y)) return false;
        if (!VectorComponent.TryParse(components[2], out vector3.Z)) return false;
        return true;
    }
    
    public static implicit operator Vector3((double, double, double) tuple) => new() {
        X = tuple.Item1, Y = tuple.Item2, Z = tuple.Item3
    };

    public static implicit operator Vector3(string str) {
        if (!TryParse(str, out var coordinate)) {
            throw new FormatException($"'{str}' is not a valid vector3.");
        }
        return coordinate;
    }
    
    public static readonly Vector3 Zero = (0, 0, 0);
    public static readonly Vector3 Here = (0d, 0d, 0d).AsRelative();
}

public static class CoordinateExtensions {
    public static Vector3 AsLocal(this (double, double, double) tuple) {
        Vector3 vector3 = tuple;
        vector3.Space = Space.Local;
        return vector3;
    }
    
    public static Vector3 AsRelative(this (double, double, double) tuple) {
        Vector3 vector3 = tuple;
        vector3.Space = Space.Relative;
        return vector3;
    }
    
    public static Vector3 AsWorld(this (double, double, double) tuple) {
        Vector3 vector3 = tuple;
        vector3.Space = Space.World;
        return vector3;
    }
}

public enum Space {
    World,
    Local,
    Relative
}

public enum Axis {
    X,
    Y,
    Z
}

public enum Anchor {
    Feet,
    Eyes
}