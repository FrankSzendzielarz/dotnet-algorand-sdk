using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Predefineds;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class ByteArrayScratchVariable : ScratchVariable
    {
        internal ByteArrayScratchVariable(string name) : base(name,Core.VariableType.ByteSlice) { }

     

        internal override void AddLoadCode(CodeBuilder code, Scope scope)
        {
            if (byte.TryParse(Name, out byte index))
            {
                code.load(index, scope);
            }
            else
            {
                throw new Exception($"Invalid scratch space key {Name} ");
            }

        }

        internal override void AddSaveCode(CodeBuilder code, Scope scope)
        {
            if (byte.TryParse(Name, out byte index))
            {
                code.store(index, scope);
            }
            else
            {
                throw new Exception($"Invalid scratch space key {Name} ");
            }
        }

        internal override void InvokeMethod(CodeBuilder code, Scope scope, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            if (byte.TryParse(Name, out byte index))
            {
                ByteArrayPredefineds predefineds = new ByteArrayPredefineds(code, scope, index, nulledOptionals, literals);
                var type = predefineds.GetType();
                var method = type.GetMethod(identifier);
                if (method != null)
                {
                    method.Invoke(predefineds, new object[] { });
                }
                else
                {
                    throw new Exception($"Compiler error. The byte array reference property {identifier} does not exist. ");
                };
            }
            else
            {
                throw new Exception($"Invalid scratch var index {Name} ");
            }
        }

        internal override void AddReferencedLoadCode(CodeBuilder code, Scope scope, SyntaxToken identifier, Core.StorageType storageType)
        {
        
                if (byte.TryParse(Name, out byte index))
                {
                    ByteArrayPredefineds predefineds = new ByteArrayPredefineds(code, scope, index, new List<IParameterSymbol>(),null);
                    var type = predefineds.GetType();
                    var method = type.GetMethod(identifier.ValueText);
                    if (method != null)
                    {
                        method.Invoke(predefineds, new object[] { });
                    }
                    else
                    {
                        throw new ErrorDiagnosticException("E047");
                    }
                }
                else
                {
                    throw new Exception($"Invalid scratch index {Name} ");
                }

       
        }

        internal override void AddReferencedSaveCode(CodeBuilder code, Scope scope, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new ErrorDiagnosticException("E025");
        }
    }
}
