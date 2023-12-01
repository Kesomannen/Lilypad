using Lilypad;
using Lilypad.Helpers;
using Lilypad.Text;

const string outputPath = @"C:\Users\bobbo\AppData\Roaming\PrismLauncher\instances\Default 1.20.1\.minecraft\saves\Testing\datapacks\lilypad-test";

var datapack = new Datapack("test");

datapack.Functions.Create(f => {
    var objective = datapack.Objectives.Create();
    var variable = new ScoreVariable("test", objective);

    f.Increment(variable);
    f.Operation(variable, "-=", 2);
    f.Operation(variable, "/=", 5);
    
    f.Tellraw("@a", $"Result: <{variable}>");
}).SetTick();

datapack.Transpile(outputPath);