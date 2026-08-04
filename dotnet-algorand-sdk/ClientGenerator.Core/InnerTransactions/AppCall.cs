namespace AVM.ClientGenerator.Core
{
    public sealed class AppCall: InnerTransaction
    {

        public ulong NumAppArgs { get; private set; }

        public ulong NumAccounts { get; private set; }

        public ulong NumAssets { get; private set; }

        public ulong NumApplications { get; private set; }

        public ulong GlobalNumUint { get; private set; }

        public ulong GlobalNumByteSlice { get; private set; }

        public ulong LocalNumUint { get; private set; }

        public ulong LocalNumByteSlice { get; private set; }

        public ulong ApplicationID { get; private set; }

        public ulong OnCompletion { get; private set; }

        public byte[] ApprovalProgram { get; private set; }

        public byte[] ClearStateProgram { get; private set; }

        public ulong ExtraProgramPages { get; private set; }


        private AppCall() { }

        public AppCall(ulong numAppArgs, ulong numAccounts, ulong numAssets, ulong numApplications, ulong localNumUint, ulong localNumByteSlice,  OnCompleteType onCompletion, ulong? globalNumUint = 0, ulong? globalNumByteSlice = 0, byte[] approvalProgram=null, byte[] clearStateProgram=null, ulong? extraProgramPages=0, ulong? fee = 0, ulong? firstValid = 0, ulong? lastValid = 0, AccountReference sender = null, ulong? txType = 0, byte[] lease = null, byte[] note = null, byte[] rekeyTo = null) : base ("appl",fee,firstValid,lastValid, sender,  lease, note, rekeyTo)
        {
         
        }
    }


}
