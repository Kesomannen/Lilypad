namespace Lilypad.ItemModifiers; 

/// <summary>
/// Sets the item's damage value (durability).
/// </summary>
public class SetDamage : ItemFunction {
    /// <summary>
    /// Specifies the damage fraction to set (1.0 is undamaged, 0.0 is zero durability left).
    /// </summary>
    public NumberProvider Damage;

    public bool? Add;
    
    public SetDamage(NumberProvider damage, bool? add = null) {
        Damage = damage;
        Add = add;
    }
}