using Lilypad.Helpers;

namespace Lilypad.Examples; 

public class ParkourGame : DatapackBuilder {
    protected override string DefaultNamespace => "parkour";

    protected override void Build() {
        ExampleUtil.SetHelpMessage(Datapack, Example.ParkourGame,
            "<yellow>Stepping on different colors of concrete gives you different effects!</yellow>\n \n" +
            "<aqua>Light blue</aqua> gives <aqua>speed</aqua>\n" +
            "<green>Lime</green> gives <green>jump boost</green>\n" +
            "<gray>Black</gray> gives <gray>blindness</gray>\n" +
            "<red>Red</red> kills you"
        );
        
        SetBlockEffect(Block.LightBlueConcrete, Effect.Speed, 2);
        SetBlockEffect(Block.LimeConcrete, Effect.JumpBoost, 3);
        SetBlockEffect(Block.BlackConcrete, Effect.Blindness, 0);

        Datapack.Functions.Create(f => {
            f.Kill(GetBlockSelector(Block.RedConcrete));
        }).SetTick();
    }

    void SetBlockEffect(Block block, Effect effect, byte amplifier) {
        Datapack.Functions.Create(f => {
            f.Effect(GetBlockSelector(block), effect, 1, amplifier);
        }).SetTick();
    }
    
    Selector GetBlockSelector(Block block) {
        var predicate = Datapack.Predicates.Add(new EntityProperties {
            Predicate = new EntityConditions {
                SteppingOn = new LocationTags(block)
            }
        });
        return Selector.All.Predicate(predicate);
    }
}