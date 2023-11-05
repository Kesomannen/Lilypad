namespace Lilypad.NBT;

public readonly struct DataSource {
    public readonly EnumReference<DataSourceType> Type;
    public readonly object Target;

    public DataSource(string storageName, string? storageNamespace = null) {
        storageNamespace ??= Namespaces.Default;
        Type = DataSourceType.Storage;
        Target = $"{storageNamespace}:{storageName}";
    }
    
    public DataSource(Argument<Selector> selector) {
        Type = DataSourceType.Entity;
        Target = selector;
    }
    
    public DataSource(Coordinate coordinate) {
        Type = DataSourceType.Block;
        Target = coordinate;
    }
    
    public override string ToString() {
        return $"{Type} {Target}";
    }
    
    public static DataSource Storage(string storageName, string? storageNamespace = null) => new(storageName, storageNamespace);
    public static DataSource Entity(Argument<Selector> selector) => new(selector);
    public static DataSource Block(Coordinate coordinate) => new(coordinate);
}

public enum DataSourceType {
    Block,
    Entity,
    Storage
}