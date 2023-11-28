using Lilypad.Extensions;

namespace Lilypad; 

/// <summary>
/// A reference to an enum value. You can implicitly convert a <typeparamref name="T"/> and <see cref="string"/>
/// to this type.
/// </summary>
/// <remarks>
/// Formats its value as snake_case when turned into a string.
/// Use instead of a normal enum when referencing minecraft objects.
/// </remarks>
/// <seealso cref="Reference{T}"/>
public readonly struct EnumReference<T> : ISerializeInner where T : struct, Enum {
    public T Value { get; }

    public EnumReference(T value) {
        Value = value;
    }
    
    public static EnumReference<T> Parse(string text) {
        var camelCase = text.ToCamelCase();
        if (!Enum.TryParse(camelCase, true, out T result)) {
            throw new ArgumentException($"Invalid {typeof(T).Name} value '{text}'");
        }
        return new EnumReference<T>(result);
    }
    
    /// <summary>
    /// Implicitly converts a string to a <see cref="EnumReference{T}"/>.
    /// Note that the conversion can fail if the string is not a valid enum vThe converted string can be in any case.alue.
    /// </summary>
    public static implicit operator EnumReference<T>(string text) => Parse(text);
    public static implicit operator EnumReference<T>(T enumValue) => new(enumValue);

    public static bool operator ==(EnumReference<T> a, T b) {
        return a.Value.Equals(b);
    }
    
    public static bool operator !=(EnumReference<T> a, T b) {
        return !a.Value.Equals(b);
    }
    
    public bool Equals(EnumReference<T> other) {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj) {
        return obj is EnumReference<T> other && Equals(other);
    }

    public override int GetHashCode() {
        return Value.GetHashCode();
    }

    public override string ToString() {
        return Value.ToString().ToSnakeCase();
    }
    
    object ISerializeInner.SerializedData => Value;
}