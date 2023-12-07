using Lilypad;

namespace Lilypad.Helpers; 

public static class CustomHeads {
    public static NBT FromPlayer(string name) {
        return new NBT {
            ["SkullOwner"] = new NBT {
                ["Name"] = name
            }
        };
    }
    
    public static NBT FromTexture(string texture) {
        return new NBT {
            ["SkullOwner"] = new NBT {
                ["Id"] = Uuid.New(),
                ["Properties"] = new NBT {
                    ["textures"] = new[] {
                        ("Value", texture)
                    }
                }
            }
        };
    }
}