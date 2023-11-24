using Lilypad.Extensions;

namespace Lilypad;

public readonly struct DataSource {
    public readonly EnumReference<DataSourceType> Type;
    public readonly object Target;

    public static readonly DataSource Self = Entity(Selector.Self);

    public DataSource(string storageName, string? @namespace = null) {
        @namespace ??= Datapack.DefaultDefaultNamespace;
        Type = DataSourceType.Storage;
        Target = $"{@namespace}:{storageName}";
    }

    public DataSource(Argument<Selector> selector) {
        Type = DataSourceType.Entity;
        Target = selector;
    }

    public DataSource(Vector3 vector3) {
        Type = DataSourceType.Block;
        Target = vector3;
    }

    public override string ToString() {
        return $"{Type} {Target}";
    }

    public static bool TryParse(string str, out DataSource source) {
        source = default;
        var components = str.Split(' ');
        if (components.Length != 2) {
            return false;
        }
        
        if (!Enum.TryParse(components[0].ToCamelCase(), out DataSourceType type)) return false;

        switch (type) {
            case DataSourceType.Block:
                if (!Vector3.TryParse(components[1], out var coordinate)) return false;
                source = Block(coordinate);
                return true;

            case DataSourceType.Entity:
                source = Entity(components[1]);
                return true;
                
            case DataSourceType.Storage:
                var segments = components[1].Split(':');
                switch (segments.Length) {
                    case > 2:
                        return false;
                    case 2:
                        source = Storage(segments[1], segments[0]);
                        return true;
                    default:
                        source = Storage(segments[0]);
                        return true;
                }

            default: throw new ArgumentOutOfRangeException();
        }
    }

    public static DataSource Storage(string storageName, string? @namespace = null) => new(storageName, @namespace);
    public static DataSource Entity(Argument<Selector> selector) => new(selector);
    public static DataSource Block(Vector3 vector3) => new(vector3);

    public static implicit operator DataSource(string str) {
        if (!TryParse(str, out var source)) {
            throw new FormatException($"'{str}' is not a valid data source.");
        }
        return source;
    }
}

public enum DataSourceType {
    Block,
    Entity,
    Storage
}