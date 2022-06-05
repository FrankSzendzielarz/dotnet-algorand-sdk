﻿


using Newtonsoft.Json;
using System.ComponentModel;

namespace Algorand.Algod.Model.Transactions
{

    public partial class ApplicationOptInTransaction : ApplicationCallTransaction
    {

        [JsonProperty(PropertyName = "apan")]
        public OnCompletion OnCompletion => OnCompletion.Optin;



    }
}
