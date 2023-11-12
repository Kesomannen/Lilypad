namespace Lilypad; 

public class EntityProperties : Predicate {
    public EnumReference<LootContextEntity> Entity;
    public EntityConditions Predicate = new();
}