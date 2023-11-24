using Lilypad.ItemModifiers;

namespace Lilypad; 

public class LootPool {
    public List<Predicate>? Predicates;
    public List<ItemFunction>? Functions;
    public NumberProvider Rolls;
    public NumberProvider? BonusRolls;
    public List<LootEntryProvider> Entries = new();
}