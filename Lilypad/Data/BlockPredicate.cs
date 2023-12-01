using Lilypad.Extensions;

namespace Lilypad; 

public class BlockPredicate {
    public EnumReference<Block> Block;
    public BlockState? State;
    public NBT? Data;
    
    public BlockPredicate(EnumReference<Block> block) {
        Block = block;
    }

    public override string ToString() {
        return $"{Block}{State.ToStringOrEmpty()}{Data.ToStringOrEmpty()}";
    }
}