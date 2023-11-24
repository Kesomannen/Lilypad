﻿namespace Lilypad; 

/// <summary>
/// A range of values.
/// </summary>
public struct Range<T> {
    /// <summary>
    /// The minimum value, null if there is none.
    /// </summary>
    public T? Min;
    
    /// <summary>
    /// The maximum value, null if there is none.
    /// </summary>
    public T? Max;

    public Range(T? min, T? max) {
        Min = min;
        Max = max;
    }

    public override string ToString() {
        if (Min is null && Max is null) return "";
        if (Min is null) return $"..{Max}";
        if (Max is null) return $"{Min}..";
        return $"{Min}..{Max}";
    }
    
    public static implicit operator Range<T>(T? value) => new(value, value);
    public static implicit operator Range<T>((T? min, T? max) value) => new(value.min, value.max);
}