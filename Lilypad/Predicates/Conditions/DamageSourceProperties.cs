namespace Lilypad.Predicates; 

public class DamageSourceProperties : Predicate {
    public EnumReference<Block> Block;
    public BlockProperties? Properties;
}