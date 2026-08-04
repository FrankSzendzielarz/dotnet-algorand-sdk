using MessagePack;
using MessagePack.Formatters;
using System.Buffers;

namespace Algorand.Algod.Model.Converters.MsgPack
{
    public class AddressFormatterMsgPack : IMessagePackFormatter<Address?>
    {
        public void Serialize(ref MessagePackWriter writer, Address? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            writer.Write(value.Bytes);
        }

        public Address? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
                return null;
            var bytes = reader.ReadBytes();
            if (bytes is null) return null;
            return new Address(bytes.Value.ToArray());
        }
    }

}
