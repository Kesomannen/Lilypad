using Newtonsoft.Json;

namespace Lilypad;

public class DatapackMetadata {
    public PackInformation Pack = new();
    public PackFilters? Filter;
    public PackOverlays? Overlay;
    [JsonIgnore]
    public string? IconPath;
}