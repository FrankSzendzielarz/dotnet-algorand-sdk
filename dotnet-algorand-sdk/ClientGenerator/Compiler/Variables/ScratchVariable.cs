using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class ScratchVariable : Variable
    {
        internal ScratchVariable(string name,Core.VariableType valueType) : base(name, valueType) { }

        internal override void AddReferencedLoadCode(CodeBuilder code, Scope _, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new ErrorDiagnosticException("E025");
        }

        internal override void AddReferencedSaveCode(CodeBuilder code, Scope _, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new ErrorDiagnosticException("E025");
        }

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

        internal override void InvokeMethod(CodeBuilder code, Scope _, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            throw new ErrorDiagnosticException("E025");
        }
    }
}
