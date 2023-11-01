namespace Lilypad.Predicates;

public class EntityConditions {
    public DistanceTags? Distance;
    public Dictionary<EnumReference<Effect>, EffectProperty>? Effects;
    public EquipmentTags? Equipment;
    public EntityFlags? Flags;
    public LocationTags? Location;
    public string? Nbt;
    public EntityConditions? Passenger;
    public LocationTags? SteppingOn;
    public string? Team;
    public EnumReference<Entity>? Type;
    public EntityConditions? TargetedEntity;
    public EntityConditions? Vehicle;
    public EntityConditions? SourceEntity;
    public ITypeSpecificTag? TypeSpecific;
}