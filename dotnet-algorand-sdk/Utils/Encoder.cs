using Algorand.Algod.Model.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Msgpack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algorand.Utils
{
    /// <summary>
    /// Convenience class for serializing and deserializing arbitrary objects to json or msgpack.
    /// </summary>
    public static class Encoder
    {
        public static bool EncodeToMsgPack = false;
        /// <summary>
        /// Convenience method for serializing arbitrary objects.
        /// </summary>
        /// <param name="o">the object to serializing</param>
        /// <returns>serialized object</returns>
        public static byte[] EncodeToMsgPackOrdered(object o)
        {
            try
            {
                EncodeToMsgPack = true;
                MemoryStream memoryStream = new MemoryStream();
                JsonSerializer serializer = new JsonSerializer()
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    ContractResolver = new OrderedContractResolver(),
                    Formatting = Formatting.None
                };

                MessagePackWriter writer = new MessagePackWriter(memoryStream);
                serializer.Serialize(writer, o);
                var bytes = memoryStream.ToArray();
                return bytes;
            }
            finally
            {
                EncodeToMsgPack = false;
            }
        }

        /// <summary>
        /// Convenience method for serializing lists without the list wrapper: just concat each serialised object
        /// </summary>
        /// <param name="o">the object to serializing</param>
        /// <returns>serialized object</returns>
        public static byte[] EncodeToMsgPackOrdered(List<SignedTransaction> o)
        {
            MemoryStream memoryStream = new MemoryStream();
            JsonSerializer serializer = new JsonSerializer()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new OrderedContractResolver(),
                Formatting = Formatting.None
            };

            MessagePackWriter writer = new MessagePackWriter(memoryStream);
            foreach (var e in o) { serializer.Serialize(writer, e); }
            var bytes = memoryStream.ToArray();
            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static byte[] EncodeToMsgPackNoOrder(object o)
        {
            MemoryStream memoryStream = new MemoryStream();
            JsonSerializer serializer = new JsonSerializer()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None
            };

            MessagePackWriter writer = new MessagePackWriter(memoryStream);
            serializer.Serialize(writer, o);
            var bytes = memoryStream.ToArray();
            return bytes;
        }


        /// <summary>
        /// Convenience method for deserializing arbitrary objects encoded with canonical msg-pack
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="input">input byte array representing canonical msg-pack encoding</param>
        /// <returns>deserialized object</returns>
        public static T DecodeFromMsgPack<T>(byte[] input)
        {
            try
            {
                var options = MessagePack.MessagePackSerializerOptions.Standard.WithResolver(
                    MessagePack.Resolvers.CompositeResolver.Create(
                        new MessagePack.Formatters.IMessagePackFormatter[] { Algorand.Algod.Model.Converters.MsgPack.NullableStringFormatterMsgPack.Instance },
                        new MessagePack.IFormatterResolver[] { MessagePack.Resolvers.ContractlessStandardResolver.Instance }
                    )
                );
                var result = MessagePack.MessagePackSerializer.Deserialize<T>(input, options);
                if (!MayBeMisresolvedKeyreg(result))
                {
                    return result;
                }
                // fall through to the Newtonsoft.Msgpack path below - see MayBeMisresolvedKeyreg for why
            }
            catch
            {
                // fall through to the Newtonsoft.Msgpack path below
            }

            MemoryStream st = new MemoryStream(input);
            //memoryStream.Write(input, 0, input.Length);
            //memoryStream.Seek(0, SeekOrigin.Begin);
            JsonSerializer serializer = new JsonSerializer()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None
            };
            MessagePackReader reader = new MessagePackReader(st);
            return serializer.Deserialize<T>(reader);
            //return DecodeFromJson<T>(MessagePackSerializer.ConvertToJson(input));
        }

        /// <summary>
        /// KeyRegisterOfflineTransaction's fields are a strict subset of KeyRegisterOnlineTransaction's, and
        /// Algorand's canonical msgpack transactions are plain field maps rather than MessagePack-CSharp's own
        /// [typeId, payload] union framing - so the native contractless [Union] resolver above can't actually
        /// tell the two apart from the wire bytes, and it doesn't throw when it silently guesses "Offline". A
        /// transaction that decoded as Offline is exactly the ambiguous case (a genuine offline txn would decode
        /// the same way, so this is a deliberately cheap, over-inclusive check): re-decode via the
        /// Newtonsoft.Msgpack + JsonSubtypes path instead, which honors the KnownSubTypeWithProperty
        /// discriminators declared on KeyRegistrationTransaction and resolves correctly either way.
        /// </summary>
        private static bool MayBeMisresolvedKeyreg<T>(T result)
        {
            if (result is KeyRegisterOfflineTransaction) return true;
            var signedTransaction = result as SignedTransaction;
            return signedTransaction != null && signedTransaction.Tx is KeyRegisterOfflineTransaction;
        }

        /// <summary>
        /// Encode an object as json.
        /// </summary>
        /// <param name="o">object to encode</param>
        /// <returns>json string</returns>
        public static string EncodeToJson(object o)
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            // JsonTextWriter's indented output uses the underlying TextWriter's NewLine, which
            // defaults to Environment.NewLine (\r\n on Windows, \n on Linux/macOS). Fix it to \r\n
            // explicitly so EncodeToJson produces identical bytes on every platform.
            using var stringWriter = new StringWriter { NewLine = "\r\n" };
            using var jsonWriter = new JsonTextWriter(stringWriter)
            {
                Formatting = Formatting.Indented
            };
            serializer.Serialize(jsonWriter, o);
            return stringWriter.ToString();
        }
        /// <summary>
        /// Decode a json string to an object.
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="json">json string</param>
        /// <returns>object</returns>
        public static T DecodeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Convenience method for writing bytes as hex.
        /// </summary>
        /// <param name="bytes">bytes input to encodeToMsgPack as hex string</param>
        /// <returns>encoded hex string</returns>
        public static string EncodeToHexStr(byte[] bytes)
        {
            return BitConverter.ToString(bytes, 0).Replace("-", string.Empty).ToLower();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            int n = hex.Length;
            byte[] bytes = new byte[n / 2];
            for (int i = 0; i < n; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// Convenience method to get a value as a big-endian byte array
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte[] ToBigEndianBytes(this ulong val)
        {
            var bytes = BitConverter.GetBytes(val);
            if (BitConverter.IsLittleEndian) //depends on hardware
                Array.Reverse(bytes);

            return bytes;
        }
        /// <summary>
        /// Big endian conversion for ulong value from 8 bytes.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static ulong FromBigEndianBytes(this byte[] bytes)
        {
            if (bytes.Length != 8)
                throw new ArgumentException("Byte array must be exactly 8 bytes long.");
            if (BitConverter.IsLittleEndian) //depends on hardware
                Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static byte[] DeltaValueStringToBytes(string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }
        public static string DeltaValueBytesToString(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }
        public static ulong UInt256ToUlong(byte[] bytes)
        {
            ulong result = 0;
            foreach (byte b in bytes)
            {
                result = (result << 8) | b;
            }
            return result;
        }
    }

    public class OrderedContractResolver : DefaultContractResolver
    {
        protected override System.Collections.Generic.IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization).OrderBy(p => p.PropertyName).ToList();
        }
    }
}
