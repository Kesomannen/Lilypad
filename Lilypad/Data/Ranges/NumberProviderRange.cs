namespace Lilypad;

public class NumberProviderRange : Range<NumberProvider> {
    public NumberProviderRange(NumberProvider? min, NumberProvider? max) : base(min, max) { }

    public static implicit operator NumberProviderRange(NumberProvider? value) => new(value, value);
    public static implicit operator NumberProviderRange((NumberProvider? min, NumberProvider? max) value) => new(value.min, value.max);
}