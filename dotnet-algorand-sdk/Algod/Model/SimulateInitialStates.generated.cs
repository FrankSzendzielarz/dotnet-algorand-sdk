
namespace Algorand.Algod.Model
{
    using Algorand.Algod.Model.Transactions;

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    [MessagePack.MessagePackObject]
    public partial class SimulateInitialStates
    {

        [Newtonsoft.Json.JsonProperty("app-initial-states", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("app-initial-states")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The initial states of accessed application before simulation. The order of this array is arbitrary.")]
    [field:InspectorName(@"AppInitialStates")]
    public System.Collections.Generic.List<ApplicationInitialStates> AppInitialStates {get;set;} = new System.Collections.Generic.List<ApplicationInitialStates>();
#else
        public System.Collections.Generic.ICollection<ApplicationInitialStates> AppInitialStates { get; set; } = new System.Collections.ObjectModel.Collection<ApplicationInitialStates>();
#endif

    }


}
