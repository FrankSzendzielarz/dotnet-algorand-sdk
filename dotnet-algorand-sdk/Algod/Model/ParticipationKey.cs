namespace Algorand.Algod.Model
{
    using System = global::System;

    //TODO: Why is this class not in the generated output from Generator?

    /// <summary>Represents a participation key used by the node.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.5.2.0 (Newtonsoft.Json v12.0.0.0)")]
    [MessagePack.MessagePackObject]
    public partial class ParticipationKey
    {
        /// <summary>The key's ParticipationID.</summary>
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("id")]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Id { get; set; }

        /// <summary>Address the key was generated for.</summary>
        [Newtonsoft.Json.JsonProperty("address", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("address")]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Address { get; set; }

        /// <summary>When registered, this is the first round it may be used.</summary>
        [Newtonsoft.Json.JsonProperty("effective-first-valid", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("effective-first-valid")]
        public ulong? EffectiveFirstValid { get; set; }

        /// <summary>When registered, this is the last round it may be used.</summary>
        [Newtonsoft.Json.JsonProperty("effective-last-valid", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("effective-last-valid")]
        public ulong? EffectiveLastValid { get; set; }

        /// <summary>Round when this key was last used to vote.</summary>
        [Newtonsoft.Json.JsonProperty("last-vote", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("last-vote")]
        public ulong? LastVote { get; set; }

        /// <summary>Round when this key was last used to propose a block.</summary>
        [Newtonsoft.Json.JsonProperty("last-block-proposal", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("last-block-proposal")]
        public ulong? LastBlockProposal { get; set; }

        /// <summary>Round when this key was last used to generate a state proof.</summary>
        [Newtonsoft.Json.JsonProperty("last-state-proof", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("last-state-proof")]
        public ulong? LastStateProof { get; set; }

        /// <summary>Key information stored on the account.</summary>
        [Newtonsoft.Json.JsonProperty("key", Required = Newtonsoft.Json.Required.Always)]
        [MessagePack.Key("key")]
        [System.ComponentModel.DataAnnotations.Required]
        public AccountParticipation Key { get; set; } = new AccountParticipation();


    }
}
