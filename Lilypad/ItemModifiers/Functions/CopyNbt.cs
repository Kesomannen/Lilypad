using Lilypad.Helpers;
using Newtonsoft.Json;

namespace Lilypad.ItemModifiers; 

/// <summary>
/// Copies NBT values from a specified block entity or entity, or from storage to the item's <c>tag</c> tag. 
/// </summary>
public class CopyNbt : ItemFunction {
    public CopyNBTSource Source;
    [JsonProperty("ops")]
    public List<NBTOperation> Operations = new();

    public static CopyNbt FromVariable(
        Function function,
        IVariable variable,
        NBTPath target,
        EnumReference<NBTOperationType>? operationType = null,
        EnumReference<StoreDataType>? dataType = null,
        double scale = 1
    ) {
        var storage = function.ToStorage(variable, Names.Get("#copynbt"), dataType, scale);
        return new CopyNbt {
            Source = CopyNBTSource.From(storage.Source),
            Operations = {
                new NBTOperation {
                    Operation = operationType ?? NBTOperationType.Replace,
                    Source = storage.Path,
                    Target = target
                }
            }
        };
    }
}

public struct NBTOperation {
    public NBTPath Source;
    public NBTPath Target;
    [JsonProperty("op")] 
    public EnumReference<NBTOperationType> Operation;

    public NBTOperation(NBTPath source, NBTPath target, EnumReference<NBTOperationType>? operationType = null) {
        Source = source;
        Target = target;
        Operation = operationType ?? NBTOperationType.Replace;
    }
}

public enum NBTOperationType {
    /// <summary>
    /// Replaces any existing data at the target path with the copied data.
    /// </summary>
    Replace,
    
    /// <summary>
    /// Appends the copied data to an array or list at the target path.
    /// </summary>
    Append,
    
    /// <summary>
    /// Merges the copied data into a compound tag at the target path.
    /// </summary>
    Merge
}

/// <summary>
/// Defines the source of the NBT data to copy.
/// </summary>
public struct CopyNBTSource {
    public EnumReference<CopyNBTSourceType> Type;
    public EnumReference<LootContextEntity>? Target;
    public string? Source;
    
    public CopyNBTSource(string storage) {
        Type = CopyNBTSourceType.Storage;
        Source = storage;
    }
    
    public CopyNBTSource(EnumReference<LootContextEntity> target) {
        Type = CopyNBTSourceType.Context;
        Target = target;
    }

    public static CopyNBTSource From(DataSource source) {
        if (source.Type.Value == DataSourceType.Storage) {
            return new CopyNBTSource((string)source.Target);
        }
        throw new InvalidOperationException("Cannot create CopyNBTSource from a non-storage DataSource");
    }
}

public enum CopyNBTSourceType {
    Context,
    Storage
}