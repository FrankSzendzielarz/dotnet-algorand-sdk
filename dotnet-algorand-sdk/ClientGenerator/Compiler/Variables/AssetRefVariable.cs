using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Predefineds;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class AssetRefVariable : ScratchVariable
    {
        internal static bool IsAssetRef(ITypeSymbol typeSymbol)
        {
            return typeSymbol.ToString() == "AVM.ClientGenerator.Core.AssetReference";
        }

        internal AssetRefVariable(string name) : base(name, Core.VariableType.UlongReference) { }
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
            if (storageType == Core.StorageType.Protocol) //TODO - why are 
            {
                if (byte.TryParse(Name, out byte index))
                {
                    AssetRefPredefineds predefineds = new AssetRefPredefineds(code, scope, index, new List<IParameterSymbol>());
                    var type = predefineds.GetType();
                    var method = type.GetMethod(identifier.ValueText);
                    if (method != null)
                    {
                        method.Invoke(predefineds, new object[] { });
                    }
                    else
                    {
                        throw new Exception($"Compiler error. The asset reference property {identifier.ValueText} does not exist. ");
                    }
                }
                else
                {
                    throw new Exception($"Invalid asset ref index {Name} ");
                }

            }
            else
            {
                throw new ErrorDiagnosticException("E025");
            }
        }

        internal override void InvokeMethod(CodeBuilder code, Scope scope, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            if (byte.TryParse(Name, out byte index))
            {
                AssetRefPredefineds predefineds = new AssetRefPredefineds(code, scope, index, nulledOptionals);
                var type = predefineds.GetType();
                var method = type.GetMethod(identifier);
                if (method != null)
                {
                    method.Invoke(predefineds, new object[] { });
                }
                else
                {
                    throw new Exception($"Compiler error. The asset reference property {identifier} does not exist. ");
                };
            }
            else
            {
                throw new Exception($"Invalid asset index {Name} ");
            }
        }
    }
}
