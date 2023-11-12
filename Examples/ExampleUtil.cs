using Lilypad.Text;

namespace Lilypad.Examples; 

internal static class ExampleUtil {
    public static string PathInput(string message, Func<string, bool>? validator = null) {
        validator ??= _ => true;
        Console.Clear();
        Console.WriteLine($"{message}:");
    
        while (true) {
            var path = Console.ReadLine();
            if (Directory.Exists(path) && validator(path)) return path;
        
            Console.Clear();
            Console.Write($"{message}:\nInvalid path, try again\n");
        }
    }

    public static T ChoiceInput<T>(string message, IReadOnlyList<T> choices) {
        for (var i = 0; i < choices.Count; i++) {
            message += $"\n{i + 1}: {choices[i]}";
        }
    
        Console.Clear();
        Console.WriteLine(message);
    
        while (true) {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var index) && index - 1 >= 0 && index - 1 < choices.Count) {
                return choices[index - 1];
            }
            Console.Clear();
            Console.Write($"{message}\nInvalid choice, try again\n");
        }
    }
    
    public static bool YesNoInput(string message) {
        Console.Clear();
        Console.WriteLine($"{message} (y/n)");
    
        while (true) {
            var input = Console.ReadLine();
            switch (input) {
                case "y":
                    return true;
                case "n":
                    return false;
                default:
                    Console.Clear();
                    Console.Write($"{message} (y/n)\nInvalid choice, try again\n");
                    break;
            }
        }
    }

    public static bool TryFindMinecraftDirectory(out string path) {
        var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        path = Path.Combine(appdata, ".minecraft");
        return Directory.Exists(path);
    }
    
    public static void SetHelpMessage(Datapack datapack, Example example, JsonText message) {
        var text = new JsonText(
            $"\n----- <green>Lilypad Example (<yellow>{example}</yellow>)</green> -----\n \n",
            message,
            "\n \n--------------------------------------\n"
        );
        var helperFunction = datapack.Functions.Create("help").Tellraw(text);
        datapack.Functions.Create(f => {
            f.Tellraw("@a",
                $"<green>Lilypad </green>| Example <yellow>{example}</yellow> loaded! " +
                  $"<u><run {helperFunction.Location}>Click here</u></run> " +
                  $"or run <i>'/function {helperFunction.Location}'</i> for more information."
            );
        });
    }
}