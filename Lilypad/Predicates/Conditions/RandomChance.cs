namespace Lilypad; 

public class RandomChance : Predicate {
    float _chance;
    
    public float Chance {
        get => _chance;
        set => _chance = Math.Clamp(value, 0, 1);
    }
}