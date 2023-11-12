namespace Lilypad.Helpers; 

public readonly struct DataVariable : IVariable {
    readonly DataSource _source;
    readonly EnumReference<StoreDataType> _type;
    readonly NBTPath _path;
    readonly double _scale;

    public DataVariable(DataSource source, EnumReference<StoreDataType> type, NBTPath path, double scale = 1) {
        _source = source;
        _type = type;
        _path = path;
        _scale = scale;
    }

    public void Get(Function function) {
        function.GetData(_source, _path, _scale);
    }

    public ExecuteCommand Set(ExecuteCommand execute) {
        return execute.Store(_source, _path, _type, 1 / _scale);
    }

    public override string ToString() {
        return $"Variable-Nbt-{_path}-{_source.Type}-'{_source.Target}'".Replace(" ", "$");
    }
}