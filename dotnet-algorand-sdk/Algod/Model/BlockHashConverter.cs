using Algorand.Utils;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Algorand.Algod.Model
{
    /// <summary>
    /// JSON converter for block hash that handles "blk-" prefixed base32 format
    /// </summary>
    public class BlockHashConverter : JsonConverter<byte[]>
    {
        private const string BLOCK_HASH_PREFIX = "blk-";

        public override void WriteJson(JsonWriter writer, byte[]? value, JsonSerializer serializer)
        {
            if (value == null || value.Length == 0)
            {
                writer.WriteNull();
                return;
            }
            if (Encoder.EncodeToMsgPack)
            {
                writer.WriteValue(value);
                return;
            }

            // Encode as "blk-" + base32
            string base32Hash = Base32.EncodeToString(value, false);
            writer.WriteValue(BLOCK_HASH_PREFIX + base32Hash);
        }

        public override byte[]? ReadJson(JsonReader reader, Type objectType, byte[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.Bytes)
            {

                byte[]? valueBytes = reader.Value as byte[];
                return valueBytes;
            }

            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException($"Expected string token for block hash, got {reader.TokenType}");
            }

            string? value = reader.Value as string;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            // Handle "blk-" prefixed format
            if (value.StartsWith(BLOCK_HASH_PREFIX))
            {
                string base32Hash = value.Substring(BLOCK_HASH_PREFIX.Length);
                return Base32.DecodeFromString(base32Hash);
            }

            // Fallback: try to decode as direct base32 or hex
            try
            {
                // Try base32 first
                return Base32.DecodeFromString(value);
            }
            catch
            {
                // Try hex as fallback
                try
                {
                    return Enumerable.Range(0, value.Length / 2)
                        .Select(i => Convert.ToByte(value.Substring(i * 2, 2), 16))
                        .ToArray();
                }
                catch
                {
                    throw new JsonSerializationException($"Unable to decode block hash from value: {value}");
                }
            }
        }
    }
}