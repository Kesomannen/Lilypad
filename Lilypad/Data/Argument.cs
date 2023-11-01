namespace Lilypad; 

public readonly struct Argument<T> {
    public string Text { get; }

    public Argument(string text) {
        Text = text;
    }
    
    public static implicit operator Argument<T>(string text) => new(text);
    public static implicit operator Argument<T>(T value) => new(value.ToString());

    public override string ToString() => Text;
}