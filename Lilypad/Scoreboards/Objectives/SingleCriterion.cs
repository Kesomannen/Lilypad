namespace Lilypad; 

public class SingleCriterion : ObjectiveCriterion {
    protected override string Name { get; }
    public override bool CanBeModified { get; }

    public static readonly SingleCriterion Dummy = new("dummy");
    public static readonly SingleCriterion Trigger = new("trigger");
    public static readonly SingleCriterion DeathCount = new("deathCount");
    public static readonly SingleCriterion PlayerKillCount = new("playerKillCount");
    public static readonly SingleCriterion TotalKillCount = new("totalKillCount");
    
    public static readonly SingleCriterion Health = new("health", false);
    public static readonly SingleCriterion Food = new("food", false);
    public static readonly SingleCriterion Air = new("air", false);
    public static readonly SingleCriterion Armor = new("armor", false);
    public static readonly SingleCriterion Level = new("level", false);
    public static readonly SingleCriterion Experience = new("xp", false);
    
    SingleCriterion(string name, bool canBeModified = true) {
        Name = name;
        CanBeModified = canBeModified;
    }
    
    public override string ToString() {
        return Name;
    }
}