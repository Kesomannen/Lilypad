using Lilypad;
using Lilypad.Helpers;
using Lilypad.Scoreboards;

const string outputPath = @"C:\Users\bobbo\AppData\Roaming\PrismLauncher\instances\Default 1.20.1\.minecraft\saves\Testing\datapacks\lilypad-example";

var datapack = new Datapack();

var objective = datapack.Objectives.Create();
objective.DisplaySlot = DisplaySlot.Sidebar;

datapack.Functions.Create("test/scoreboard_operations", f => {
    f.Scoreboard(objective)
        .Set("A", 1)
        .Operation("Result", "Assign", "A")
        .Operation("Result", "Add", 2);
}).SetLoad();

datapack.Transpile(new TranspilationOptions(outputPath));