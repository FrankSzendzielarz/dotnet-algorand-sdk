﻿


using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Algorand.V2.Algod.Model { 

    public  class ApplicationCloseOutTransaction : ApplicationCallTransaction
    {


        [JsonProperty(PropertyName = "apid")]
        [DefaultValue(0)]
        public ulong? ApplicationId = 0;
       
        [JsonProperty(PropertyName = "apan")]
        public OnCompletion OnCompletion => OnCompletion.Closeout; 





    

    }
}
