using Lilypad.Extensions;
using Newtonsoft.Json;

namespace Lilypad.ItemModifiers; 

public abstract class ItemFunction {
    public List<Predicate>? Conditions;
    
    [JsonProperty("function")]
    public virtual string FunctionName => GetType().Name.ToSnakeCase();
}