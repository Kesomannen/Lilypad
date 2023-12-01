namespace Lilypad.Helpers; 

public static class Temp {
    static Objective? _objective;

    public static ScoreVariable Get(Resource resource, string name) {
        return Get(resource.Datapack, name);
    }

    public static ScoreVariable Get(Datapack datapack, string name) {
        return new ScoreVariable(name, GetObjective(datapack));
    }
    
    public static ScoreVariable CopyTempScore(this Function function, IVariable variable, string name) {
        var copy = Get(function, name);
        function.SetVariable(copy, variable);
        return copy;
    }

    public static ScoreVariable ToScore(this Function function, IVariable variable, string name) {
        if (variable is ScoreVariable score) return score;
        return function.CopyTempScore(variable, name);
    }

    public static DataVariable CopyTempStorage(
        this Function function,
        IVariable variable,
        NBTPath path,
        EnumReference<StoreDataType>? dataType = null,
        double scale = 1
    ) {
        var type = dataType?.Value ?? StoreDataType.Int;
        var result = new DataVariable(DataSource.Storage(Names.Get("temp_storage"), function.Namespace), path, type, scale);
        function.SetVariable(result, variable);
        return result;
    }
    
    public static DataVariable ToStorage(
        this Function function,
        IVariable variable,
        NBTPath path,
        EnumReference<StoreDataType>? dataType = null,
        double scale = 1
    ) {
        if (Math.Abs(scale - 1) < double.Epsilon && variable is DataVariable data) return data;
        return function.CopyTempStorage(variable, path, dataType, scale);
    }

    static Objective GetObjective(Datapack datapack) {
        return _objective ??= datapack.Objectives.GetOrCreate("temp");
    }
}