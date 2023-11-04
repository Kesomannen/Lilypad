using Lilypad.Helpers;
using Lilypad.Text;

namespace Lilypad.Scoreboards; 

public class Team {
    public string Name { get; }
    public RichText DisplayName { get; }
    
    public Team(
        Datapack datapack,
        string? name = null,
        RichText? displayName = null
    ) {
        Name = name ?? Names.Get("scoreboard");
        DisplayName = displayName ?? Name;

        datapack.RegisterInstallation(
            install => {
                
            },
            uninstall => uninstall.Add($"scoreboard objectives remove {Name}")
        );
    }

    public override string ToString() {
        return Name;
    }
}