using Lilypad.Extensions;

namespace Lilypad; 

public class NamedCompoundTag : NBTPathNode {
    public string Name;
    public NBT Tag;
    
    public NamedCompoundTag(string name, NBT tag) {
        Name = name;
        Tag = tag;
    }
    
    public override string ToString() {
        return $"{Name.Quote()}{Tag}";
    }
}