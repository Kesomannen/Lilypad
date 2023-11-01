namespace Lilypad.Predicates; 

public class BlockStateProperty : Predicate {
    public EnumReference<Block> Block;
    public BlockProperties? Properties;
}