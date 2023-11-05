using Lilypad.Extensions;

namespace Lilypad; 

public readonly struct EnumReference<T> : ISerializeInner where T : struct, Enum {
    public T Value { get; }

    public EnumReference(T value) {
        Value = value;
    }
    
    public static implicit operator EnumReference<T>(string text) {
        var camelCase = text.ToCamelCase();
        if (!Enum.TryParse(camelCase, true, out T result)) {
            throw new ArgumentException($"Invalid {typeof(T).Name} value '{text}'");
        }
        return new EnumReference<T>(result);
    }
    
    public static implicit operator EnumReference<T>(T enumValue) {
        return new EnumReference<T>(enumValue);
    }
    
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