namespace Lilypad.Helpers; 

public abstract class DatapackBuilder {
    protected abstract string DefaultNamespace { get; }
    
    protected Datapack Datapack { get; private set; } = null!;

    public void Create(TranspilationOptions options) {
        Datapack = new Datapack(DefaultNamespace);
        Build();
        Datapack.Transpile(options);
    }
    
    protected abstract void Build();
}