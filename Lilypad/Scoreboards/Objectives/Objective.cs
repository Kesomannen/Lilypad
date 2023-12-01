using Lilypad.Helpers;
using Lilypad.Text;

namespace Lilypad; 

public class Objective : Resource {
    public JsonText? DisplayName { get; set; }
    public ObjectiveCriterion? Criterion { get; set; }
    public DisplaySlotArgument? DisplaySlot { get; set; }
    public EnumReference<RenderType>? RenderType { get; set; }

    public override string Location => $"{Namespace}_{Name}";

    internal Objective(string name, string @namespace, Datapack datapack) : base(name, @namespace, datapack) {
        datapack.RegisterInstallation(
            install: f => f.Add(_ => {
                DisplayName ??= Name;
                Criterion ??= SingleCriterion.Dummy;
                
                f.Add($"scoreboard objectives add {this} {Criterion} {DisplayName}");
                var command = f.Scoreboard(this);
                if (DisplaySlot.HasValue) {
                    command.SetDisplay(DisplaySlot.Value);
                }
                if (RenderType.HasValue) {
                    command.SetRenderType(RenderType.Value);
                }
            }),
            uninstall: f => f.Add(_ => {
                f.Add($"scoreboard objectives remove {this}");
            })
        );
    }
    
    public Objective SetDisplayName(JsonText displayName) {
        DisplayName = displayName;
        return this;
    }
    
    public Objective SetCriterion(ObjectiveCriterion criterion) {
        Criterion = criterion;
        return this;
    }
    
    public Objective SetDisplaySlot(DisplaySlotArgument displaySlot) {
        DisplaySlot = displaySlot;
        return this;
    }
    
    public Objective SetRenderType(EnumReference<RenderType> renderType) {
        RenderType = renderType;
        return this;
    }

    public Function CreateRewardFunction(IntRange range, int resetValue, Action<Function> build) {
        return Datapack.Functions.Create(f => {
            var selector = Selector.All.Scores((this, range));
            f.Execute().As(selector).Run(g => {
                g.Scoreboard(this).Set("@s", resetValue);
                build(g);
            });
        }).SetTick();
    }

    public Function CreateRewardFunction(Action<Function> build) {
        return CreateRewardFunction((1, int.MaxValue), 0, build);
    }
}