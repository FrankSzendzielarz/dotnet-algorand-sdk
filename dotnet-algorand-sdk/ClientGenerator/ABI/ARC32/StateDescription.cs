using AVM.ClientGenerator.Clients;
using System.Collections.Generic;
using System.Text;

namespace AVM.ClientGenerator.ABI.ARC32
{
    public class StateDescription
    {
        public StorageSection Local { get; set; }
        public StorageSection Global { get; set; }

        internal void ToProxy(Code proxyBody, List<string> structs)
        {
            Local?.ToProxy(proxyBody, structs, true);
            Global?.ToProxy(proxyBody, structs, false);
        }


        internal void ToSmartContractReference(StringBuilder stateBuilder, List<string> structs)
        {
            Local?.ToSmartContractReference(stateBuilder, "[Storage(StorageType.Local)]", structs);
            Global?.ToSmartContractReference(stateBuilder, "[Storage(StorageType.Global)]", structs);
        }
    }
}
