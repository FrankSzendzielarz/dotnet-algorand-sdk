using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Algorand.Algod.Model.Converters.Json
{
    public class BlockCertVoteSigConverterJson : JsonConverter<BlockCertVoteSig>
    {
        public override void WriteJson(JsonWriter writer, BlockCertVoteSig? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartObject();

            WriteBase64(writer, "p", value.P);
            WriteBase64(writer, "p1s", value.P1S);
            WriteBase64(writer, "p2", value.P2);
            WriteBase64(writer, "p2s", value.P2S);
            WriteBase64(writer, "ps", value.PS);
            WriteBase64(writer, "s", value.S);

            writer.WriteEndObject();
        }

        private void WriteBase64(JsonWriter writer, string propertyName, byte[]? data)
        {
            writer.WritePropertyName(propertyName);
            writer.WriteValue(data != null ? Convert.ToBase64String(data) : null);
        }

        public override BlockCertVoteSig? ReadJson(JsonReader reader, Type objectType, BlockCertVoteSig? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject obj = JObject.Load(reader);

            return new BlockCertVoteSig
            {
                P = ReadBase64(obj, "p"),
                P1S = ReadBase64(obj, "p1s"),
                P2 = ReadBase64(obj, "p2"),
                P2S = ReadBase64(obj, "p2s"),
                PS = ReadBase64(obj, "ps"),
                S = ReadBase64(obj, "s")
            };
        }

        private byte[]? ReadBase64(JObject obj, string propertyName)
        {
            var token = obj[propertyName];
            return token?.Type == JTokenType.String
                ? Convert.FromBase64String(token!.ToString())
                : null;
        }
    }
}
