namespace Lilypad; 

public class EquipmentTags {
    public ItemConditions? Mainhand, Offhand, Head, Chest, Legs, Feet;

    public EquipmentTags() { }
    
    public EquipmentTags(EnumReference<AttributeSlot> slot, ItemConditions conditions) {
        switch (slot.Value) {
            case AttributeSlot.Mainhand:
                Mainhand = conditions; break;
            case AttributeSlot.Offhand:
                Offhand = conditions; break;
            case AttributeSlot.Head:
                Head = conditions; break;
            case AttributeSlot.Chest:
                Chest = conditions; break;
            case AttributeSlot.Legs:
                Legs = conditions; break;
            case AttributeSlot.Feet:
                Feet = conditions; break;
            
            default: throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
        }
    }
}