using Lilypad.Examples;
using Lilypad;
using Lilypad.Helpers;

var examples = new Dictionary<Example, DatapackBuilder> {
    { Example.ParkourGame, new ParkourGame() }
};

var foundPath = ExampleUtil.TryFindMinecraftDirectory(out var minecraftPath);
var rightOne = foundPath && ExampleUtil.YesNoInput($"Found minecraft installation at '{minecraftPath}'\nUse it?");

if (!rightOne || !foundPath) {
    var message = foundPath ? "" : "Could not find minecraft installation.\n";
    
    minecraftPath = ExampleUtil.PathInput(
        $"{message}Enter path to the '.minecraft' directory", 
        path => Directory.Exists(Path.Join(path, "saves"))
    );
}

Console.Clear();

var savesPath = Path.Join(minecraftPath, "saves");
var worlds = Directory.GetDirectories(savesPath);

if (worlds.Length == 0) {
    Console.WriteLine("No worlds found! Create a world and try again.");
    return;
}

var world = ExampleUtil.ChoiceInput("Choose a world to add an example", worlds.Select(Path.GetFileName).ToArray());
var example = ExampleUtil.ChoiceInput("Choose an example to add", examples.Keys.ToArray());

var options = new TranspilationOptions {
    OutputPath = Path.Join(savesPath, world, "datapacks", $"Lilypad Example ({example})"),
    Overwrite = true
};
examples[example].Create(options);

Console.WriteLine("Datapack added! Run '/reload' in the world to load the datapack");

internal enum Example {
    ParkourGame
}