namespace Lilypad; 

public static class AttributeModifier {
    public static NBTCompound Get(
        EnumReference<AttributeSlot> slot,
        int amount,
        string attribute,
        EnumReference<AttributeOperation> operation,
        string? name = null,
        Uuid? uuid = null
    ) {
        name ??= attribute;
        uuid ??= Uuid.New();

        return new NBTCompound {
            ["Amount"] = amount,
            ["AttributeName"] = attribute,
            ["Name"] = name,
            ["Operation"] = (int)operation.Value,
            ["UUID"] = uuid.Value,
            ["Slot"] = slot.ToString()
        }; 
    }
}

public enum AttributeSlot {
    Mainhand,
    Offhand,
    Feet,
    Legs,
    Chest,
    Head
}

public enum AttributeOperation {
    Add,
    Multiply,
    MultiplyBase
}