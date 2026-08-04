using Newtonsoft.Json;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC4
{
    /// <summary>
    /// Represents an arc4 contract description
    /// </summary>
    public class ContractDescription
    {
        [JsonRequired]
        public string Name { get; set; }
        public string Desc { get; set; }
        public Dictionary<string, NetworkAppId> Networks { get; set; }
        
        [JsonRequired]
        public List<MethodDescription> Methods { get; set; } = new List<MethodDescription>();


    }
}
