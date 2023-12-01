using Lilypad;
using Lilypad.Extensions;
using Lilypad.Recipes;

namespace Lilypad;

public interface ITypeSpecificTag {
    string Type => GetType().Name.ToSnakeCase();
}

public class Any : ITypeSpecificTag { }

public class Cat : ITypeSpecificTag {
    public EnumReference<CatVariant>? Variant;
}

public class FishingHook : ITypeSpecificTag {
    public bool? InOpenWater;
}

public class Frog : ITypeSpecificTag {
    public EnumReference<FrogVariant>? Variant;
}

public class Lightning : ITypeSpecificTag {
    public Range? BlocksOnFire;
    public EntityConditions? StruckEntity;
}

public class Player : ITypeSpecificTag {
    public EntityConditions? LookingAt;
    public Dictionary<Reference<Advancement>, bool>? Advancements;
    public Gamemode? Gamemode;
    public Range? Level;
    public Dictionary<Reference<Recipe>, bool>? Recipes;
}

public class Slime : ITypeSpecificTag {
    public IntRange? Size;
}