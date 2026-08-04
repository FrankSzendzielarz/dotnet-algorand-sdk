namespace Algorand.Gossip.Model
{
    public class Tag
    {
        public const string AgreementVote = "AV";
        public const string MsgOfInterestTag = "MI";

        public const string MsgDigestSkipTag = "MS";

        public const string NetPrioResponseTag = "NP";

        public const string NetIDVerificationTag = "NI";

        public const string PingTag = "pi"; // was removed in 3.2.1

        public const string PingReplyTag = "pj"; // was removed in 3.2.1

        public const string ProposalPayloadTag = "PP";

        public const string StateProofSigTag = "SP";

        public const string TopicMsgRespTag = "TS";

        public const string TxnTag = "TX";
        //UniCatchupReqTag   = "UC" was replaced by UniEnsBlockReqTag
        public const string UniEnsBlockReqTag = "UE";
        //UniEnsBlockResTag  = "US" was used for wsfetcherservice
        //UniCatchupResTag   = "UT" was used for wsfetcherservice
        public const string VoteBundleTag = "VB";

    }
}
