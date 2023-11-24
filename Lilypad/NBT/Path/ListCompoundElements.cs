using Lilypad.Extensions;

namespace Lilypad; 

public class ListCompoundElements : NBTPathNode {
    public string Name;
    public NBTCompound Tag;
    
    public ListCompoundElements(string name, NBTCompound tag) {
        Name = name;
        Tag = tag;
    }
    
    public override string ToString() {
        return $"{Name.Quote()}[{Tag}]";
    }
}