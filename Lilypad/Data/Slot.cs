namespace Lilypad; 

/// <summary>
/// Represents a slot in an inventory.
/// </summary>
public readonly struct Slot {
    /// <summary>
    /// String identifier. Used in the <c>item</c> command.
    /// </summary>
    public readonly string Id;
    
    /// <summary>
    /// Numeric identifier. Used in NBT data.
    /// </summary>
    public readonly int Index;

    Slot(string id, int index = 0) {
        Id = id;
        Index = index;
    }
    
    public override string ToString() => Id;

    public static class Armor {
        public static readonly Slot Chest = new("armor.chest", 102);
        public static readonly Slot Feet = new("armor.feet", 100);
        public static readonly Slot Head = new("armor.head", 103);
        public static readonly Slot Legs = new("armor.legs", 101);
    }

    public static class Weapon {
        public static readonly Slot MainHand = new("weapon.mainhand", 0);
        public static readonly Slot OffHand = new("weapon.offhand", -106);
    }
    
    public static class Horse {
        public static readonly Slot Saddle = new("horse.armor", 400);
        public static readonly Slot Chest = new("horse.chest", 499);
        public static readonly Slot Armor = new("horse.saddle", 401);
        
        public static Slot Inventory(int index) {
            AssertRange(index, 0, 14);
            return new Slot($"horse.inventory.{index}", index + 500);
        }
    }

    public static class Villager {
        public static Slot Inventory(int index) {
            AssertRange(index, 0, 7);
            return new Slot($"villager.inventory.{index}", index + 300);
        }
    }
    
    public static Slot Container(int index) {
        AssertRange(index, 0, 53);
        return new Slot($"container.{index}", index);
    }
    
    public static Slot Enderchest(int index) {
        AssertRange(index, 0, 26);
        return new Slot($"enderchest.{index}", index + 200);
    }
    
    public static Slot Hotbar(int index) {
        AssertRange(index, 0, 8);
        return new Slot($"hotbar.{index}", index);
    }
    
    public static Slot Inventory(int index) {
        AssertRange(index, 0, 26);
        return new Slot($"inventory.{index}", index + 9);
    }
    
    static void AssertRange(int index, int min, int max) {
        if (index < min || index > max) {
            throw new ArgumentOutOfRangeException(nameof(index), index, $"Index must be between {min} and {max}");
        }
    }
}