namespace Lilypad; 

public class DamageTypeTags {
    public DamageTypeTag[]? Tags;
    public EntityConditions? SourceEntity;
    public EntityConditions? DirectEntity;
}

public struct DamageTypeTag {
    public EnumReference<DamageType> Type;
    public bool Expected;
}