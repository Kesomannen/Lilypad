using Lilypad.Text;

namespace Lilypad.Scoreboards; 

public class Objective {
    public string Name { get; }
    public RichText DisplayName { get; }
    public Criterion Criterion { get; }

    public Objective(
        Datapack datapack,
        string? name = null,
        RichText? displayName = null,
        Criterion? criterion = null,
        DisplaySlotArgument? displaySlot = null
    ) {
        Name = name ?? Names.Get("scoreboard");
        DisplayName = displayName ?? Name;
        Criterion = criterion ?? SingleCriterion.Dummy;

        datapack.RegisterInstallation(
            install => {
                install.Add($"scoreboard objectives add {Name} {Criterion} {DisplayName}");
                if (displaySlot.HasValue) {
                    install.Scoreboard(this).SetDisplay(displaySlot.Value);
                }
            },
            uninstall => uninstall.Add($"scoreboard objectives remove {Name}")
        );
    }

    public override string ToString() {
        return Name;
    }
}