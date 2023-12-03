using Lilypad;
using Lilypad.Helpers;
using Lilypad.Text;

const string outputPath = @"C:\Users\bobbo\AppData\Roaming\PrismLauncher\instances\Default 1.20.1\.minecraft\saves\Testing\datapacks\lilypad-test";

var datapack = new Datapack("test");

datapack.Functions.Create(f => {
    f.Execute().As(Selector.All).At("@s").Run(f => {
        f.RaycastEntity(Selector.Entites.Distance((0, 2)), (f, player) => {
                player.Type(Entity.Player);
                f.Title(player, TitleType.Actionbar, "<green>You're looking at <entity @s>!");
            }).OnMiss(f => {
                f.Title("@s", TitleType.Actionbar, "<red>You're not looking at anything!");
            })
            .StepForward(1, 12)
            .BlockedBySolids();
    });
}).SetLoop(0.2f);

datapack.Transpile(outputPath);