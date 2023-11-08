using System.Reflection;
using Lilypad.Helpers;

namespace Lilypad; 

public class Gamerules {
    public bool? AnnounceAdvancements;
    public bool? BlockExplosionDropDecay;
    public bool? CommandBlockOutput;
    public int? CommandModificationBlockLimit;
    public bool? DisableElytraMovementCheck;
    public bool? DisableRaids;
    public bool? DoDaylightCycle;
    public bool? DoEntityDrops;
    public bool? DoFireTick;
    public bool? DoInsomnia;
    public bool? DoImmediateRespawn;
    public bool? DoLimitedCrafting;
    public bool? DoMobLoot;
    public bool? DoMobSpawning;
    public bool? DoPatrolSpawning;
    public bool? DoTileDrops;
    public bool? DoTraderSpawning;
    public bool? DoVinesSpread;
    public bool? DoWeatherCycle;
    public bool? DoWardenSpawning;
    public bool? DrowningDamage;
    public bool? EnderPearlsVanishOnDeath;
    public bool? FallDamage;
    public bool? FireDamage;
    public bool? ForgiveDeadPlayers;
    public bool? FreezeDamage;
    public bool? GlobalSoundEvents;
    public bool? KeepInventory;
    public bool? LavaSourceConversion;
    public bool? LogAdminCommands;
    public int? MaxCommandChainLength;
    public int? MaxEntityCramming;
    public bool? MobExplosionDropDecay;
    public bool? MobGriefing;
    public bool? NaturalRegeneration;
    public int? PlayersSleepingPercentage;
    public int? RandomTickSpeed;
    public bool? ReducedDebugInfo;
    public bool? SendCommandFeedback;
    public bool? ShowDeathMessages;
    public int? SnowAccumulationHeight;
    public int? SpawnRadius;
    public bool? SpectatorsGenerateChunks;
    public bool? TntExplodes;
    public bool? TntExplosionDropDecay;
    public bool? UniversalAnger;
    public bool? WaterSourceConversion;

    public void Generate(Datapack datapack) {
        var install = datapack.GetInstallFunction();
        
        var properties = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties) {
            var lowercase = char.ToLower(property.Name[0]) + property.Name[1..];
            Set(lowercase, property.GetValue(this));
        }
        return;

        void Set<T>(string name, T? value) {
            if (value != null) {
                install.Gamerule(name, value.ToString()!.ToLower());
            }
        }
    }
}