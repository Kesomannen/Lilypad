using Lilypad.Extensions;
using Newtonsoft.Json;

namespace Lilypad; 

[JsonConverter(typeof(Converter))]
public class NBTPath {
    public List<NBTPathNode> Nodes = new();
    
    public string Path => string.Join('.', Nodes.Select(node => node.ToString()));

    public NBTPath() { }

    public NBTPath(string path) {
        Name(path);
    }
    
    public NBTPath(NBTCompound root) {
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
    
    public NBTPath Tag(string name, NBTCompound tag) {
        return Add(new NamedCompoundTag(name, tag));
    }
    
    public NBTPath Index(string name, int index) {
        return Add(new ListSingleElement(name, index));
    }
    
    public NBTPath All(string name) {
        return Add(new ListAllElements(name));
    }
    
    public NBTPath CompoundInList(string name, NBTCompound tag) {
        return Add(new ListCompoundElements(name, tag));
    }

    public override string ToString() => Path;

    public static implicit operator NBTPath(string path) => new(path);
    
    class Converter : WriteOnlyConverter<NBTPath> {
        protected override void WriteJson(JsonWriter writer, NBTPath value, JsonSerializer serializer) {
            writer.WriteValue(value.Path);
        }
    }
}