using MessagePack;
using MessagePack.Formatters;
using System.Buffers;

namespace Algorand.Algod.Model.Converters.MsgPack
{
    public class DigestFormatterMsgPack : IMessagePackFormatter<Digest?>
    {
        public void Serialize(ref MessagePackWriter writer, Digest? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            writer.Write(value.Bytes);
        }

        public Digest? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
                return null;
            var bytes = reader.ReadBytes();
            if (bytes is null) return null;
            return new Digest(bytes.Value.ToArray());
        }
    }

}
