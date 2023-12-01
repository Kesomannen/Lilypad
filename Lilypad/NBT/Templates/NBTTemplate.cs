namespace Lilypad; 

public abstract class NBTTemplate : ICustomNBTSerializer {
    public abstract NBT ToNBT();
    
    public string Serialize() {
        return ToNBT().Serialize();
    }

    public override string ToString() => Serialize();
}