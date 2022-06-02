﻿


using JsonSubTypes;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Algorand.Algod.Model
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(ApplicationCreateTransaction), "apap")] 
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(ApplicationCreateTransaction), "apsu")]
    [JsonSubtypes.FallBackSubType(typeof(ApplicationNoopTransaction))]
    public  class ApplicationNoopTransaction : ApplicationCallTransaction
    {

        [JsonProperty(PropertyName = "apid")]
        [DefaultValue(0)]
        public ulong? ApplicationId { get; set; }  = 0;


        [JsonProperty(PropertyName = "apan")]
        public OnCompletion OnCompletion => OnCompletion.Noop;





    }
}
