
namespace Algorand.Algod.Model
{

using System = global::System;


public partial class DryrunResponse{

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public static DryrunResponse FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DryrunResponse>(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

    }


}
