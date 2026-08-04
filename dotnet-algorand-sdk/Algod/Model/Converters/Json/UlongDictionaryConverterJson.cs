using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Algorand.Algod.Model.Converters.Json
{
    public class UlongDictionaryConverterJson<T> : JsonConverter<IDictionary<ulong, T>>
    {
        public override void WriteJson(JsonWriter writer, IDictionary<ulong, T> value, JsonSerializer serializer)
        {
            // Write as object format for better compatibility
            writer.WriteStartObject();
            if (value != null)
            {
                foreach (var kv in value)
                {
                    writer.WritePropertyName(kv.Key.ToString());
                    //if (Algorand.Utils.Encoder.EncodeToMsgPack)
                    //{
                    //    writer.WriteValue(kv.Key);
                    //}
                    //else
                    //{
                    //    writer.WritePropertyName(kv.Key.ToString());
                    //}
                    serializer.Serialize(writer, kv.Value);
                }
            }
            writer.WriteEndObject();
        }

        public override IDictionary<ulong, T> ReadJson(JsonReader reader, Type objectType, IDictionary<ulong, T> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var result = new Dictionary<ulong, T>();

            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.StartObject)
            {
                // Handle object format: {"key1": value1, "key2": value2}
                var obj = JObject.Load(reader);
                foreach (var property in obj.Properties())
                {
                    if (ulong.TryParse(property.Name, out ulong key))
                    {
                        var value = property.Value.ToObject<T>(serializer);
                        if (value != null)
                        {
                            result[key] = value;
                        }
                    }
                }
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                // Handle legacy array format: [[key1, value1], [key2, value2]]
                var array = JArray.Load(reader);
                foreach (var item in array)
                {
                    var pair = (JArray)item!;
                    var key = pair[0]!.ToObject<ulong>();
                    var value = pair[1]!.ToObject<T>(serializer);
                    result[key] = value!;
                }
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token type {reader.TokenType} when parsing UlongDictionary. Expected StartObject or StartArray.");
            }

            return result;
        }
    }
}
