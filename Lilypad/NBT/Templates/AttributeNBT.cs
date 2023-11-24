namespace Lilypad;

public class AttributeModifierNBT : NBTTemplate {
    public EnumReference<AttributeSlot> Slot;
    public int Amount;
    public string Attribute;
    public EnumReference<AttributeOperation> Operation;
    public string Name;
    public Uuid Uuid;

    public AttributeModifierNBT(string attribute, int amount, EnumReference<AttributeSlot> slot) {
        Slot = slot;
        Amount = amount;
        Attribute = attribute;
        Operation = AttributeOperation.Add;
        Name = attribute;
        Uuid = Uuid.New();
    }

    public override NBT ToNBT() {
        return new NBT {
            ["Amount"] = Amount,
            ["AttributeName"] = Attribute,
            ["Name"] = Name,
            ["Operation"] = (int)Operation.Value,
            ["UUID"] = Uuid,
            ["Slot"] = Slot
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