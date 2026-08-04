using MessagePack;
using MessagePack.Formatters;
using System.Buffers;

namespace Algorand.Algod.Model.Converters.MsgPack
{
    public class VRFPublicKeyFormatterMsgPack : IMessagePackFormatter<VRFPublicKey?>
    {
        public void Serialize(ref MessagePackWriter writer, VRFPublicKey? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.Write(value.Bytes);
        }

        public VRFPublicKey? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
                return null;
            var bytes = reader.ReadBytes();
            if (bytes is null) return null;
            return new VRFPublicKey(bytes.Value.ToArray());
        }
    }

}
