using Lilypad.Extensions;

namespace Lilypad; 

public abstract class LootEntryProvider {
    public List<Predicate>? Conditions;
    public string Type => GetType().Name.Replace("Entry", "").ToSnakeCase();
}