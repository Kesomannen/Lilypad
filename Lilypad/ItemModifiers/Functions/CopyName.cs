namespace Lilypad.ItemModifiers; 

/// <summary>
/// Copies an entity's or a block entity's name tag into the item's <c>display.Name</c> tag. 
/// </summary>
public class CopyName : ItemFunction {
    public EnumReference<CopyNameSource> Source;
}

public enum CopyNameSource {
    This,
    BlockEntity,
    Killer,
    KillerPlayer
}