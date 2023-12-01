using Lilypad.Extensions;

namespace Lilypad.Helpers; 

public struct DataVariable : IVariable {
    public DataSource Source;
    public EnumReference<StoreDataType> Type;
    public NBTPath Path;
    public double Scale;

    public DataVariable(DataSource source, NBTPath path, EnumReference<StoreDataType>? type = null, double scale = 1) {
        Source = source;
        Path = path;
        Type = type ?? StoreDataType.Int;
        Scale = scale;
    }

    public void Get(Function function) {
        function.GetData(Source, Path, Scale);
    }

    public ExecuteCommand Set(ExecuteCommand execute) {
        return execute.Store(Source, Path, Type, Scale);
    }

    public override string ToString() {
        return IVariable.GetName("Variable", "Nbt", Path, Source.Type, Source.Target.ToString()!.Quote('\''));
    }
}