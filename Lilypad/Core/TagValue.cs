using Newtonsoft.Json;

namespace Lilypad; 

public struct TagValue {
    [JsonProperty("id")]
    public string Location;
    public bool Required;
}