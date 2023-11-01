using Lilypad.Extensions;
using Newtonsoft.Json;

namespace Lilypad.Predicates; 

public abstract class Predicate {
    [JsonProperty]
    string Condition => GetType().Name.ToSnakeCase();
    
    public static implicit operator Predicate(EntityConditions predicate) {
        return new EntityProperties { Predicate = predicate };
    }
}