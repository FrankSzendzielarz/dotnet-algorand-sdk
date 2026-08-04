using Newtonsoft.Json;
using System;

namespace Algorand.Algod.Model.Converters.Json
{
    // Converter for ValueDelta.Bytes (state delta "bs" values), which hold arbitrary binary data. Algod's own
    // JSON API always represents these as "0x"-prefixed hex strings; without this converter, decoding the same
    // value from algod's msgpack API (via the Newtonsoft.Msgpack bridge used by Utils.Encoder.DecodeFromMsgPack)
    // instead surfaces the raw binary as a string with each byte mapped 1:1 to a char code, which then
    // re-serializes to JSON very differently (escaped control-character sequences instead of hex digits)
    // depending on byte content - producing a different JSON representation for the exact same on-chain value
    // depending only on which API format (json vs msgpack) it was originally fetched with.
    public class ValueDeltaBytesConverterJson : JsonConverter<string?>
    {
        public override string? ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            if (reader.Value is byte[] bytes)
            {
                return "0x" + BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }

            // Already text - either algod's own "0x..." hex (native JSON API) or genuine text; pass through as-is.
            return reader.Value as string ?? reader.Value.ToString();
        }

        public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
