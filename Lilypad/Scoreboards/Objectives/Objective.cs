using Lilypad.Helpers;
using Lilypad.Predicates;
using Lilypad.Text;

namespace Lilypad.Scoreboards; 

public class Objective : Resource {
    public RichText? DisplayName { get; set; }
    public Criterion? Criterion { get; set; }
    public DisplaySlotArgument? DisplaySlot { get; set; }
    public EnumReference<ObjectiveRenderType>? RenderType { get; set; }
    
    internal Objective(string name, Datapack datapack) : base(name, datapack) {
        datapack.RegisterInstallation(
            install: f => f.Add(_ => {
                DisplayName ??= Name;
                Criterion ??= SingleCriterion.Dummy;
                
                f.Add($"scoreboard objectives add {Name} {Criterion} {DisplayName}");
                var scoreboard = f.Scoreboard(this);
                if (DisplaySlot.HasValue) {
                    scoreboard.SetDisplay(DisplaySlot.Value);
                }
                if (RenderType.HasValue) {
                    scoreboard.SetRenderType(RenderType.Value);
                }
            }),
            uninstall: f => {
                f.Add($"scoreboard objectives remove {Name}");
            }
        );
    }

    protected override string GetLocation() {
        return $"{Namespace}_{Name}";
    }
    
    public Objective SetDisplayName(RichText displayName) {
        DisplayName = displayName;
        return this;
    }
    
    public Objective SetCriterion(Criterion criterion) {
        Criterion = criterion;
        return this;
    }
    
    public Objective SetDisplaySlot(DisplaySlotArgument displaySlot) {
        DisplaySlot = displaySlot;
        return this;
    }
    
    public Objective SetRenderType(EnumReference<ObjectiveRenderType> renderType) {
        RenderType = renderType;
        return this;
    }

    public Function CreateRewardFunction(Argument<Range<int>> range, int resetValue, Action<Function> build) {
        return Datapack.Functions.Create(f => {
            var selector = Selector.All.Scores((this, range));
            f.Execute().As(selector).Run(g => {
                g.Scoreboard(this).Set("@s", resetValue);
                build(g);
            });
        }).SetTick();
    }

    public Function CreateRewardFunction(Action<Function> build) {
        return CreateRewardFunction("1..", 0, build);
    }
}