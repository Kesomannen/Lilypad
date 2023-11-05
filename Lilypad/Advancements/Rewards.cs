using Lilypad.Loot;
using Lilypad.Recipes;

namespace Lilypad.Advancements; 

public class Rewards {
    public List<Reference<Recipe>>? Recipes;
    public List<Reference<LootTable>>? LootTables;
    public Reference<Function>? Function;
    public double? Experience;
}