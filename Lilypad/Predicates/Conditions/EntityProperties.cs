namespace Lilypad.Predicates; 

public class EntityProperties : Predicate {
    public EnumReference<ContextEntity> Entity;
    public EntityConditions Predicate = new();
}