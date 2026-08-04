namespace Algorand.AVM.ClientGenerator.ABI.ARC32
{
    public class SourceDescription
    {
        /// <summary>
        /// Approval program in Teal in B64
        /// </summary>
        [Newtonsoft.Json.JsonProperty("approval")]
        public string Approval { get; set; }
        /// <summary>
        /// Approval program in B64
        /// </summary>
        [Newtonsoft.Json.JsonProperty("approval-avm")]
        public string ApprovalAVM { get; set; }
        /// <summary>
        /// Clear program in Teal in B64
        /// </summary>
        [Newtonsoft.Json.JsonProperty("clear")]
        public string Clear { get; set; }
        /// <summary>
        /// Clear program in B64
        /// </summary>
        [Newtonsoft.Json.JsonProperty("clear-avm")]
        public string ClearAVM { get; set; }

    }
}
