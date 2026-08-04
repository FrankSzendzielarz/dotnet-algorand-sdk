using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class SmartContractRefPredefineds : ITypePredefined
    {

        CodeBuilder code;


        internal SmartContractRefPredefineds(CodeBuilder code, Scope scope, byte scratchIndex, List<IParameterSymbol> nulledOptionals)
        {
            this.code = code;

        }


        internal SmartContractRefPredefineds(CodeBuilder code)
        {
            this.code = code;

        }

        internal void LoadLocal(string identifier)
        {
            code.loadabsolute((byte)(Core.Constants.ScratchSpaceSize - 1));  //the account reference
            code.swap();                                                //the app id
            code.byte_string_literal(identifier);                       //the key
            code.app_local_get_ex();
            code.pop();                                                 //discard the top 'did exist' flag . User must use app_opted_in
        }

        internal void LoadGlobal(string identifier)
        {
            code.byte_string_literal(identifier);                       //the key
            code.app_global_get_ex();                                   //
            code.pop();                                                 //discard the top 'did exist' flag . 
        }


        private void getAppParam(string app_field)
        {
            code.app_params_get(app_field);
            code.pop(); //discard the did exist flag in this version
        }
        public void Id()
        {

        }

        public void Balance()
        {

            code.balance();
        }

        public void ApprovalProgram()
        {
            getAppParam("AppApprovalProgram");
        }

        public void ClearStateProgram()
        {
            getAppParam("AppClearStateProgram");
        }

        public void GlobalNumUint()
        {
            getAppParam("AppGlobalNumUint");
        }
        public void GlobalNumByteSlice()
        {
            getAppParam("AppGlobalNumByteSlice");
        }
        public void LocalNumUint()
        {
            getAppParam("AppLocalNumUint");
        }
        public void LocalNumByteSlice()
        {
            getAppParam("AppLocalNumByteSlice");
        }

        public void ExtraProgramPages()
        {
            getAppParam("AppExtraProgramPages");
        }

        public void Creator()
        {
            getAppParam("AppCreator");
        }

        public void Address()
        {
            getAppParam("AppAddress");
        }




    }
}
