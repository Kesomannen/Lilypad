![258656966-1861f698-765a-4937-88d6-5b7c4b89a683](https://github.com/Kesomannen/Lilypad/assets/113015915/2fc1c41c-fd03-4154-a7de-df7f88812aed)

# Lilypad
.NET library for creating datapacks

## Example

```cs
var datapack = new Datapack();

var nbt = new NBTCompound {
    ["display"] = new NBTCompound {
        ["Name"] = (RichText) "<gold></i>God Sword" // raw JSON text parsing
    }
};

datapack.Functions.Create(f => {
    f.Give("@a", Item.NetheriteSword, nbt);
}).SetLoad(); // adds the function to the load tag

datapack.Transpile(outputPath);
```
Transpiles to:
```mcfunction
give @a netherite_sword{display:{Name:'{"text":"God Sword","color":"gold","italic":false}'}} 1
```
