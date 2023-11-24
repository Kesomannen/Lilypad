using Lilypad.Extensions;

namespace Lilypad; 

public class NamedTag : NBTPathNode {
    public string Name;
    
    public NamedTag(string name) {
        Name = name;
    }
    
    public override string ToString() {
        return Name.Quote();
    }
}