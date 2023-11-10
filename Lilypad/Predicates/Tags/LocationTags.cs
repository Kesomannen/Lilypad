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

    public LocationTags(params Block[] blocks) {
        Block = new BlockTags(blocks);
    }
}