using Lilypad.Extensions;

namespace Lilypad.Helpers; 

public struct DataVariable : IVariable, IEquatable<DataVariable> {
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
    
    public bool Equals(DataVariable other) {
        return Source.Equals(other.Source) && Type.Equals(other.Type) && Path.Equals(other.Path) && Scale.Equals(other.Scale);
    }
    
    public override bool Equals(object? obj) {
        return obj is DataVariable other && Equals(other);
    }
    
    public override int GetHashCode() {
        return HashCode.Combine(Source, Type, Path, Scale);
    }
    
    public static bool operator ==(DataVariable left, DataVariable right) {
        return left.Equals(right);
    }
    
    public static bool operator !=(DataVariable left, DataVariable right) {
        return !left.Equals(right);
    }
}