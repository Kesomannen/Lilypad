namespace Lilypad.Helpers; 

public struct DataVariable : IVariable {
    public DataSource Source;
    public EnumReference<StoreDataType> Type;
    public NBTPath Path;
    public double Scale;

    public DataVariable(DataSource source, EnumReference<StoreDataType> type, NBTPath path, double scale = 1) {
        Source = source;
        Type = type;
        Path = path;
        Scale = scale;
    }

    public void Get(Function function) {
        function.GetData(Source, Path, Scale);
    }

    public ExecuteCommand Set(ExecuteCommand execute) {
        return execute.Store(Source, Path, Type, 1 / Scale);
    }

    public override string ToString() {
        return IVariable.GetName("Variable", "Nbt", Path, Source.Type, $"'{Source.Target}'");
    }
}