using MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
#if UNITY
using UnityEngine;
#endif
namespace Algorand.Algod.Model.Transactions
{
    [MessagePack.MessagePackObject(AllowPrivate = true)]
    public partial class ApplicationCreateTransaction : ApplicationNoopTransaction
    {

        public bool ShouldSerializeGlobalStateSchema()
        {
            return GlobalStateSchema != null && (GlobalStateSchema.NumByteSlice != 0 || GlobalStateSchema.NumUint != 0);
        }

        public bool ShouldSerializeLocalStateSchema()
        {
            return LocalStateSchema != null && (LocalStateSchema.NumByteSlice != 0 || LocalStateSchema.NumUint != 0);
        }

        public bool ShouldSerializeExtraProgramPages()
        {
            return ExtraProgramPages != 0;
        }

       



#if UNITY
        [field: SerializeField]
        [Tooltip(@"")]
        [field: InspectorName(@"ApplicationIndex")]
        [JsonIgnore]
        [IgnoreMember]
        public ulong ApplicationIndex { get; internal set; }
#else
        [JsonIgnore]
        [IgnoreMember]
        public ulong? ApplicationIndex { get; internal set; }
#endif


        [OnError]
        public void HandleError(StreamingContext context, ErrorContext errorContext)
        {

            // check if the error is related to a missing property
            if (errorContext.Error is JsonSerializationException && errorContext.Error.Message.ToLowerInvariant().Contains("required"))
            {
                // ignore the error and continue
                errorContext.Handled = true;
            }

            // otherwise, let the error bubble up
        }


    }
}
