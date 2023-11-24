using Lilypad.Helpers;
using Newtonsoft.Json;

namespace Lilypad.ItemModifiers; 

/// <summary>
/// Copies NBT values from a specified block entity or entity, or from storage to the item's <c>tag</c> tag. 
/// </summary>
public class CopyNBT : ItemFunction {
    public CopyNBTSource Source;
    [JsonProperty("ops")]
    public List<NBTOperation> Operations = new();
    
    public override string FunctionName => "copy_nbt";

    /// <summary>
    /// Creates a <see cref="CopyNBT"/> function from a variable,
    /// by first copying it to temporary data storage.
    /// </summary>
    /// <param name="function">
    /// A function to put the commands for copying the variable to storage,
    /// which should always be ran before using the output function.
    /// The storage will also share the same namespace as the function.
    /// </param>
    /// <param name="variable">Variable to copy from, must be the same type as <paramref name="operationType"/>.</param>
    /// <param name="target">The item path to copy the data to.</param>
    /// <param name="operationType">The operation type to use when copying, defaults to <see cref="NBTOperationType.Replace"/>.</param>
    /// <param name="dataType">The data type to use when copying the variable, defaults to <see cref="StoreDataType.Int"/>.</param>
    /// <param name="scale">Value of variable will be multiplied by this value before copying to the <paramref name="target"/></param>
    /// <returns>A <see cref="CopyNBT"/> function from <paramref name="variable"/>.</returns>
    public static CopyNBT FromVariable(
        Function function,
        IVariable variable,
        NBTPath target,
        EnumReference<NBTOperationType>? operationType = null,
        EnumReference<StoreDataType>? dataType = null,
        double scale = 1
    ) {
        var storage = function.ToStorage(variable, Names.Get("#copynbt"), dataType, scale);
        return new CopyNBT {
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

/// <summary>
/// One copy operation in a <see cref="CopyNBT"/> function.
/// </summary>
public struct NBTOperation {
    /// <summary>
    /// The NBT path in the source to copy from.
    /// </summary>
    public NBTPath Source;
    
    /// <summary>
    /// The NBT path in the target to copy to.
    /// </summary>
    public NBTPath Target;
    [JsonProperty("op")] 
    public EnumReference<NBTOperationType> Operation;
    
    /// <param name="operationType">Defaults to <see cref="NBTOperationType.Replace"/>.</param>
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
/// Defines the source of the NBT data in a <see cref="CopyNBT"/> function.
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