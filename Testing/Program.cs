using Lilypad;
using Lilypad.Helpers;
using Lilypad.Text;

const string outputPath = @"C:\Users\bobbo\AppData\Roaming\PrismLauncher\instances\Default 1.20.1\.minecraft\saves\Testing\datapacks\lilypad-example";

var datapack = new Datapack("test");

datapack.Functions.Create(f => {
    f.For(0, 10, (f, i) => {
        f.Tellraw("@a", $"Number: <{i}>");
    });
}).SetLoad();

datapack.Transpile(new TranspilationOptions(outputPath));