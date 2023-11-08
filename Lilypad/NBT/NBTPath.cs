namespace Lilypad; 

public readonly struct NBTPath {
    readonly string _path;
    
    public NBTPath(string path) {
        _path = path;
    }
    
    public override string ToString() {
        return _path;
    }
    
    public static implicit operator NBTPath(string path) => new(path);
}