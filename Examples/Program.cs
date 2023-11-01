using Lilypad;
using Lilypad.Predicates;

const string outputPath = @"C:\Users\bobbo\AppData\Roaming\PrismLauncher\instances\Default 1.20.1\.minecraft\saves\Testing\datapacks\lilypad-example";

var datapack = new Datapack();

EffectOnBlock(Block.LightBlueConcrete, Effect.Speed);
EffectOnBlock(Block.LimeConcrete, Effect.JumpBoost);
EffectOnBlock(Block.BlackConcrete, Effect.Blindness);

datapack.Transpile(outputPath);
return;

void EffectOnBlock(Block block, Effect effect, int amplifier = 3) {
    var predicate = datapack.Predicates.Add(
        new EntityConditions {
            SteppingOn = block
        }
    );

    datapack.Functions.Create(f => {
        f.Effect(Selector.All.Predicate(predicate), effect, 1, amplifier);
    }).SetTick();
}