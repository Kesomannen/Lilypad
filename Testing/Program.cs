using Lilypad;
using Lilypad.Helpers;
using Lilypad.Text;
using Attribute = Lilypad.Attribute;

const string outputPath = @"C:\Users\bobbo\AppData\Roaming\PrismLauncher\instances\Default 1.20.1\.minecraft\saves\Testing\datapacks\lilypad-example";

Culture.Initialize();
var datapack = new Datapack("test");

new Gamerules {
    KeepInventory = true,
    DoImmediateRespawn = true,
    DoEntityDrops = false,
}.Generate(datapack);

datapack.Functions.Create(f => {
    f.Execute().As("@a").Run(g => {
        var maxHealth = Temp.Get(datapack);
        var health = new DataVariable("entity @s", "int", "Health");

        g.SetVariable(maxHealth, h => h.Attribute(Attribute.MaxHealth).Get());
        var emptyHearts = g.Evaluate($"({maxHealth} - {health}) / 2", health, maxHealth);
        g.Title("@s", TitleType.Actionbar, $"Missing hearts: <red><{emptyHearts}>");
    });
}).SetTick();

datapack.Transpile(new TranspilationOptions(outputPath));