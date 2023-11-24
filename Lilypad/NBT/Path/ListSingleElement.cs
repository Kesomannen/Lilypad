using Lilypad.Extensions;

namespace Lilypad; 

public class ListSingleElement : NBTPathNode {
    public string Name;
    public int Index;
    
    public ListSingleElement(string name, int index) {
        Name = name;
        Index = index;
    }
    
    public override string ToString() {
        return $"{Name.Quote()}[{Index}]";
    }
}