using CarFactory_Domain;
using Newtonsoft.Json;
using System;

namespace CarFactory.Formatter
{
    public class CustomJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PaintJob);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //todo handle deserialization
            return serializer.Deserialize(reader, typeof(PaintJob));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
