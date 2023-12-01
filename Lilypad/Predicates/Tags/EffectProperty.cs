using Newtonsoft.Json;

namespace Lilypad; 

public struct EffectProperty {
    public bool? Ambient;
    public IntRange? Amplifier;
    [JsonProperty("Duration")]
    public IntRange? DurationRemaining;
    public bool? Visible;
}