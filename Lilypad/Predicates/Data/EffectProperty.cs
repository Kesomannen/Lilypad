using Newtonsoft.Json;

namespace Lilypad; 

public struct EffectProperty {
    public bool? Ambient;
    public Range<int>? Amplifier;
    [JsonProperty("Duration")]
    public Range<int>? DurationRemaining;
    public bool? Visible;
}