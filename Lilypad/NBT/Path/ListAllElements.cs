using Lilypad.Extensions;

namespace Lilypad; 

public class ListAllElements : NBTPathNode {
    public string Name;
    
    public ListAllElements(string name) {
        Name = name;
    }
    
    public override string ToString() {
        return $"{Name.Quote()}[]";
    }
}