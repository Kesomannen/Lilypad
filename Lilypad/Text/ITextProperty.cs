using Newtonsoft.Json;

namespace Lilypad.Text; 

public interface ITextProperty {
    [JsonIgnore]
    string Name { get; }
    virtual object Value => this;
}