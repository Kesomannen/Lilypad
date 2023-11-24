namespace Lilypad;

public abstract class NBTPathNode {
    public new abstract string ToString();
    
    public static implicit operator NBTPathNode(string name) => new NamedTag(name);
}