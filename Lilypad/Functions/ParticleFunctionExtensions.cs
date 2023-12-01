using Lilypad.Extensions;

namespace Lilypad; 

/// <summary>
/// Provides function extension methods for the /particle command.
/// </summary>
public static class ParticleFunctionExtensions {
    static Function Particle(
        this Function function,
        string particle,
        Vector3 position,
        Vector3 delta,
        float speed,
        int count,
        bool force = false,
        Argument<Selector>? viewers = null
    ) {
        var mode = force ? "force" : "normal";
        return function.Add($"particle {particle} {position} {delta} {speed} {count} {mode} {viewers.ToStringOrEmpty()}");
    }

    public static Function Particle(
        this Function function, 
        EnumReference<Particle> particle,
        Vector3? position = null
    ) {
        AssertNotBlock(particle);
        return function.Add($"particle {particle} {position.ToStringOrEmpty()}");
    }

    public static Function Particle(
        this Function function,
        EnumReference<Particle> particle,
        Vector3 position,
        Vector3 delta,
        float speed,
        int count,
        bool force = false,
        Argument<Selector>? viewers = null
    ) {
        AssertNotBlock(particle);
        return function.Particle(particle.ToString(), position, delta, speed, count, force, viewers);
    }

    public static Function BlockParticle(
        this Function function,
        EnumReference<Block> block,
        Vector3 position,
        Vector3 delta,
        float speed,
        int count,
        bool force = false,
        Argument<Selector>? viewers = null
    ) {
        return function.Particle($"block {block}", position, delta, speed, count, force, viewers);
    }
    
    static void AssertNotBlock(EnumReference<Particle> particle) {
        if (particle.Value == Lilypad.Particle.Block) {
            throw new ArgumentException("Block particles must be used with BlockParticle instead of Particle.");
        }
    }
}

/// <summary>
/// Particle types that can be used with the /particle command.
/// See https://minecraft.wiki/w/Particles#Types_of_particles.
/// </summary>
public enum Particle {
    AmbientEntityEffect,
    AngryVillager,
    Ash,
    Block,
    Blockdust,
    BlockMarker,
    Bubble,
    BubbleColumnUp,
    BubblePop,
    CampfireCosySmoke,
    CampfireSignalSmoke,
    CherryLeaves,
    Cloud,
    Composter,
    CrimsonSpore,
    Crit,
    CurrentDown,
    DamageIndicator,
    Dolphin,
    DragonBreath,
    DrippingDripstoneLava,
    DrippingDripstoneWater,
    DrippingHoney,
    DrippingLava,
    DrippingObsidianTear,
    DrippingWater,
    Dust,
    DustColorTransition,
    Effect,
    ElderGuardian,
    ElectricSpark,
    Enchant,
    EnchantedHit,
    EndRod,
    EntityEffect,
    Explosion,
    ExplosionEmitter,
    FallingDripstoneLava,
    FallingDripstoneWater,
    FallingDust,
    FallingHoney,
    FallingLava,
    FallingNectar,
    FallingObsidianTear,
    FallingSporeBlossom,
    FallingWater,
    Firework,
    Fishing,
    Flame,
    Flash,
    Glow,
    GlowSquidInk,
    HappyVillager,
    Heart,
    InstantEffect,
    Item,
    ItemSlime,
    ItemSnowball,
    LandingHoney,
    LandingLava,
    LandingObsidianTear,
    LargeSmoke,
    Lava,
    Mycelium,
    Nautilus,
    Note,
    Poof,
    Portal,
    Rain,
    Scrape,
    SculkCharge,
    SculkChargePop,
    SculkSoul,
    Shriek,
    Smoke,
    Sneeze,
    Snowflake,
    SonicBoom,
    Soul,
    SoulFireFlame,
    Spit,
    SporeBlossomAir,
    Splash,
    SquidInk,
    SweepAttack,
    TotemOfUndying,
    Underwater,
    Vibration,
    WarpedSpore,
    WaxOff,
    WaxOn,
    WhiteAsh,
    Witch,
}