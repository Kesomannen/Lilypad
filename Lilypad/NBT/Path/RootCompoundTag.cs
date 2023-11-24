namespace Lilypad; 

public class RootCompoundTag : NBTPathNode {
    public NBT Tag;
    
    public RootCompoundTag(NBT tag) {
        Tag = tag;
    }

    public override string ToString() {
        return Tag.ToString();
    }
}