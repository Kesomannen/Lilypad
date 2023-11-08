namespace Lilypad; 

public class LocationTags {
    public EnumReference<Biome>? Biome;
    public EnumReference<Dimension>? Dimension;
    public BlockTags? Block;
    public FluidTags? Fluid;
    public LightTags? Light;
    public PositionTags? Position;
    public bool? Smokey;
    public EnumReference<Structure>? Structure;
    
    public static implicit operator LocationTags(Block block) => new() {
        Block = new BlockTags(block)
    };
}