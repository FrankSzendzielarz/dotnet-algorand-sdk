using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class TransactionRefVariablePredefineds : ITypePredefined
    {

        byte scratchIndex;
        CodeBuilder code;
        Scope scope;
        List<IParameterSymbol> nulledOptionals;

        internal TransactionRefVariablePredefineds(CodeBuilder code, Scope scope, byte scratchIndex, List<IParameterSymbol> nulledOptionals)
        {
            this.code = code;
            this.scratchIndex = scratchIndex;
            this.nulledOptionals = nulledOptionals;
            this.scope = scope;
        }

        internal TransactionRefVariablePredefineds(CodeBuilder code)
        {
            this.code = code;
        }


        private void relativeGtxns(string field)
        {
            string rel = code.ReserveLabel();
            string end = code.ReserveLabel();

            code.dup();
            code.bnz(rel);
            code.pop();
            code.txn(field);
            code.b(end);
            
            code.AddLabel(rel);
            code.txn("GroupIndex");
            code.swap();
            code.minus();
            code.gtxns(field);

            code.AddLabel(end);
        }


        #region Common Fields
        public void Fee()
        {
           relativeGtxns("Fee");
        }

        public void FirstValid()
        {
            relativeGtxns("FirstValid");
        }
        public void LastValid()
        {
            relativeGtxns("LastValid");
        }
        public void Sender()
        {
            relativeGtxns("Sender");
        }
        public void TxType()
        {
            relativeGtxns("Type");
        }
        public void Lease()
        {
            relativeGtxns("Lease");
        }
        public void Note()
        {
            relativeGtxns("Note");
        }
        public void RekeyTo()
        {
            relativeGtxns("RekeyTo");
        }
        public void GroupIndex()
        {
            relativeGtxns("GroupIndex");
        }
        public void TxID()
        {
            relativeGtxns("TxID");
        }


        #endregion Common Fields

        #region App Fields

        public void NumAppArgs()
        {
            relativeGtxns("NumAppArgs");
        }

        public void NumAccounts()
        {
            relativeGtxns("NumAccounts");
        }

        public void NumAssets()
        {
            relativeGtxns("NumAssets");
        }

        public void NumApplications()
        {
            relativeGtxns("NumApplications");
        }

        public void GlobalNumUint()
        {
            relativeGtxns("GlobalNumUint");
        }

        public void GlobalNumByteSlice()
        {
            relativeGtxns("GlobalNumByteSlice");
        }

        public void LocalNumUint()
        {
            relativeGtxns("LocalNumUint");
        }

        public void LocalNumByteSlice()
        {
            relativeGtxns("LocalNumByteSlice");
        }

        public void ApplicationID()
        {
            relativeGtxns("ApplicationID");
        }

        public void OnCompletion()
        {
            relativeGtxns("OnCompletion");
        }

        public void ApprovalProgram()
        {
            relativeGtxns("ApprovalProgram");
        }

        public void ClearStateProgram()
        {
            relativeGtxns("ClearStateProgram");
        }

        public void ExtraProgramPages()
        {
            relativeGtxns("ExtraProgramPages");
        }


        #endregion App Fields

        #region Asset Txfer

        public void XferAsset()
        {
            relativeGtxns("XferAsset");
        }

        public void AssetAmount()
        {
            relativeGtxns("AssetAmount");
        }

        public void AssetSender()
        {
            relativeGtxns("AssetSender");
        }

        public void AssetReceiver()
        {
            relativeGtxns("AssetReceiver");
        }

        public void AssetCloseTo()
        {
            relativeGtxns("AssetCloseTo");
        }

        #endregion

        #region Asset Accept

        //(already done)
        //XferAsset 
        //Sender
        //AssetReceiver

        #endregion

        #region Asset Freeze

        public void FreezeAssetAccount()
        {
            relativeGtxns("FreezeAssetAccount");
        }
        public void FreezeAsset()
        {
            relativeGtxns("FreezeAsset");
        }
        public void FreezeAssetFrozen()
        {
            relativeGtxns("FreezeAssetFrozen");
        }

        #endregion

        #region Payment 

        public void Receiver()
        {
            relativeGtxns("Receiver");
        }
        public void Amount()
        {
            relativeGtxns("Amount");
        }

        public void CloseRemainderTo()
        {
            relativeGtxns("CloseRemainderTo");
        }

        #endregion

        #region Key Reg

        public void VotePK()
        {
            relativeGtxns("VotePK");
        }

        public void SelectionPK()
        {
            relativeGtxns("SelectionPK");
        }

        public void StateProofPK()
        {
            relativeGtxns("StateProofPK");
        }

        public void VoteFirst()
        {
            relativeGtxns("VoteFirst");
        }

        public void VoteLast()
        {
            relativeGtxns("VoteLast");
        }

        public void VoteKeyDilution()
        {
            relativeGtxns("VoteKeyDilution");
        }

        public void Nonparticipation()
        {
            relativeGtxns("Nonparticipation");
        }

        #endregion

        #region Config Asset

        public void ConfigAsset()
        {
            relativeGtxns("ConfigAsset");
        }

        public void ConfigAssetTotal()
        {
            relativeGtxns("ConfigAssetTotal");
        }

        public void ConfigAssetDecimals()
        {
            relativeGtxns("ConfigAssetDecimals");
        }

        public void ConfigAssetDefaultFrozen()
        {
            relativeGtxns("ConfigAssetDefaultFrozen");
        }

        public void ConfigAssetUnitName()
        {
            relativeGtxns("ConfigAssetUnitName");
        }

        public void ConfigAssetName()
        {
            relativeGtxns("ConfigAssetName");
        }

        public void ConfigAssetURL()
        {
            relativeGtxns("ConfigAssetURL");
        }

        public void ConfigAssetMetadataHash()
        {
            relativeGtxns("ConfigAssetMetadataHash");
        }

        public void ConfigAssetManager()
        {
            relativeGtxns("ConfigAssetManager");
        }

        public void ConfigAssetReserve()
        {
            relativeGtxns("ConfigAssetReserve");
        }

        public void ConfigAssetFreeze()
        {
            relativeGtxns("ConfigAssetFreeze");
        }

        public void ConfigAssetClawback()
        {
            relativeGtxns("ConfigAssetClawback");
        }



        #endregion Config Asset
        //asset config vars





    }
}
