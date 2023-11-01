using Lilypad.Advancements;
using Lilypad.Recipes;

namespace Lilypad.Predicates;

public interface ITypeSpecificTag {
    string Type { get; }
}

public class Any : ITypeSpecificTag {
    public string Type => "any";
}

public class Cat : ITypeSpecificTag {
    public EnumReference<CatVariant>? Variant;

    public string Type => "cat";
}

public class FishingHook : ITypeSpecificTag {
    public bool? InOpenWater;

    public string Type => "fishing_hook";
}

public class Frog : ITypeSpecificTag {
    public EnumReference<FrogVariant>? Variant;

    public string Type => "frog";
}

public class Lightning : ITypeSpecificTag {
    public Range? BlocksOnFire;
    public EntityConditions? StruckEntity;

    public string Type => "lightning";
}

public class Player : ITypeSpecificTag {
    public EntityConditions? LookingAt;
    public Dictionary<Reference<Advancement>, bool>? Advancements;
    public Gamemode? Gamemode;
    public Range? Level;
    public Dictionary<Reference<Recipe>, bool>? Recipes;

    public string Type => "player";
}

public class Slime : ITypeSpecificTag {
    public Range<int>? Size;

    public string Type => "slime";
}