namespace Lilypad.NBT; 

[Flags]
public enum HideFlags {
    Enchantments = 1 << 0,
    Attributes = 1 << 1,
    Unbreakable = 1 << 2,
    CanDestroy = 1 << 3,
    CanPlaceOn = 1 << 4,
    Other = 1 << 5
}