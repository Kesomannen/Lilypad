﻿namespace Lilypad; 

public static class DataFunctionExtensions {
    public static Function GetData(this Function function, in DataSource source, NBTPath path, double scale = 1) {
        return function.Add($"data get {source} {path} {scale}");
    }
    
    public static Function GetData(this Function function, in DataSource source) {
        return function.Add($"data get {source}");
    }
    
    public static Function MergeData(this Function function, in DataSource source, NBT data) {
        return function.Add($"data merge {source} {data}");
    }
    
    public static Function RemoveData(this Function function, in DataSource source, NBTPath path) {
        return function.Add($"data remove {source} {path}");
    }
    
    public static Function ModifyData(this Function function, in DataSource source, NBTPath path, in DataOperation operation) {
        return function.Add($"data modify {source} {path} {operation}");
    }
}
