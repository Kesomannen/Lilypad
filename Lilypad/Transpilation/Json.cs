using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Lilypad; 

internal static class Json {
    public static string Serialize(object value, bool pretty = false) {
        return JsonConvert.SerializeObject(value, pretty ? Formatting.Indented : Formatting.None, Settings);
    }
    
    public static readonly JsonSerializerSettings Settings = new() {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.None,
        Converters = {
            new StringEnumConverter {
                NamingStrategy = new SnakeCaseNamingStrategy(),
            },
            new SerializeInnerConverter()
        },
        ContractResolver = new DefaultContractResolver {
            NamingStrategy = new SnakeCaseNamingStrategy()
        },
    };

    class SerializeInnerConverter : WriteOnlyConverter<ISerializeInner> {
        protected override bool SerializeDerivedTypes => true;

        protected override void WriteJson(JsonWriter writer, ISerializeInner value, JsonSerializer serializer) {
            serializer.Serialize(writer, value.SerializedData);
        }
    }
}

internal interface ISerializeInner {
    object? SerializedData { get; }
}