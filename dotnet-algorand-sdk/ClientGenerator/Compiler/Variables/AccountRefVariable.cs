using AVM.ClientGenerator.Compiler.Predefineds;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class AccountRefVariable : ScratchVariable
    {
        internal static bool IsAccountRef(ITypeSymbol typeSymbol)
        {
            return typeSymbol.ToString() == "AVM.ClientGenerator.Core.AccountReference";
        }
        internal AccountRefVariable(string name) : base(name, Core.VariableType.ByteArrayReference) { }
      

        internal override void AddReferencedSaveCode(CodeBuilder code, Scope _, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new System.NotImplementedException();
        }
        internal override void AddReferencedLoadCode(CodeBuilder code, Scope _, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new System.NotImplementedException();
        }

        internal override void InvokeMethod(CodeBuilder code, Scope scope, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            if (byte.TryParse(Name, out byte index))
            {
                AccountRefPredefineds predefineds = new AccountRefPredefineds(code,scope, index,nulledOptionals);
                var type = predefineds.GetType();
                var method = type.GetMethod(identifier);
                if (method != null)
                {
                    method.Invoke(predefineds, new object[] { });
                }
                else
                {
                    throw new Exception($"Compiler error. The application reference property {identifier} does not exist. ");
                };
                
            }
            else
            {
                throw new Exception($"Invalid account index {Name} ");
            }
        }
    }
}
