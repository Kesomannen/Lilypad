namespace Lilypad.Helpers; 

public abstract class DatapackBuilder {
    protected Datapack Datapack { get; private set; } = null!;

    public void Create(TranspilationOptions options) {
        Datapack = new Datapack();
        Build();
        Datapack.Transpile(options);
    }
    
    protected abstract void Build();
}