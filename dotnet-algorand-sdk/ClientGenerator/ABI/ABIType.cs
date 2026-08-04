using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI
{
    internal static class ABI
    {
        
        
        internal static Dictionary<string, string> ABIType = new Dictionary<string, string>
        {
            { "application", "application"},
            {"asset", "asset" },
            {"account","address" },
            {"appl","appl" },
            {"afrz", "afrz" },
            {"acfg", "acfg"},
            {"keyreg","keyreg" },
            {"pay", "pay" },
            {"txn", "txn" },
            {"axfer", "axfer" },
            {"decimal","byte[]" },
            {"string","string" },
            {"bool","bool" },
            {"datetime","uint64" },
            {"sbyte","byte" },
            {"byte","byte" },
            {"bigint","byte[]" },
            {"int64","uint64" },
            {"int32","uint64" },
            {"int16","uint64" },
            {"int8","uint64" },
            {"ubigint","byte[]" },
            {"uint64","uint64" },
            {"uint32","uint32" },
            {"uint16","uint16" },
            {"uint8","uint8" },
            {"unsupported","unsupported" },
            {"void","void" }
        };
    }
}
