using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace AVM.ClientGenerator.ABI.ARC32
{
    public class CallConfigSpec
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig No_op { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig Opt_in { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig Close_out { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig Update_application { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig Delete_application { get; set; }

        [JsonIgnore]
        public string Summary => $"No_op: {No_op}, Opt_in: {Opt_in}, Close_out: {Close_out}, Update_application: {Update_application}, Delete_application: {Delete_application}";
    }
}
