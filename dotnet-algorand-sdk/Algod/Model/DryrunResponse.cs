
namespace Algorand.Algod.Model
{
    [MessagePack.MessagePackObject]
    public partial class DryrunResponse{

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public static DryrunResponse FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DryrunResponse>(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

    }


}
