using AVM.ClientGenerator.Core.Attributes;

namespace AVM.ClientGenerator.Core
{
    public class KeyRegistrationTransactionReference : TransactionReference
    {
        [Storage(StorageType.Protocol)]
        public byte[] VotePK;

        [Storage(StorageType.Protocol)]
        public byte[] SelectionPK;

        [Storage(StorageType.Protocol)]
        public byte[] StateProofPK;

        [Storage(StorageType.Protocol)]
        public ulong VoteFirst;

        [Storage(StorageType.Protocol)]
        public ulong VoteLast;

        [Storage(StorageType.Protocol)]
        public ulong VoteKeyDilution;

        [Storage(StorageType.Protocol)]
        public bool Nonparticipation;


    }
}
