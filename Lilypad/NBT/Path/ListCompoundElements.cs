using Lilypad.Extensions;

namespace Lilypad; 

public class ListCompoundElements : NBTPathNode {
    public string Name;
    public NBT Tag;
    
    public ListCompoundElements(string name, NBT tag) {
        Name = name;
        Tag = tag;
    }
    
    public override string ToString() {
        return $"{Name.Quote()}[{Tag}]";
    }
}