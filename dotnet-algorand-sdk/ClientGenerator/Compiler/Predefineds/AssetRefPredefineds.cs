using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace AVM.ClientGenerator.Compiler.Predefineds
{
    internal class AssetRefPredefineds : ITypePredefined
    {

        CodeBuilder code;
        Scope scope;
        byte scratchIndex;
        List<IParameterSymbol> nulledOptionals;

        internal AssetRefPredefineds(CodeBuilder code, Scope scope, byte scratchIndex, List<IParameterSymbol> nulledOptionals)
        {
            this.code = code;
            this.scratchIndex = scratchIndex;
            this.scope = scope;
            this.nulledOptionals = nulledOptionals;
        }

        internal AssetRefPredefineds(CodeBuilder code)
        {
            this.code = code;
        }

        public void Id()
        {
            
        }
      
        public void Total()
        {
            
            code.asset_params_get("AssetTotal");
        }
        
        public void Decimals()
        {
            
            code.asset_params_get("AssetDecimals");
        }
        
        public void DefaultFrozen()
        {
            
            code.asset_params_get("AssetDefaultFrozen");
        }
        
        public void UnitName()
        {
            
            code.asset_params_get("AssetUnitName");
        }
        
        public void Name()
        {
            
            code.asset_params_get("AssetName");
        }
        
        public void URL()
        {
            
            code.asset_params_get("AssetURL");
        }
        
        public void MetadataHash()
        {
            
            code.asset_params_get("AssetMetadataHash");
        }
        
        public void Manager()
        {
          
            code.asset_params_get("AssetManager");
        }
        
        public void Reserve()
        {
            
            code.asset_params_get("AssetReserve");
        }
        
        public void Freeze()
        {
          
            code.asset_params_get("AssetFreeze");
        }
        
        public void Clawback()
        {
            
            code.asset_params_get("AssetClawback");
        }
        
        public void Creator()
        {
            
            code.asset_params_get("AssetCreator");
        }

        public void Transfer()
        {
            code.itxn_begin();
            code.int_literal_constant("axfer");
            code.itxn_field("TypeEnum");
            code.itxn_field("AssetAmount");
            code.itxn_field("AssetReceiver");
            code.uncover(4) ;
            code.itxn_field("XferAsset");
            code.itxn_submit();
        }

        public void Update()
        {
            code.itxn_begin();
            code.int_literal_constant("acfg");
            code.itxn_field("TypeEnum");
            code.uncover(2);
            code.itxn_field("XferAsset");
            if (!nulledOptionals.Any(p=>p.Name=="Clawback" ))
            {
                code.itxn_field("ConfigAssetClawback");
            }
            if (!nulledOptionals.Any(p => p.Name == "Freeze"))
            {
                code.itxn_field("ConfigAssetFreeze");
            }
            if (!nulledOptionals.Any(p => p.Name == "Reserve"))
            {
                code.itxn_field("ConfigAssetReserve");
            }
            if (!nulledOptionals.Any(p => p.Name == "Manager"))
            {
                code.itxn_field("ConfigAssetManager");
            }

            code.itxn_submit();

            
        }




    }
}
