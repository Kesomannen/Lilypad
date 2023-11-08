using Lilypad;
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
    
    public int[] ToIntArray() {
        return new[] { A, B, C, D };
    }

    public string ToHyphenatedHexadecimal() {
        var bytes = new byte[16];
        BitConverter.TryWriteBytes(bytes.AsSpan(0, 4), A);
        BitConverter.TryWriteBytes(bytes.AsSpan(4, 4), B);
        BitConverter.TryWriteBytes(bytes.AsSpan(8, 4), C);
        BitConverter.TryWriteBytes(bytes.AsSpan(12, 4), D);
        
        return new Guid(bytes).ToString("D");
    }
    
    public override string ToString() {
        return ToHyphenatedHexadecimal();
    }
    
    public static Uuid Parse(string value) {
        var parts = value.Split(',');
        return new Uuid(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }
    
    public static Uuid New() {
        return new Uuid(Random.NextInt(), Random.NextInt(), Random.NextInt(), Random.NextInt());
    }

    public static implicit operator NBTValue(Uuid uuid) => uuid.ToIntArray();
    
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