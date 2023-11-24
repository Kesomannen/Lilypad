namespace Lilypad; 

/// <summary>
/// Represents a 128-bit UUID. Used in minecraft for identifying entities,
/// usually formatted like <c>f81d4fae-7dec-11d0-a765-00a0c91e6bf6</c>.
/// </summary>
public struct Uuid {
    /// <summary>
    /// 32 bit segment of the UUID.
    /// </summary>
    public int A, B, C, D;
    
    int[]? _array;
    
    /// <summary>
    /// Creates a UUID from four 32 bit segments.
    /// </summary>
    public Uuid(int a, int b, int c, int d) {
        A = a;
        B = b;
        C = c;
        D = d;
    }
    
    /// <summary>
    /// Formats the UUID as four 32 bit integers.
    /// </summary>
    public int[] ToIntArray() {
        return _array ??= new[] { A, B, C, D };
    }

    /// <summary>
    /// Formats the UUID as a hexadecimal string in the format 8-4-4-4-12.
    /// </summary>
    /// <example><c>f81d4fae-7dec-11d0-a765-00a0c91e6bf6</c>.</example>
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
    
    /// <summary>
    /// Creates a new random UUID.
    /// </summary>
    public static Uuid New() {
        return new Uuid(Random.Shared.Next(), Random.Shared.Next(), Random.Shared.Next(), Random.Shared.Next());
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