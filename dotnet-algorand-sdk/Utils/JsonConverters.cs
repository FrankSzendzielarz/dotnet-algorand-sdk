using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSec.Cryptography;
using System;
using System.Linq;
using System.Reflection;

namespace Algorand.Utils
{
    public class BytesConverter : JsonConverter
    {
        //是否开启自定义反序列化，值为true时，反序列化时会走ReadJson方法，值为false时，不走ReadJson方法，而是默认的反序列化
        public override bool CanRead => true;
        //是否开启自定义序列化，值为true时，序列化时会走WriteJson方法，值为false时，不走WriteJson方法，而是默认的序列化
        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return (typeof(Signature) == objectType || typeof(Digest) == objectType || typeof(Address) == objectType ||
                typeof(VRFPublicKey) == objectType || typeof(ParticipationPublicKey) == objectType ||
                typeof(PublicKey) == objectType || typeof(TEALProgram) == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(Address))
            {
                byte[] bytes;

                switch (reader.Value)
                {
                    case byte[] b:
                        {
                            bytes = b;
                            break;
                        }
                    case string s:
                        {
                            try
                            {
                                bytes = Convert.FromBase64String(s);
                                break;
                            }
                            catch (Exception e)
                            {
                                return new Address(s);
                            }

                        }
                    default:
                        bytes = null;
                        break;
                }

                if (bytes != null && bytes.Length > 0) return new Address(bytes);
                else return new Address();
            }
            else if (objectType == typeof(TEALProgram))
            {
                byte[] bytes;
                switch (reader.Value)
                {
                    case byte[] b:
                        {
                            bytes = b;
                            break;
                        }
                    case string s:
                        {
                            bytes = Convert.FromBase64String(s);
                            break;
                        }
                    default:
                        bytes = null;
                        break;
                }

                if (bytes != null && bytes.Length > 0) return new TEALProgram(bytes);
                else return new TEALProgram();
            }
            else if (objectType == typeof(Digest))
            {
                byte[] bytes;
                switch (reader.Value)
                {
                    case byte[] b:
                        {
                            bytes = b;
                            break;
                        }
                    case string s:
                        {
                            bytes = Convert.FromBase64String(s);
                            break;
                        }
                    default:
                        bytes = null;
                        break;
                }

                if (bytes != null && bytes.Length > 0) return new Digest(bytes);
                else return new Digest();
            }
            else
                return new object();

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            byte[] bytes = null;
            if (value is Address)
            {
                var adr = value as Address;
                bytes = adr.Bytes;
            }
            else if (value is Signature)
            {
                var sig = value as Signature;
                if (sig.Bytes.Any(b => b != 0))
                    bytes = sig.Bytes;


            }
            else if (value is Digest)
            {
                var dig = value as Digest;
                bytes = dig.Bytes;
            }
            else if (value is VRFPublicKey)
            {
                var vrf = value as VRFPublicKey;
                bytes = vrf.Bytes;
            }
            else if (value is ParticipationPublicKey)
            {
                var ppk = value as ParticipationPublicKey;
                bytes = ppk.Bytes;
            }
            else if (value is PublicKey key)
            {
                bytes = key.Export(KeyBlobFormat.RawPublicKey);
            }
            else if (value is TEALProgram)
            {
                var program = value as TEALProgram;
                bytes = program.Bytes;
            }
            //writer.WriteValue(Convert.ToBase64String(bytes));
            writer.WriteValue(bytes);
        }
    }
    public class MultisigAddressConverter : JsonConverter
    {
        //是否开启自定义反序列化，值为true时，反序列化时会走ReadJson方法，值为false时，不走ReadJson方法，而是默认的反序列化
        public override bool CanRead => false;
        //是否开启自定义序列化，值为true时，序列化时会走WriteJson方法，值为false时，不走WriteJson方法，而是默认的序列化
        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return typeof(MultisigAddress) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            MultisigAddress mAddress = (MultisigAddress)value;
            writer.WriteStartObject();
            writer.WritePropertyName("version");
            writer.WriteValue(mAddress.version);
            writer.WritePropertyName("threshold");
            writer.WriteValue(mAddress.threshold);
            writer.WritePropertyName("publicKeys");
            writer.WriteStartArray();
            foreach (var item in mAddress.publicKeys)
                writer.WriteValue(item.Export(KeyBlobFormat.RawPublicKey));
            writer.WriteEnd();
            writer.WriteEndObject();
            //writer.WriteValue(mAddress.publicKeys);
            //base.WriteJson(writer, value, serializer);
            //writer.WriteValue(Convert.ToBase64String(bytes));
            //writer.WriteValue(bytes);
        }
    }

    public class ReturnedTransactionConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;



        /// <summary>Determines if this converter is designed to deserialization to objects of the specified type.</summary>
        /// <param name="objectType">The target type for deserialization.</param>
        /// <returns>True if the type is supported.</returns>
        public override bool CanConvert(Type objectType)
        {
            // FrameWork 4.5
            // return typeof(T).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
            // Otherwise
            return typeof(Transaction).IsAssignableFrom(objectType);
        }

        /// <summary>Parses the json to the specified type.</summary>
        /// <param name="reader">Newtonsoft.Json.JsonReader</param>
        /// <param name="objectType">Target type.</param>
        /// <param name="existingValue">Ignored</param>
        /// <param name="serializer">Newtonsoft.Json.JsonSerializer to use.</param>
        /// <returns>Deserialized Object</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {


            if (reader.TokenType == JsonToken.Null)
                return null;

            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            ReturnedTransaction target = new ReturnedTransaction();

            //Create a new reader for this jObject, and set all properties to match the original reader.
            JsonReader jObjectReader = jObject.CreateReader();
            jObjectReader.Culture = reader.Culture;
            jObjectReader.DateParseHandling = reader.DateParseHandling;
            jObjectReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
            jObjectReader.FloatParseHandling = reader.FloatParseHandling;

            // Populate the object properties
            serializer.Populate(jObjectReader, target);

            //now get any properties on the returned txn (which wraps the txn) and set them on the actual txn class
            var returnedProps = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var txnProps = target.Transaction.Tx.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var returnedProp in returnedProps.Where(p => p.Name != "Transaction"))
            {
                foreach (var txnProp in txnProps)
                {
                    if (returnedProp.Name == txnProp.Name && returnedProp.PropertyType == txnProp.PropertyType)
                    {
                        txnProp.SetValue(target.Transaction.Tx, returnedProp.GetValue(target));
                        break;
                    }
                }

            }



            return target.Transaction.Tx;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
