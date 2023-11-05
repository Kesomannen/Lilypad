using Lilypad.Extensions;
using Random = Lilypad.Extensions.Random;

namespace Lilypad; 

public readonly struct Uuid {
    public readonly int A, B, C, D;
    
    public Uuid(int a, int b, int c, int d) {
        A = a;
        B = b;
        C = c;
        D = d;
    }
    
    public override string ToString() {
        return $"{A:X8}-{B:X8}-{C:X8}-{D:X8}";
    }
    
    public static Uuid Parse(string value) {
        var parts = value.Split(',');
        return new Uuid(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }
    
    public static Uuid New() {
        return new Uuid(Random.NextInt(), Random.NextInt(), Random.NextInt(), Random.NextInt());
    }
    
    public int[] ToIntArray() {
        return new[] { A, B, C, D };
    }
    
    public static bool operator ==(Uuid a, Uuid b) {
        return a.A == b.A && a.B == b.B && a.C == b.C && a.D == b.D;
    }
    
    public static bool operator !=(Uuid a, Uuid b) {
        return !(a == b);
    }
    
    public override bool Equals(object? obj) {
        return obj is Uuid other && this == other;
    }
    
    public override int GetHashCode() {
        return HashCode.Combine(A, B, C, D);
    }
}