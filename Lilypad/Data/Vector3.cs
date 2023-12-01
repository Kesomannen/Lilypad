namespace Lilypad; 

public struct Vector3 {
    public VectorComponent X, Y, Z;
    
    public Space Space {
        set => X.Space = Y.Space = Z.Space = value;
    }

    public Vector3(double x, double y, double z, Space space = Space.World) {
        X = x;
        Y = y;
        Z = z;
        Space = space;
    }

    public double Magnitude {
        get {
            GetValues(out var x, out var y, out var z);
            return Math.Sqrt(x * x + y * y + z * z);
        }
    }

    public VectorComponent Get(Axis axis) {
        return axis switch {
            Axis.X => X,
            Axis.Y => Y,
            Axis.Z => Z,
            _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
        };
    }

    public Vector3 InSpace(Space space) {
        return new Vector3 {
            X = X,
            Y = Y,
            Z = Z,
            Space = space
        };
    }

    public void GetValues(out double x, out double y, out double z) {
        x = X.Value;
        y = Y.Value;
        z = Z.Value;
    }

    public override string ToString() {
        return $"{X} {Y} {Z}";
    }

    public static bool TryParse(string? str, out Vector3 vector3) {
        vector3 = default;
        if (string.IsNullOrWhiteSpace(str)) {
            return false;
        }
        
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
    
    public static implicit operator Vector3((VectorComponent, VectorComponent, VectorComponent) tuple) => new() {
        X = tuple.Item1, Y = tuple.Item2, Z = tuple.Item3
    };

    public static implicit operator Vector3(string str) {
        if (!TryParse(str, out var coordinate)) {
            throw new FormatException($"'{str}' is not a valid Vector3.");
        }
        return coordinate;
    }
    
    public static readonly Vector3 Zero = (0, 0, 0);
    public static readonly Vector3 One = (1, 1, 1);
    public static readonly Vector3 Here = (0d, 0d, 0d).AsRelative();

    public static readonly Vector3 LocalUp = (0d, 1d, 0d).AsLocal();
    public static readonly Vector3 LocalDown = (0d, -1d, 0d).AsLocal();
    public static readonly Vector3 Forward = (0d, 0d, 1d).AsLocal();
    public static readonly Vector3 Back = (0d, 0d, -1d).AsLocal();
    public static readonly Vector3 Left = (-1d, 0d, 0d).AsLocal();
    public static readonly Vector3 Right = (1d, 0d, 0d).AsLocal();

    public static readonly Vector3 Up = (0, 1, 0);
    public static readonly Vector3 Down = (0, -1, 0);
    public static readonly Vector3 North = (0, 0, -1);
    public static readonly Vector3 South = (0, 0, 1);
    public static readonly Vector3 East = (1, 0, 0);
    public static readonly Vector3 West = (-1, 0, 0);
    
    public static Vector3 operator +(Vector3 a, Vector3 b) => (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3 operator -(Vector3 a, Vector3 b) => (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    
    public static Vector3 operator *(Vector3 a, double b) => (a.X * b, a.Y * b, a.Z * b);
    public static Vector3 operator /(Vector3 a, double b) => (a.X / b, a.Y / b, a.Z / b);
}

public static class Vector3Extensions {
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

    public static Vector3 Splat(this double value) => (value, value, value);
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