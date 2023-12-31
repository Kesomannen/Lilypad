﻿namespace Lilypad; 

/// <summary>
/// A 128-bit UUID. Used in minecraft for identifying entities,
/// usually formatted like <c>f81d4fae-7dec-11d0-a765-00a0c91e6bf6</c>.
/// </summary>
public readonly struct Uuid : ISerializeInnerNBT, ISerializeInnerJson {
    /// <summary>
    /// A 32 bit segment of the UUID.
    /// </summary>
    public readonly int A, B, C, D;

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
    /// Formats the UUID as four 32 bit integers in an array.
    /// </summary>
    public int[] ToIntArray() {
        return new[] { A, B, C, D };
    }

    /// <summary>
    /// Formats the UUID as a hexadecimal string in the format 8-4-4-4-12.
    /// This is the most common format used by Minecraft and the format used when turning a UUID into a string.
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
    
    /// <inheritdoc cref="ToHyphenatedHexadecimal"/>
    public override string ToString() {
        return ToHyphenatedHexadecimal();
    }

    /// <summary>
    /// Parses a UUID from a string formatted as four 32 bit integers separated by commas.
    /// </summary>
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

    object ISerializeInnerNBT.SerializedData => ToIntArray();
    object ISerializeInnerJson.SerializedData => ToHyphenatedHexadecimal();
}