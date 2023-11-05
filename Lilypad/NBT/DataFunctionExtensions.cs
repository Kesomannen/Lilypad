namespace Lilypad.NBT; 

public static class DataFunctionExtensions {
    public static Function GetData(this Function function, DataSourceType source, string path, double scale = 1) {
        return function.Add($"data get {source} {path} {scale}");
    }
    
    public static Function GetData(this Function function, DataSourceType source) {
        return function.Add($"data get {source}");
    }
    
    public static Function MergeData(this Function function, DataSourceType source, NBTCompound data) {
        return function.Add($"data merge {source} {data}");
    }
    
    public static Function RemoveData(this Function function, DataSourceType source, string path) {
        return function.Add($"data remove {source} {path}");
    }
    
    public static Function ModifyData(this Function function, DataSourceType source, string path, DataOperation operation) {
        return function.Add($"data modify {source} {path} {operation}");
    }
}
