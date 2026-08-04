using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class ABIStructScratchVariable : ScratchVariable
    {
        internal ABIStructScratchVariable(string name) : base(name, Core.VariableType.ByteSlice) { }



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
            throw new ErrorDiagnosticException("E059");
        }

        internal override void AddReferencedLoadCode(CodeBuilder code, Scope scope, SyntaxToken identifier, Core.StorageType storageType)
        {




        }

        internal override void AddReferencedSaveCode(CodeBuilder code, Scope scope, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new ErrorDiagnosticException("E025");
        }
    }
}
