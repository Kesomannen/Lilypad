namespace Lilypad.NBT; 

public readonly struct NBTPath {
    readonly string[] _path;
    
    public NBTPath(params string[] path) {
        _path = path;
    }
    
    public override string ToString() {
        return string.Join('.', _path);
    }

    public static implicit operator NBTPath(string path) => new(path.Split("."));
    public static implicit operator NBTPath(string[] path) => new(path);
}