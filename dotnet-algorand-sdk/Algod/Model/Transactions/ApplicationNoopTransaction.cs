﻿


using JsonSubTypes;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Algorand.Algod.Model.Transactions
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(ApplicationCreateTransaction), "apap")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(ApplicationCreateTransaction), "apsu")]
    [JsonSubtypes.FallBackSubType(typeof(ApplicationNoopTransaction))]
    public partial class ApplicationNoopTransaction : ApplicationCallTransaction
    {






    }
}
