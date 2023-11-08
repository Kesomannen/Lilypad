using Lilypad;
using Lilypad.Recipes;
using Newtonsoft.Json;

namespace Lilypad; 

public class Criterion {
    [JsonIgnore]
    public string Name { get; set; }

    [JsonProperty("trigger")]
    readonly EnumReference<Trigger> _trigger;

    [JsonProperty("conditions")]
    Dictionary<string, object> _conditions;

    Criterion(Trigger trigger, params (string, object?)[] conditions) {
        _trigger = trigger;
        Name = _trigger.ToString();
        
        _conditions = conditions
            .Where(tuple => tuple.Item2 != null && (tuple.Item2 is not Array array || array.Length > 0))
            .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2!);
    }
    
    public Criterion AddPlayerPredicates(params Predicate[] predicates) {
        if (!_conditions.ContainsKey("player")) {
            _conditions.Add("player", new List<Predicate>());
        }
        
        ((List<Predicate>) _conditions["player"]).AddRange(predicates);
        return this;
    }
    
    public Criterion AddPlayerConditions(params EntityConditions[] conditions) {
        var predicates = conditions
            .Select(c => new EntityProperties { Predicate = c })
            .Cast<Predicate>()
            .ToArray();
        return AddPlayerPredicates(predicates);
    }
    
    public static Criterion AllayDropItemOnBlock(params Predicate[] locationPredicates) {
        return new Criterion(Trigger.AllayDropItemOnBlock, ("location", locationPredicates));
    }
    
    public static Criterion AvoidVibration() {
        return new Criterion(Trigger.AvoidVibration);
    }

    public static Criterion BeeNestDestroyed(EnumReference<Block>? block = null, ItemConditions? item = null, Range<int>? beesInside = null) {
        return new Criterion(Trigger.BeeNestDestroyed, ("block", block), ("item", item), ("num_bees_inside", beesInside));
    }
    
    public static Criterion BredAnimals(Predicate[]? child = null, Predicate[]? parent = null, Predicate[]? partner = null) {
        return new Criterion(Trigger.BredAnimals, ("child", child), ("parent", parent), ("partner", partner));
    }
    
    public static Criterion BrewedPotion() {
        return new Criterion(Trigger.BrewedPotion);
    }
    
    public static Criterion BrewedPotion(EnumReference<Potion> potion) {
        return new Criterion(Trigger.BrewedPotion, ("potion", (int) potion.Value));
    }
    
    public static Criterion ChangedDimension(EnumReference<Dimension>? from = null, EnumReference<Dimension>? to = null) {
        return new Criterion(Trigger.ChangedDimension, ("from", from), ("to", to));
    }
    
    public static Criterion ChanneledLightning(params Predicate[][] victims) {
        return new Criterion(Trigger.ChanneledLightning, ("victims", victims));
    }
    
    public static Criterion ConstructBeacon(Range? level = null) {
        return new Criterion(Trigger.ConstructBeacon, ("level", level));
    }
    
    public static Criterion ConsumeItem(ItemConditions? item = null) {
        return new Criterion(Trigger.ConsumeItem, ("item", item));
    }
    
    public static Criterion CuredZombieVillager(Predicate[]? villager = null, Predicate[]? zombie = null) {
        return new Criterion(Trigger.CuredZombieVillager, ("villager", villager), ("zombie", zombie));
    }
    
    public static Criterion EffectsChanged() {
        throw new NotImplementedException();
    }
    
    public static Criterion EnchantedItem(ItemConditions? item = null, Range? levels = null) {
        return new Criterion(Trigger.EnchantedItem, ("item", item), ("levels", levels));
    }

    public static Criterion EnterBlock(Block? block = null, BlockProperties? properties = null) {
        return new Criterion(Trigger.EnterBlock, ("block", block), ("state", properties));
    }
    
    public static Criterion EntityHurtPlayer(DamageTypeTags? damageTags = null) {
        return new Criterion(Trigger.EntityHurtPlayer, ("damage", damageTags));
    }
    
    public static Criterion EntityKilledPlayer(DamageTypeTags? killingBlow = null, params Predicate[] entity) {
        return new Criterion(Trigger.EntityKilledPlayer, ("entity", entity), ("killing_blow", killingBlow));
    }
    
    public static Criterion FallFromHeight(LocationTags? startPosition = null, DistanceTags? distance = null) {
        return new Criterion(Trigger.FallFromHeight, ("start_position", startPosition), ("distance", distance));
    }
    
    public static Criterion FilledBucket(ItemConditions? item = null) {
        return new Criterion(Trigger.FilledBucket, ("item", item));
    }
    
    public static Criterion FishingRodHooked(Predicate[]? pulledEntity = null, ItemConditions? caughtItem = null, ItemConditions? fishingRod = null) {
        return new Criterion(Trigger.FishingRodHooked, ("entity", pulledEntity), ("item", caughtItem), ("rod", fishingRod));
    }
    
    public static Criterion HeroOfTheVillage() {
        return new Criterion(Trigger.HeroOfTheVillage);
    }
    
    public static Criterion Impossible() {
        return new Criterion(Trigger.Impossible);
    }
    
    public static Criterion InventoryChanged(params ItemConditions[] inventoryItems) {
        return new Criterion(Trigger.InventoryChanged, ("items", inventoryItems));
    }
    
    public static Criterion InventoryChanged(Slots? slots, params ItemConditions[] inventoryItems) {
        return new Criterion(Trigger.InventoryChanged, ("slots", slots), ("items", inventoryItems));
    }
    
    public static Criterion ItemDurabilityChanged(ItemConditions? item = null, Range? durability = null, Range? delta = null) {
        return new Criterion(Trigger.ItemDurabilityChanged, ("item", item), ("durability", durability), ("delta", delta));
    }

    public static Criterion ItemUsedOnBlock(params Predicate[] predicates) {
        return new Criterion(Trigger.ItemUsedOnBlock, ("location", predicates));
    }

    public static Criterion KillMobNearSculkCatalyst(Predicate[]? killedEntity = null, DamageTypeTags? killingBlow = null) {
        return new Criterion(Trigger.KillMobNearSculkCatalyst, ("entity", killedEntity), ("killing_blow", killingBlow));
    }
    
    public static Criterion KilledByCrossbow(params Predicate[][] victims) {
        return new Criterion(Trigger.KilledByCrossbow, ("victims", victims));
    }
    
    public static Criterion KilledByCrossbow(Range? uniqueTypes = null, params Predicate[][] victims) {
        return new Criterion(Trigger.KilledByCrossbow, ("victims", victims), ("unique_entity_types", uniqueTypes));
    }
    
    public static Criterion Levitation(DistanceTags? distance = null, Range<int>? durationSeconds = null) {
        Range<int>? range = durationSeconds is null ? null : 
            new Range<int>(durationSeconds.Value.Min * 20, durationSeconds.Value.Max * 20);
        
        return new Criterion(Trigger.Levitation, ("distance", distance), ("duration", range));
    }
    
    public static Criterion LightningStrike(Predicate[]? lightningBolt = null, Predicate[]? bystander = null) {
        return new Criterion(Trigger.LightningStrike, ("lightning", lightningBolt), ("bystander", bystander));
    }
    
    public static Criterion Location() {
        return new Criterion(Trigger.Location);
    }
    
    public static Criterion NetherTravel(LocationTags? start = null, DistanceTags? distance = null) {
        return new Criterion(Trigger.NetherTravel, ("start_position", start), ("distance", distance));
    }
    
    public static Criterion PlacedBlock(params Predicate[] locationPredicates) {
        return new Criterion(Trigger.PlacedBlock, ("location", locationPredicates));
    }
    
    public static Criterion PlayerGeneratesContainerLoot(LootTable lootTable) {
        return new Criterion(Trigger.PlayerGeneratesContainerLoot, ("loot_table", lootTable));
    }

    public static Criterion PlayerHurtEntity(DamageTypeTags? damage = null, params Predicate[] entity) {
        return new Criterion(Trigger.PlayerHurtEntity, ("entity", entity), ("damage", damage));
    }
    
    public static Criterion PlayerInteractedWithEntity(ItemConditions? item = null, params Predicate[] entity) {
        return new Criterion(Trigger.PlayerInteractedWithEntity, ("entity", entity), ("item", item));
    }

    public static Criterion PlayerKilledEntity(DamageTypeTags? killingBlow = null, params Predicate[] entity) {
        return new Criterion(Trigger.PlayerKilledEntity, ("entity", entity), ("damage", killingBlow));
    }
    
    public static Criterion RecipeCrafted(Reference<Recipe> recipe, params Predicate[] ingredients) {
        return new Criterion(Trigger.RecipeCrafted, ("recipe", recipe), ("ingredients", ingredients));
    }
    
    public static Criterion RecipeUnlocked(Reference<Recipe> recipe) {
        return new Criterion(Trigger.RecipeUnlocked, ("recipe", recipe));
    }
    
    public static Criterion RideEntityInLaval(LocationTags? start = null, DistanceTags? distance = null) {
        return new Criterion(Trigger.RideEntityInLava, ("start_position", start), ("distance", distance));
    }
    
    public static Criterion ShotCrossbow(ItemConditions? item = null) {
        return new Criterion(Trigger.ShotCrossbow, ("item", item));
    }
    
    public static Criterion SleptInBed(LocationTags? location = null) {
        return new Criterion(Trigger.SleptInBed);
    }
    
    public static Criterion SlideDownBlock(EnumReference<Block>? block = null, BlockProperties? properties = null) {
        return new Criterion(Trigger.SlideDownBlock, ("block", block), ("state", properties));
    }
    
    public static Criterion StartedRiding() {
        return new Criterion(Trigger.StartedRiding);
    }
    
    public static Criterion SummonedEntity(params Predicate[]? predicates) {
        return new Criterion(Trigger.SummonedEntity, ("entity", predicates));
    }
    
    public static Criterion TameAnimal(params Predicate[]? predicates) {
        return new Criterion(Trigger.TameAnimal, ("entity", predicates));
    }
    
    public static Criterion TargetHit(Range<int>? signalStrength = null, params Predicate[] projectile) {
        return new Criterion(Trigger.TargetHit, ("signal_strength", signalStrength), ("projectile", projectile));
    }
    
    public static Criterion ThrownItemPickedUpByEntity(ItemConditions? item = null, params Predicate[] entity) {
        return new Criterion(Trigger.ThrownItemPickedUpByEntity, ("item", item), ("entity", entity));
    }
    
    public static Criterion ThrownItemPickedUpByPlayer(ItemConditions? item = null, params Predicate[] entity) {
        return new Criterion(Trigger.ThrownItemPickedUpByPlayer, ("item", item), ("player", entity));
    }
    
    public static Criterion Tick() {
        return new Criterion(Trigger.Tick);
    }
    
    public static Criterion UsedEnderEye(Range<double>? distance = null) {
        return new Criterion(Trigger.UsedEnderEye, ("distance", distance));
    }
    
    public static Criterion UsedTotem(ItemConditions? item = null) {
        return new Criterion(Trigger.UsedTotem, ("item", item));
    }
    
    public static Criterion UsingItem(ItemConditions? item = null) {
        return new Criterion(Trigger.UsingItem, ("item", item));
    }
    
    public static Criterion VillagerTrade(ItemConditions? item = null, params Predicate[] villager) {
        return new Criterion(Trigger.VillagerTrade, ("item", item), ("villager", villager));
    }
    
    public static Criterion VoluntaryExile() {
        return new Criterion(Trigger.VoluntaryExile);
    }
}