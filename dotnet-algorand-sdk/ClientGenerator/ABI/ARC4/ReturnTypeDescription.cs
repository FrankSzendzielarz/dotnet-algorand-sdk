using Newtonsoft.Json;

namespace AVM.ClientGenerator.ABI.ARC4
{
    public class ReturnTypeDescription
    {
        [JsonRequired]
        public string Type { get; set; }

        public string TypeDetail { get; set; }
        public string Desc { get; set; }
    }
}
