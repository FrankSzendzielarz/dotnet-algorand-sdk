using MessagePack;
using Newtonsoft.Json;

namespace Algorand.Algod.Model.Transactions
{
    [MessagePackObject(AllowPrivate = true)]
    public partial class KeyRegisterOnlineTransaction : KeyRegistrationTransaction
    {
        public bool ShouldSerializeVoteKeyDilution()
        {
            return VoteKeyDilution != 0;
        }

        public bool ShouldSerializeVoteFirst()
        {
            return VoteFirst != 0;
        }

        public bool ShouldSerializeVoteLast()
        {
            return VoteLast != 0;
        }

        // NonParticipation is `bool?`, and Newtonsoft's DefaultValueHandling.Ignore only omits a *null* nullable
        // value, not an explicit `false` - so a caller setting NonParticipation = false (a very natural thing to
        // do defensively) would get it serialized. That's non-canonical (Algorand's canonical msgpack encoding
        // omits zero/default-valued fields, including false booleans), so algod's own re-derivation of the
        // transaction hash for signature verification would produce different bytes than the client signed
        // against, and reject the transaction with "signature didn't pass verification".
        public bool ShouldSerializeNonParticipation()
        {
            return NonParticipation == true;
        }

        [JsonProperty(PropertyName = "sprfkey")]
        [MessagePack.Key("sprfkey")]
        public byte[] StateProofPK { get; set; } 

    }
}
