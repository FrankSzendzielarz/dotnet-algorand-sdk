namespace Algorand.Algod.Model.Converters.MsgPack
{
    //public class StringToByteFormatterMsgPack : IMessagePackFormatter<string?>
    //{
    //    public void Serialize(ref MessagePackWriter writer, string? value, MessagePackSerializerOptions options)
    //    {
    //        if (value == null)
    //        {
    //            writer.WriteNil();
    //            return;
    //        }
    //        writer.Write(Encoding.UTF8.GetBytes(value));
    //    }

    //    public string? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    //    {
    //        if (reader.TryReadNil())
    //            return null;
    //        var bytes = reader.ReadBytes().Value.ToArray();
    //        if (bytes is null) return null;
    //        return Encoding.UTF8.GetString(bytes);
    //    }
    //}

}
