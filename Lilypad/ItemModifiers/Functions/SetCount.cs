namespace Lilypad.ItemModifiers; 

/// <summary>
/// Sets the stack size. 
/// </summary>
public class SetCount : ItemFunction {
    public NumberProvider Count;
    public bool? Add;
    
    public SetCount(NumberProvider count, bool? add = null) {
        Count = count;
        Add = add;
    }
}