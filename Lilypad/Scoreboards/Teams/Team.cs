using Lilypad.Helpers;
using Lilypad.Text;

namespace Lilypad; 

public class Team {
    public string Name { get; }
    public JsonText DisplayName { get; }
    
    public Team(
        Datapack datapack,
        string? name = null,
        JsonText? displayName = null
    ) {
        Name = name ?? Names.Get("scoreboard");
        DisplayName = displayName ?? Name;

        datapack.RegisterInstallation(
            install => {
                
            },
            uninstall => uninstall.Add($"scoreboard objectives remove {Name}")
        );
    }

    public override string ToString() => Name;
}