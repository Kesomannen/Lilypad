namespace Lilypad; 

[Flags]
public enum HideFlags {
    None = 0,
    Enchantments = 1 << 0,
    Attributes = 1 << 1,
    Unbreakable = 1 << 2,
    CanDestroy = 1 << 3,
    CanPlaceOn = 1 << 4,
    Additional = 1 << 5,
    Dye = 1 << 6,
    Everything = Enchantments | Attributes | Unbreakable | CanDestroy | CanPlaceOn | Additional | Dye
}