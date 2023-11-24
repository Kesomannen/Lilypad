using Lilypad;
using Lilypad.Helpers;

const string outputPath = @"C:\Users\bobbo\AppData\Roaming\PrismLauncher\instances\Default 1.20.1\.minecraft\saves\Testing\datapacks\lilypad-test";

var datapack = new Datapack("test");

datapack.Functions.Create(f => {
    f.Execute().As("@a").At("@s").Run(f => {
        f.RaycastForward(8, Selector.Entites, f => f.Kill());
    });
}).SetTick();

datapack.Transpile(outputPath);