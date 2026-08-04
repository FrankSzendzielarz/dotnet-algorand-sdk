using Algorand.Algod.Model.Transactions;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Buffers;

namespace Algorand.Algod.Model.Converters.MsgPack
{
    public class ValueDeltaFormatterMsgPack : IMessagePackFormatter<ValueDelta?>
    {
        public void Serialize(ref MessagePackWriter writer, ValueDelta? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.Write(value.Bytes);
        }

        public ValueDelta? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
                return null;
            var bytes = reader.ReadBytes();
            if (bytes is null) return null;
            return new ValueDelta()
            {
                Bytes = Convert.ToBase64String(bytes.Value.ToArray())
            };
        }
    }

}
