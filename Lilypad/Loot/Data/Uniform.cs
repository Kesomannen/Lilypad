using Lilypad.Predicates;
using Newtonsoft.Json;

namespace Lilypad.Loot;

[JsonConverter(typeof(Converter))]
public class Uniform : NumberProvider {
    public Range<NumberProvider> Range;

    class Converter : WriteOnlyConverter<Uniform> {
        protected override void WriteJson(JsonWriter writer, Uniform value, JsonSerializer serializer) {
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue(value.Type);
            writer.WritePropertyName("min");
            serializer.Serialize(writer, value.Range.Min);
            writer.WritePropertyName("max");
            serializer.Serialize(writer, value.Range.Max);
            writer.WriteEndObject();
        }
    }
}