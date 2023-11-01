using Lilypad.Predicates;

namespace Lilypad;

public class Datapack {
    public int PackFormat { get; set; } = 18;
    public string Description { get; set; } = string.Empty;

    public ResourceCollection<Function> Functions { get; }
    public ResourceCollection<DataResource<Predicate>> Predicates { get; }
    
    public ResourceCollection<Tag<Function>> FunctionTags { get; }

    public Datapack() {
        Functions = new ResourceCollection<Function>(this);
        Predicates = new ResourceCollection<DataResource<Predicate>>(this);
        
        FunctionTags = new ResourceCollection<Tag<Function>>(this);
    }
    
    public void Transpile(string path) {
        Transpiler.Transpile(this, path);
    }
}