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
    
    public Function GetInstallFunction() {
        return Functions.GetOrCreate("install").SetLoad();
    }
    
    public Function GetUninstallFunction() {
        return Functions.GetOrCreate("uninstall");
    }
    
    public void RegisterInstallation(Action<Function> install, Action<Function> uninstall) {
        install(GetInstallFunction());
        uninstall(GetUninstallFunction());
    }

    public void RegisterInstallation(object install, object uninstall) {
        GetInstallFunction().Add(install);
        GetUninstallFunction().Add(uninstall);
    }
    
    public void Transpile(in TranspilationOptions options) {
        Transpiler.Transpile(this, options);
    }
}