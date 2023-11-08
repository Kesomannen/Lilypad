namespace Lilypad;

public abstract class ObjectiveCriterion {
    protected abstract string Name { get; }
    
    public virtual bool CanBeModified => true;

    public override string ToString() {
        return Name;
    }
}