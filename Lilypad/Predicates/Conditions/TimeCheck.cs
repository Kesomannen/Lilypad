﻿using Lilypad.Loot;

namespace Lilypad.Predicates; 

public class TimeCheck : Predicate {
    public Range<NumberProvider> Value;
    public int? Period;
    
    public TimeCheck(Range<NumberProvider> value, int? period) {
        Value = value;
        Period = period;
    }
}