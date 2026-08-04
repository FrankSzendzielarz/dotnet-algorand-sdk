using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Predefineds;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class TransactionRefVariable : ScratchVariable
    {
        internal static bool IsTxRef(ITypeSymbol typeSymbol)
        {
            switch (typeSymbol.ToString())
            {
                case "AVM.ClientGenerator.Core.TransactionReference":
                case "AVM.ClientGenerator.Core.PaymentTransactionReference":
                case "AVM.ClientGenerator.Core.KeyRegistrationTransactionReference":
                case "AVM.ClientGenerator.Core.AssetAcceptTransactionReference":
                case "AVM.ClientGenerator.Core.AssetClawbackTransactionReference":
                case "AVM.ClientGenerator.Core.AssetConfigurationTransactionReference":
                case "AVM.ClientGenerator.Core.AssetFreezeTransactionReference":
                case "AVM.ClientGenerator.Core.AssetTransferTransactionReference":
                case "AVM.ClientGenerator.Core.AppCallTransactionReference":
                    return true;
                default:    
                    return false;

            }
            
        }

        internal TransactionRefVariable(string name) : base(name, Core.VariableType.UlongReference) { }
        //internal override void AddLoadCode(CodeBuilder code, Scope _)
        //{
        //    throw new ErrorDiagnosticException("E023");
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
            if (storageType == Core.StorageType.Protocol)
            {
                if (byte.TryParse(Name, out byte index))
                {
                    TransactionRefVariablePredefineds predefineds = new TransactionRefVariablePredefineds(code, scope, index, new List<IParameterSymbol>());
                    var type = predefineds.GetType();
                    var method = type.GetMethod(identifier.ValueText);
                    if (method != null)
                    {
                        method.Invoke(predefineds, new object[] { });
                    }
                    else
                    {
                        throw new Exception($"Compiler error. The transaction reference property {identifier.ValueText} does not exist. ");
                    }
                }
                else
                {
                    throw new Exception($"Invalid transaction ref index {Name} ");
                }
            }
            else
            {
                throw new ErrorDiagnosticException("E025");
            }
        }

        internal override void InvokeMethod(CodeBuilder code, Scope _, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            throw new WarningDiagnosticException("W005");
        }
    }
}
