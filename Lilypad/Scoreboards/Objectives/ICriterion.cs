namespace Lilypad.Scoreboards;

public abstract class Criterion {
    protected abstract string Name { get; }
    
    public virtual bool CanBeModified => true;

    public override string ToString() {
        return Name;
    }
}