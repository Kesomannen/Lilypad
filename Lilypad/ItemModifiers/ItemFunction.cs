using Lilypad.Extensions;

namespace Lilypad; 

public abstract class ItemFunction {
    public List<Predicate>? Conditions;
    
    public string Function => GetType().Name.ToSnakeCase();
}