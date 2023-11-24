namespace Lilypad; 

/// <summary>
/// Provides function extension methods for the /playsound command.
/// </summary>
public static class SoundFunctionExtensions {
    public static Function PlaySound(
        this Function function,
        string sound,
        EnumReference<SoundSource> soundSource,
        Argument<Selector> selector,
        Vector3 position,
        float volume = 1,
        float pitch = 1,
        float minVolume = 0
    ) {
        return function.Add($"playsound {sound} {soundSource} {selector} {position} {volume} {pitch} {minVolume}");
    }
    
    public static Function PlaySound(
        this Function function,
        string sound,
        EnumReference<SoundSource> soundSource,
        Argument<Selector> selector
    ) {
        return function.Add($"playsound {sound} {soundSource} {selector}");
    }
}

public enum SoundSource {
    Master,
    Music,
    Record,
    Weather,
    Block,
    Hostile,
    Neutral,
    Player,
    Ambient,
    Voice,
}

public static class Sounds {
    public static BlockSounds Block(EnumReference<Block> block) => new(block.ToString());

    public class BlockSounds {
        readonly string _name;
        
        internal BlockSounds(string name) => _name = name;
        
        public string Break => $"block.{_name}.break";
        public string Fall => $"block.{_name}.fall";
        public string Hit => $"block.{_name}.hit";
        public string Place => $"block.{_name}.place";
        public string Step => $"block.{_name}.step";
    }
    
    public static EntitySounds Entity(EnumReference<Entity> entity) => new(entity.ToString());
    
    public class EntitySounds {
        readonly string _name;
        
        internal EntitySounds(string name) => _name = name;
        
        public string Ambient => $"entity.{_name}.ambient";
        public string Death => $"entity.{_name}.death";
        public string Hurt => $"entity.{_name}.hurt";
        public string Step => $"entity.{_name}.step";
    }

    public static class Noteblock {
        public static readonly string Banjo = "block.note_block.banjo";
        public static readonly string Basedrum = "block.note_block.basedrum";
        public static readonly string Bass = "block.note_block.bass";
        public static readonly string Bell = "block.note_block.bell";
        public static readonly string Bit = "block.note_block.bit";
        public static readonly string Chime = "block.note_block.chime";
        public static readonly string CowBell = "block.note_block.cow_bell";
        public static readonly string Didgeridoo = "block.note_block.didgeridoo";
        public static readonly string Flute = "block.note_block.flute";
        public static readonly string Guitar = "block.note_block.guitar";
        public static readonly string Harp = "block.note_block.harp";
        public static readonly string Hat = "block.note_block.hat";
        public static readonly string IronXylophone = "block.note_block.iron_xylophone";
        public static readonly string Pling = "block.note_block.pling";
        public static readonly string Snare = "block.note_block.snare";
        public static readonly string Xylophone = "block.note_block.xylophone";
    }
    
    public static class Ambient {
        public static readonly BiomeSounds BasaltDeltas = new("basalt_deltas");
        public static readonly BiomeSounds CrimsonForest = new("crimson_forest");
        public static readonly BiomeSounds NetherWastes = new("nether_wastes");
        public static readonly BiomeSounds SoulSandValley = new("soul_sand_valley");
        public static readonly BiomeSounds WarpedForest = new("warped_forest");
        
        public class BiomeSounds {
            protected readonly string Name;
        
            internal BiomeSounds(string name) => Name = name;
        
            public string Additions => $"ambient.{Name}.additions";
            public string Loop => $"ambient.{Name}.loop";
            public string Mood => $"ambient.{Name}.mood";
        }

        public class UnderwaterBiomeSounds : BiomeSounds {
            internal UnderwaterBiomeSounds(string name) : base(name) { }
            
            public string Enter => $"ambient.{Name}.enter";
            public string Exit => $"ambient.{Name}.exit";
            public string RareAdditions => $"ambient.{Name}.additions.rare";
            public string UltraRareAdditions => $"ambient.{Name}.additions.ultra_rare";
        }
    }

    public static class Player {
        public static class Attack {
            public static readonly string Crit = "entity.player.attack.crit";
            public static readonly string Knockback = "entity.player.attack.knockback";
            public static readonly string Nodamage = "entity.player.attack.nodamage";
            public static readonly string Strong = "entity.player.attack.strong";
            public static readonly string Sweep = "entity.player.attack.sweep";
            public static readonly string Weak = "entity.player.attack.weak";
        }
        
        public static readonly string BigFall = "entity.player.big_fall";
        public static readonly string Breath = "entity.player.breath";
        public static readonly string Burp = "entity.player.burp";
        public static readonly string Death = "entity.player.death";
        public static readonly string Hurt = "entity.player.hurt";
        public static readonly string HurtDrown = "entity.player.hurt_drown";
        public static readonly string HurtFreeze = "entity.player.hurt_freeze";
        public static readonly string HurtOnFire = "entity.player.hurt_on_fire";
        public static readonly string LevelUp = "entity.player.levelup";
        public static readonly string SmallFall = "entity.player.small_fall";
        public static readonly string Splash = "entity.player.splash";
        public static readonly string SplashHighSpeed = "entity.player.splash.high_speed";
        public static readonly string Swim = "entity.player.swim";
    }
}