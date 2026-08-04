using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class AccountRefPredefineds : ITypePredefined
    {


        CodeBuilder code;


        internal AccountRefPredefineds(CodeBuilder code, Scope scope, byte scratchIndex, List<IParameterSymbol> nulledOptionals)
        {
            this.code = code;

        }

        internal AccountRefPredefineds(CodeBuilder code)
        {
            this.code = code;
        }

        public void Address()
        {

        }


        public void Balance()
        {

            code.balance();


        }

        public void AssetBalance()
        {
            // asset index is already on stack

            code.swap();
            code.asset_holding_get("AssetBalance");
            code.pop(); //discard if the asset existed, user must check
        }

        public void IsAssetFrozen()
        {

            code.swap();
            code.asset_holding_get("AssetFrozen");
            code.pop(); //discard if the asset existed, user must check
        }

        public void OptedIn()
        {

            code.swap();
            code.app_opted_in();
            code.pop(); //discard if the asset existed, user must check
        }

        public void SetAsLocalAccountContext()
        {

            code.storeabsolute((byte)(Core.Constants.ScratchSpaceSize - 1));

        }


        public void Freeze()
        {
            code.itxn_begin();
            code.int_literal_constant("afrz");
            code.itxn_field("TypeEnum");
            code.itxn_field("FreezeAsset");
            code.uncover(3);  //TODO TEST
            code.itxn_field("FreezeAssetAccount");
            code.int_literal_constant(1);
            code.itxn_field("FreezeAssetFrozen");
            code.itxn_submit();

        }

        public void UnFreeze()
        {
            code.itxn_begin();
            code.int_literal_constant("afrz");
            code.itxn_field("TypeEnum");
            code.itxn_field("FreezeAsset");
            code.uncover(3);
            code.itxn_field("FreezeAssetAccount");
            code.int_literal_constant(0);
            code.itxn_field("FreezeAssetFrozen");
            code.itxn_submit();

        }

        public void Clawback()
        {
            code.itxn_begin();
            code.int_literal_constant("axfer");
            code.itxn_field("TypeEnum");
            code.global("CurrentApplicationAddress");
            code.itxn_field("AssetReceiver");
            code.itxn_field("AssetAmount");
            code.itxn_field("XferAsset");
            code.uncover(6);
            code.itxn_field("AssetSender");
            code.itxn_submit();
        }
    }
}
