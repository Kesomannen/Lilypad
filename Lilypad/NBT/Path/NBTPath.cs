namespace Lilypad; 

public class NBTPath : ISerializeInner {
    public List<NBTPathNode> Nodes = new();
    
    public string Path => string.Join('.', Nodes.Select(node => node.ToString()));

    public NBTPath() { }

    public NBTPath(NBT root) {
        Add(new RootCompoundTag(root));
    }
    
    public NBTPath(params NBTPathNode[] node) {
        Nodes.AddRange(node);
    }
    
    public NBTPath Add(NBTPathNode node) {
        Nodes.Add(node);
        return this;
    }
    
    public NBTPath Name(string name) {
        return Add(new NamedTag(name));
    }
    
    public NBTPath Tag(string name, NBT tag) {
        return Add(new NamedCompoundTag(name, tag));
    }
    
    public NBTPath Index(string listName, int index) {
        return Add(new ListSingleElement(listName, index));
    }
    
    public NBTPath All(string listName) {
        return Add(new ListAllElements(listName));
    }
    
    public NBTPath CompoundInList(string listName, NBT tag) {
        return Add(new ListCompoundElements(listName, tag));
    }

    public override string ToString() => Path;

    public static implicit operator NBTPath(string path) => new(path);
    
    object ISerializeInner.SerializedData => Path;
}