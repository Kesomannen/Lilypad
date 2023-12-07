using Lilypad;
using Lilypad.Helpers;

const string outputPath = @"C:\Users\22boro\AppData\Roaming\.minecraft\saves\Datapack\datapacks\lilypad-test";

var datapack = new Datapack("test");

new Gamerules {
    DoEntityDrops = false,
    CommandBlockOutput = false
}.Generate(datapack);

datapack.Functions.Create(f => {
    f.Execute().AsAt(Selector.All.Conditions(f, new EntityConditions {
        Equipment = new EquipmentTags("mainhand", new ItemConditions {
            Nbt = ("kill_wand", true)
        })
    })).Run(f => {
        f.DoRaycast(new EntityRaycast(f)
            .SetDetectDistance(3)
            .SetCancelOnHit(false)
            .OnHit((f, _) => f.Kill())
            .BlockedBySolids()
            .StepForward(2, 12)
            .Visualize(Particle.Portal)
        );
    });
}).SetTick();

datapack.Functions.Create("give", f => {
    f.Give("@s", new ItemNBT(Item.Stick) {
        Name = "</i><dark_purple>Kill Wand",
        Lore = new() {
            "</i><gray>Kills everything in its path"
        },
        Enchantments = ItemNBT.VisualOnlyEnchants,
        AdditionalTag = ("kill_wand", true)
    });
});

datapack.Transpile(outputPath);