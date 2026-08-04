namespace AVM.ClientGenerator.Core
{
    public sealed class KeyRegistration : InnerTransaction
    {
        public byte[] VotePK { get; private set; }

        public byte[] SelectionPK { get; private set; }

        public byte[] StateProofPK { get; private set; }

        public ulong VoteFirst { get; private set; }

        public ulong VoteLast { get; private set; }

        public ulong VoteKeyDilution { get; private set; }

        public bool Nonparticipation { get; private set; }

        private KeyRegistration() { }

        public KeyRegistration(byte[] votePK, byte[] selectionPK, byte[] stateProofPK, ulong voteFirst, ulong voteLast, ulong voteKeyDilution, bool nonparticipation, ulong? fee = 0, ulong? firstValid = 0, ulong? lastValid = 0, AccountReference sender = null, ulong? txType = 0, byte[] lease = null, byte[] note = null, byte[] rekeyTo = null) : base ("keyreg",fee,firstValid,lastValid,sender,lease,note,rekeyTo)
        {
          
        }
    }
}
