using Lilypad.Helpers;

namespace Lilypad; 

public struct VectorComponent {
    public double Value;
    public Space Space;

    public VectorComponent(double value, Space space = Space.World) {
        Value = value;
        Space = space;
    }
    
    public static bool TryParse(string str, out VectorComponent component) {
        if (string.IsNullOrEmpty(str)) {
            component = default;
            return false;
        }
        
        Space space;
        double value;
        
        if (str.StartsWith("~")) {
            space = Space.Relative;
            str = str[1..];
        } else if (str.StartsWith("^")) {
            space = Space.Local;
            str = str[1..];
        } else {
            space = Space.World;
        }
        
        if (str.Length == 0) {
            value = 0;
        } else if (double.TryParse(str, out var result)) {
            value = result;
        } else {
            component = default;
            return false;
        }
        
        component = new VectorComponent(value, space);
        return true;
    }

    public override string ToString() {
        return Space switch {
            Space.World => $"{Value:0.####}",
            Space.Local => $"^{Value:#.####}",
            Space.Relative => $"~{Value:#.####}",
            _ => throw new ArgumentOutOfRangeException(nameof(Space), Space, null)
        };
    }

    public static implicit operator VectorComponent(double value) => new(value);

    public static implicit operator VectorComponent(string str) {
        if (!TryParse(str, out var component)) {
            throw new FormatException($"'{str}' is not a valid vector component.");
        }
        return component;
    }
    
    public static VectorComponent operator +(VectorComponent a, VectorComponent b) {
        Assert.IsTrue(a.Space == b.Space, "Cannot add components in different spaces.");
        return new VectorComponent(a.Value + b.Value, a.Space);
    }
    
    public static VectorComponent operator -(VectorComponent a, VectorComponent b) {
        Assert.IsTrue(a.Space == b.Space, "Cannot subtract components in different spaces.");
        return new VectorComponent(a.Value - b.Value, a.Space);
    }
    
    public static VectorComponent operator *(VectorComponent a, VectorComponent b) {
        Assert.IsTrue(a.Space == b.Space, "Cannot multiply components in different spaces.");
        return new VectorComponent(a.Value * b.Value, a.Space);
    }
}