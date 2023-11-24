using Lilypad.Extensions;

namespace Lilypad.ItemModifiers; 

public abstract class ItemFunction {
    public List<Predicate>? Conditions;
    
    public string Function => GetType().Name.ToSnakeCase();
}