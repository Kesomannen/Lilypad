namespace Lilypad; 

public class DamageTypeTags {
    public DamageTypeTag[]? Tags;
    public EntityConditions? SourceEntity;
    public EntityConditions? DirectEntity;

    public DamageTypeTags() { }

    public DamageTypeTags(params DamageTypeTag[] tags) {
        Tags = tags;
    }

    public DamageTypeTags(params EnumReference<DamageType>[] types) {
        Tags = types.Select(DamageTypeTag.Is).ToArray();
    }
}

public struct DamageTypeTag {
    public EnumReference<DamageType> Type;
    public bool Expected;
    
    public static DamageTypeTag Is(EnumReference<DamageType> type) => (type, true);
    public static DamageTypeTag Not(EnumReference<DamageType> type) => (type, false);

    public static implicit operator DamageTypeTag((EnumReference<DamageType> type, bool expected) tuple) {
        return new DamageTypeTag {
            Type = tuple.type,
            Expected = false
        };
    }
}