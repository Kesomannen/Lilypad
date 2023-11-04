namespace Lilypad.Predicates; 

public class RandomChanceWithLooting : Predicate {
    public float LootingMultiplier;
    
    float _chance;
    
    public float Chance {
        get => _chance;
        set => _chance = Math.Clamp(value, 0, 1);
    }
}