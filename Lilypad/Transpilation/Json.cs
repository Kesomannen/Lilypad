using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Lilypad; 

internal static class Json {
    public static string Serialize(object value, bool pretty = false) {
        return JsonConvert.SerializeObject(value, pretty ? Formatting.Indented : Formatting.None, Settings);
    }

    public static T? Deserialize<T>(string text) {
        return JsonConvert.DeserializeObject<T>(text, Settings);
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

    class SerializeInnerConverter : WriteOnlyConverter<ISerializeInnerJson> {
        protected override bool SerializeDerivedTypes => true;

        protected override void WriteJson(JsonWriter writer, ISerializeInnerJson value, JsonSerializer serializer) {
            serializer.Serialize(writer, value.SerializedData);
        }
    }
}

internal interface ISerializeInnerJson {
    object? SerializedData { get; }
}

internal interface ISerializeInner : ISerializeInnerNBT, ISerializeInnerJson {
    new object? SerializedData { get; }

    object? ISerializeInnerNBT.SerializedData => SerializedData;
    object? ISerializeInnerJson.SerializedData => SerializedData;
}