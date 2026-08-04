using Newtonsoft.Json;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC32
{
    public class HintSpec
    {
        [JsonRequired]
        public CallConfigSpec Call_config { get; set; }
        
        public Dictionary<string, DefaultArgumentSpec> Default_arguments { get; set; } = new Dictionary<string, DefaultArgumentSpec>();

    }
}
