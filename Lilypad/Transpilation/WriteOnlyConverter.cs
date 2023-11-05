using Newtonsoft.Json;

namespace Lilypad;

internal abstract class WriteOnlyConverter<T> : JsonConverter {
    protected virtual bool SerializeDerivedTypes => false;
    
    public override bool CanConvert(Type objectType) {
        var type = typeof(T);
        return objectType == type || (SerializeDerivedTypes && (type.IsAssignableFrom(objectType) || CheckNullable()));

        bool CheckNullable() {
            return objectType.IsGenericType && 
                   objectType.GetGenericTypeDefinition() == typeof(Nullable<>) && 
                   type.IsAssignableFrom(objectType.GetGenericArguments()[0]);
        }
    }
        
    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
        WriteJson(writer, (T) value!, serializer);
    }
        
    protected abstract void WriteJson(JsonWriter writer, T value, JsonSerializer serializer);
}