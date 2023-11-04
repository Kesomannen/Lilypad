using Lilypad.Loot;

namespace Lilypad.Predicates; 

public class ValueCheck : Predicate {
    public NumberProvider Value;
    public Range<NumberProvider> Range;

    public ValueCheck(NumberProvider value, Range<NumberProvider> range) {
        Value = value;
        Range = range;
    }
}