using System.Globalization;

namespace Lilypad; 

public struct Coordinate {
    public double X;
    public Space XSpace;
    
    public double Y;
    public Space YSpace;
    
    public double Z;
    public Space ZSpace;
    
    public Space Space {
        set {
            XSpace = value;
            YSpace = value;
            ZSpace = value;
        }
    }
    
    public override string ToString() {
        return $"{Format(X, XSpace)} {Format(Y, YSpace)} {Format(Z, ZSpace)}";
        
        string Format(double value, Space space) {
            return space switch {
                Space.World => value.ToString(CultureInfo.InvariantCulture),
                Space.Local => $"^{value}",
                Space.Relative => $"~{value}",
                _ => throw new ArgumentOutOfRangeException(nameof(space), space, null)
            };
        }
    }

    public static Coordinate Parse(string str) {
        var parts = str.Split(' ');
        if (parts.Length != 3) {
            throw new ArgumentException("Coordinate must have 3 parts.");
        }
        return new Coordinate {
            X = ParseComponent(parts[0], out var xSpace),
            Y = ParseComponent(parts[1], out var ySpace),
            Z = ParseComponent(parts[2], out var zSpace),
            XSpace = xSpace,
            YSpace = ySpace,
            ZSpace = zSpace
        };
        
        double ParseComponent(string component, out Space space) {
            if (component.StartsWith("~")) {
                space = Space.Relative;
                component = component[1..];
            } else if (component.StartsWith("^")) {
                space = Space.Local;
                component = component[1..];
            } else {
                space = Space.World;
            }
            if (component.Length == 0) return 0;
            if (double.TryParse(component, out var value)) return value;
            throw new ArgumentException($"Invalid coordinate component: {component}");
        }
    }
    
    public static implicit operator Coordinate((double, double, double) tuple) => new() {
        X = tuple.Item1,
        Y = tuple.Item2,
        Z = tuple.Item3
    };

    public static implicit operator Coordinate(string str) => Parse(str);
}

public static class CoordinateExtensions {
    public static Coordinate AsLocal(this (double, double, double) tuple) {
        Coordinate coordinate = tuple;
        coordinate.Space = Space.Local;
        return coordinate;
    }
    
    public static Coordinate AsRelative(this (double, double, double) tuple) {
        Coordinate coordinate = tuple;
        coordinate.Space = Space.Relative;
        return coordinate;
    }
    
    public static Coordinate AsWorld(this (double, double, double) tuple) {
        Coordinate coordinate = tuple;
        coordinate.Space = Space.World;
        return coordinate;
    }
}

public enum Space {
    World,
    Local,
    Relative
}