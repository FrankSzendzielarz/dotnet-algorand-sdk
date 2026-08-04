using MessagePack;
using MessagePack.Formatters;
using System.Buffers;

namespace Algorand.Algod.Model.Converters.MsgPack
{
    public class ParticipationPublicKeyFormatterMsgPack : IMessagePackFormatter<ParticipationPublicKey?>
    {
        public void Serialize(ref MessagePackWriter writer, ParticipationPublicKey? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.Write(value.Bytes);
        }

        public ParticipationPublicKey? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
                return null;
            var bytes = reader.ReadBytes();
            if (bytes is null) return null;
            return new ParticipationPublicKey(bytes.Value.ToArray());
        }
    }

}
