using Lilypad;
using Lilypad.Helpers;
using static Lilypad.Selector;

const string outputPath = @"C:\Users\22boro\AppData\Roaming\.minecraft\saves\Datapack\datapacks\lilypad-test";

var datapack = new Datapack("test");
    
datapack.Functions.Create(f => {
    f.Execute().As(Entites.Type("cow")).At("@s").Run(f => {
        f.RaycastEntity(Entites.NotType("player").Distance((10, 0)), f => {
            f.Kill();
        }).SetBlockedByNonAir();
    });
}).SetTick();

datapack.Transpile(outputPath);