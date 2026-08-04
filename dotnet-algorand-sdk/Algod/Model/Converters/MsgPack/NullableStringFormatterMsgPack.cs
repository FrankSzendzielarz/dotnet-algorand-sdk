
using System;
using System.Buffers;
using System.Text;

namespace Algorand.Algod.Model.Converters.MsgPack
{
    public sealed class NullableStringFormatterMsgPack : MessagePack.Formatters.IMessagePackFormatter<string?>, MessagePack.Formatters.IMessagePackFormatter
    {
        public static readonly NullableStringFormatterMsgPack Instance = new NullableStringFormatterMsgPack();

        private NullableStringFormatterMsgPack()
        {
        }

        public void Serialize(ref MessagePack.MessagePackWriter writer, string? value, MessagePack.MessagePackSerializerOptions options)
        {
            // if value starts with 0x, convert hex to byte[]
            if (value == null)
            {
                writer.WriteNil();
                return;
            }
            if (value.StartsWith("0x"))
            {
                string hex = value.Substring(2);
                int numberChars = hex.Length;
                byte[] bytes = new byte[numberChars / 2];
                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                }
                writer.Write(bytes);
                return;
            }
            writer.Write(value);
        }

        public string? Deserialize(ref MessagePack.MessagePackReader reader, MessagePack.MessagePackSerializerOptions options)
        {
            var bytes= reader.ReadBytes();
            if(bytes.Value.Length==0)
            {
                return "";
            }
            if (bytes.Value.FirstSpan.ToArray()[0] == (byte) 0x00) 
            {
                // convert byte[] to hex
                StringBuilder sb = new StringBuilder();
                sb.Append("0x");
                foreach (var b in bytes.Value.ToArray())
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
            return Encoding.UTF8.GetString(bytes.Value.ToArray());
        }
    }
}