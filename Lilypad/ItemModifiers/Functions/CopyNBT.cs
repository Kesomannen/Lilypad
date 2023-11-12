using Newtonsoft.Json;

namespace Lilypad; 

/// <summary>
/// Copies NBT values from a specified block entity or entity, or from storage to the item's <c>tag</c> tag. 
/// </summary>
public class CopyNBT : ItemFunction {
    public CopyNBTSource Source;
    public List<NBTOperation> Operations = new();
}

public struct NBTOperation {
    public NBTPath Source;
    public NBTPath Target;
    [JsonProperty("op")]
    public EnumReference<NBTOperationType> Operation;
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
    
    public CopyNBTSource(string storageNamespace, string storageName) {
        Type = CopyNBTSourceType.Storage;
        Source = $"{storageNamespace}:{storageName}";
    }
    
    public CopyNBTSource(EnumReference<LootContextEntity> target) {
        Type = CopyNBTSourceType.Context;
        Target = target;
    }
}

public enum CopyNBTSourceType {
    Context,
    Storage
}