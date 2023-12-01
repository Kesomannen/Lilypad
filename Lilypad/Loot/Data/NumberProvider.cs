namespace Lilypad;

public abstract class NumberProvider {
    public string Type => GetType().Name.ToLower();
    
    public static implicit operator NumberProvider(float value) {
        return new Constant { Value = value };
    }

    public static implicit operator NumberProvider(NumberProviderRange range) {
        return new Uniform { Range = range };
    }

    public static implicit operator NumberProvider(IntRange range) {
        return new Uniform { Range = new NumberProviderRange(range.Min, range.Max) };
    }

    public static implicit operator NumberProvider(Range<float> range) {
        return new Uniform { Range = new NumberProviderRange(range.Min, range.Max) };
    }
    
    public static implicit operator NumberProvider((int? min, int? max) range) {
        return new Uniform { Range = new NumberProviderRange(range.min, range.max) };
    }

    public static implicit operator NumberProvider((float? min, float? max) range) {
        return new Uniform { Range = new NumberProviderRange(range.min, range.max) };
    }
}