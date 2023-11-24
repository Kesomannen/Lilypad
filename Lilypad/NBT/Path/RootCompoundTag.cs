namespace Lilypad; 

public class RootCompoundTag : NBTPathNode {
    public NBTCompound Tag;
    
    public RootCompoundTag(NBTCompound tag) {
        Tag = tag;
    }

    public override string ToString() {
        return Tag.ToString();
    }
}