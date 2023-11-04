namespace Lilypad.Predicates; 

public class TableBonus : Predicate {
    public EnumReference<Enchantment> Enchantment;
    public List<float> Chances = new();
}