using Lilypad.NBT;

namespace Lilypad.Helpers; 

public static class CustomHeads {
    public static NBTCompound FromPlayer(string name) {
        return new NBTCompound {
            ["SkullOwner"] = new NBTCompound {
                ["Name"] = name
            }
        };
    }
    
    public static NBTCompound FromTexture(string texture) {
        return new NBTCompound {
            ["SkullOwner"] = new NBTCompound {
                ["Id"] = Uuid.New(),
                ["Properties"] = new NBTCompound {
                    ["textures"] = new NBTCompound[] {
                        ("Value", texture)
                    }
                }
            }
        };
    }
}