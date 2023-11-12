namespace Lilypad; 

/// <summary>
/// Applies a predefined bonus formula to the count of the item stack. 
/// </summary>
public class ApplyBonus : ItemFunction {
    public EnumReference<Enchantment> Enchantment;
    public EnumReference<BonusFormula> Formula;
    public Dictionary<string, object> Parameters = new();

    public static ApplyBonus BinomialWithBonusCount(int extra, float probability) => new() {
        Formula = BonusFormula.BinomialWithBonusCount,
        Parameters = {
            ["extra"] = extra,
            ["probability"] = probability
        }
    };
    
    public static ApplyBonus UniformBonusCount(int extra) => new() {
        Formula = BonusFormula.UniformBonusCount,
        Parameters = {
            ["extra"] = extra
        }
    };
    
    public static ApplyBonus OreDrops() => new() {
        Formula = BonusFormula.OreDrops
    };
}

public enum BonusFormula {
    BinomialWithBonusCount,
    UniformBonusCount,
    OreDrops
}