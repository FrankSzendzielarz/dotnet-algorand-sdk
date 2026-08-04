namespace Algorand.Algod.Model
{
    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    [MessagePack.MessagePackObject]
    public partial class BoxRef
    {

        public bool ShouldSerializeApp()
        {
            return App != 0;
        }

        public bool ShouldSerializeName()
        {
            return Name?.Length != 0;
        }



        [Newtonsoft.Json.JsonProperty("i", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("i")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[i\] app id")]
    [field:InspectorName(@"App")]
    public ulong App {get;set;}
#else
        public ulong App { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("n", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [System.ComponentModel.DataAnnotations.Required]
        [MessagePack.Key("n")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"\[n\] box name, base64 encoded.")]
    [field:InspectorName(@"Name")]
    public byte[] Name {get;set;}
#else
        public byte[] Name { get; set; }
#endif



    }


}