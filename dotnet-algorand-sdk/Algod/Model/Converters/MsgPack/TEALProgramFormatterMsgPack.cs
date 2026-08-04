using MessagePack;
using MessagePack.Formatters;
using System.Buffers;

namespace Algorand.Algod.Model.Converters.MsgPack
{
    public class TEALProgramFormatterMsgPack : IMessagePackFormatter<TEALProgram?>
    {
        public void Serialize(ref MessagePackWriter writer, TEALProgram? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.Write(value.Bytes);
        }

        public TEALProgram? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
                return null;
            var bytes = reader.ReadBytes();
            if (bytes is null) return null;
            return new TEALProgram(bytes.Value.ToArray());
        }
    }

}
