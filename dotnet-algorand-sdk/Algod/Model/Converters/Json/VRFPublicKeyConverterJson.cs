using Newtonsoft.Json;
using System;

namespace Algorand.Algod.Model.Converters.Json
{
    public class VRFPublicKeyConverterJson : JsonConverter<VRFPublicKey>
    {
        public override VRFPublicKey ReadJson(JsonReader reader, Type objectType, VRFPublicKey existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            byte[] bytes = reader.Value is byte[] rawBytes ? rawBytes : Convert.FromBase64String((string)reader.Value);
            return new VRFPublicKey(bytes);
        }

        public override void WriteJson(JsonWriter writer, VRFPublicKey value, JsonSerializer serializer)
        {
            // Pass the raw bytes through rather than pre-encoding to base64: the JSON writer auto-base64-encodes
            // byte[] for text output, but when this same converter runs under the msgpack writer bridge used for
            // canonical transaction encoding (Utils.Encoder.EncodeToMsgPackOrdered), pre-encoding to a base64
            // *string* would emit a msgpack string instead of raw binary - corrupting the bytes that get signed
            // and causing "signature didn't pass verification" for any transaction carrying this field (e.g.
            // online key registration's selkey).
            writer.WriteValue(value.Bytes);
        }
    }
}
