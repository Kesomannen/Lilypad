namespace Lilypad.Templates; 

public abstract class NBTTemplate : ICustomNBTSerializer {
    public abstract NBTCompound ToNBT();
    
    public string Serialize() {
        return ToNBT().Serialize();
    }
}