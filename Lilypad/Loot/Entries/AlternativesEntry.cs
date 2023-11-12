namespace Lilypad; 

/// <summary>
/// Applies the first successful (where all conditions are met) entry provider to the pool. 
/// </summary>
public class AlternativesEntry : LootEntryProvider {
    public List<LootEntryProvider>? Children;
}