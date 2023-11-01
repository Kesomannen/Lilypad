using Newtonsoft.Json;

namespace Lilypad;

public abstract class WriteOnlyConverter<T> : JsonConverter {
    protected virtual bool SerializeDerivedTypes => false;
        
    public override bool CanConvert(Type objectType) {
        return objectType == typeof(T) || (SerializeDerivedTypes && typeof(T).IsAssignableFrom(objectType));
    }
        
    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
        WriteJson(writer, (T) value!, serializer);
    }
        
    protected abstract void WriteJson(JsonWriter writer, T value, JsonSerializer serializer);
}