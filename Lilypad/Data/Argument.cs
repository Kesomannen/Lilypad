using Lilypad.Helpers;

namespace Lilypad; 

/// <summary>
/// Can either be a string or a value of type <typeparamref name="T"/>.
/// </summary>
public readonly struct Argument<T> : ISerializeInner {
    public string Text { get; }

    /// <summary>
    /// Creates a new argument with the given <paramref name="text"/>.
    /// </summary>
    public Argument(string text) {
        Text = text;
    }
    
    /// <summary>
    /// Creates a new argument with the string representation of <paramref name="value"/>.
    /// </summary>
    public Argument(T value) {
        Assert.NotNull(value, nameof(value));
        var text = value!.ToString();
        Assert.NotNull(text, "value.ToString()");
        Text = text!;
    }
    
    public static implicit operator Argument<T>(string text) => new(text);
    public static implicit operator Argument<T>(T value) => new(value);

    public override string ToString() => Text;
    
    public object? SerializedData => Text;
}