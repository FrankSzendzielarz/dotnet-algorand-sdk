using Microsoft.CodeAnalysis;
using AVM.ClientGenerator.Compiler.Exceptions;
using System;
using AVM.ClientGenerator.Compiler.Predefineds;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class ApplicationRefVariable : ScratchVariable
    {
        internal static bool IsApplicationRef(ITypeSymbol typeSymbol)
        {
            if (typeSymbol==null) return false;
            if (typeSymbol.ToString() == "AVM.ClientGenerator.Core.SmartContractReference") return true;
            return IsApplicationRef(typeSymbol.BaseType);
            
            
        }
        internal ApplicationRefVariable(string name) : base(name,Core.VariableType.UlongReference) { }
        //internal override void AddLoadCode(CodeBuilder code, Scope _)
        //{
        //    if (byte.TryParse(Name, out byte index))
        //    {
        //        code.int_literal_constant(index);
        //    }
        //    else
        //    {
        //        throw new Exception($"Invalid asset ref index {Name} ");
        //    }

        //}

        //internal override void AddSaveCode(CodeBuilder code, Scope _)
        //{
        //    throw new ErrorDiagnosticException("E023");
        //}

        internal override void AddReferencedSaveCode(CodeBuilder code, Scope _, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new ErrorDiagnosticException("E024");
     
        }
        internal override void AddReferencedLoadCode(CodeBuilder code, Scope scope, SyntaxToken identifier, Core.StorageType storageType)
        {
            //no longer used.... this is part of the work in progress re-architecture where CompilerState.LoadFromType takes care of things
            if (byte.TryParse(Name, out byte index))
            {
                switch (storageType)
                {
                    case Core.StorageType.Local:
                        code.loadabsolute((byte)(Core.Constants.ScratchSpaceSize - 1));  //the account reference
                        code.load(index, scope);                                    //the app id
                        code.byte_string_literal(identifier.ValueText);           //the key
                        code.app_local_get_ex();
                        code.pop();                                                 //discard the top 'did exist' flag . User must use app_opted_in
                        break;
                    case Core.StorageType.Global:
                        code.load(index, scope);                                    //the app id
                        code.byte_string_literal(identifier.ValueText);           //the key
                        code.app_global_get_ex();                                   //
                        code.pop();                                                 //discard the top 'did exist' flag . 
                        break;
                    case Core.StorageType.Protocol:
                        SmartContractRefPredefineds predefineds = new SmartContractRefPredefineds(code, scope,index,new List<IParameterSymbol>());
                        var type = predefineds.GetType();
                        var method = type.GetMethod(identifier.ValueText);
                        if (method != null)
                        {
                            method.Invoke(predefineds, new object[] { });
                        }
                        else
                        {
                            throw new Exception($"Compiler error. The application reference property {identifier.ValueText} does not exist. ");
                        };
                        break;
                    default: 
                        throw new ErrorDiagnosticException("E021");
                }
            }
            else
            {
                throw new Exception($"Invalid application index {Name} ");
            }
        }

        internal override void InvokeMethod(CodeBuilder code, Scope _, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            throw new ErrorDiagnosticException("W004");
        }
    }
}
