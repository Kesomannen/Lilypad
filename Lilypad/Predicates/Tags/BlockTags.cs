namespace Lilypad; 

public class BlockTags {
    public List<Block>? Blocks;
    public Reference<Tag<Block>>? Tag;
    public string? Nbt;
    public BlockState? State;

    public BlockTags(params Block[] blocks) {
        Blocks = blocks.ToList();
    }
}