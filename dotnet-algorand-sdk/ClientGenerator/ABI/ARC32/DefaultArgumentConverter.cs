using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;

namespace AVM.ClientGenerator.ABI.ARC32
{
    public class DefaultArgumentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DefaultArgumentSpec);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            ArgumentDefaultSource source = jo["source"].ToObject<ArgumentDefaultSource>();
            DefaultArgumentSpec result;

            switch (source)
            {
                case ArgumentDefaultSource.AbiMethod:
                    result = new AbiMethodDefaultArgument();
                    break;
                case ArgumentDefaultSource.GlobalState:
                case ArgumentDefaultSource.LocalState:
                    result = new StateDefaultArgument();
                    break;
                case ArgumentDefaultSource.Constant:
                    result = new ConstantDefaultArgument();
                    break;
                default:
                    throw new ArgumentException($"Unknown source type: {source}");
            }

            serializer.Populate(jo.CreateReader(), result);
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        public override bool CanWrite
        {
            get { return false; }
        }
    }

}
