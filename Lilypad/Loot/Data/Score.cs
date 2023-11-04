using Lilypad.Predicates;
using Newtonsoft.Json;

namespace Lilypad.Loot; 

public class Score : NumberProvider {
    [JsonProperty("score")]
    public string Name;
    public float? Scale;
    public ScoreTarget Target;

    public Score(string name, string uuid) {
        Name = name;
        Target = new ScoreTarget {
            Type = ScoreTargetType.Fixed,
            Uuid = uuid
        };
    }

    public Score(string name, ContextEntity entity = ContextEntity.This) {
        Name = name;
        Target = new ScoreTarget {
            Type = ScoreTargetType.Context,
            Entity = entity
        };
    }
}

public struct ScoreTarget {
    public ScoreTargetType Type;
    public string Uuid;
    [JsonProperty("target")]
    public ContextEntity Entity;
}

public enum ScoreTargetType {
    Fixed,
    Context
}