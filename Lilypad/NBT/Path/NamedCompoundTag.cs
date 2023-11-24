using Lilypad.Extensions;

namespace Lilypad; 

public class NamedCompoundTag : NBTPathNode {
    public string Name;
    public NBTCompound Tag;
    
    public NamedCompoundTag(string name, NBTCompound tag) {
        Name = name;
        Tag = tag;
    }
    
    public override string ToString() {
        return $"{Name.Quote()}{Tag}";
    }
}