using Lilypad.Extensions;
using Newtonsoft.Json;

namespace Lilypad; 

public abstract class Predicate {
    [JsonProperty]
    protected virtual string Condition => GetType().Name.ToSnakeCase();
    
    public Predicate Inverted() {
        return new Inverted(this);
    }
}