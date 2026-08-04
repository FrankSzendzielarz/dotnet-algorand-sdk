using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class LocalVariable : Variable
    {
        internal LocalVariable(string name, Core.VariableType valueType) : base(name, valueType) { }

        internal override void AddReferencedLoadCode(CodeBuilder code, Scope _, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new ErrorDiagnosticException("E025");
        }

        internal override void AddReferencedSaveCode(CodeBuilder code, Scope _, SyntaxToken identifier, Core.StorageType storageType)
        {
            throw new ErrorDiagnosticException("E025");
        }

        internal override void AddLoadCode(CodeBuilder code, Scope _)
        {
           
            code.loadabsolute((byte)(Core.Constants.ScratchSpaceSize - 1));
            code.byte_string_literal(Name);
            code.app_local_get() ;
        }

        internal override void AddSaveCode(CodeBuilder code, Scope _)
        {
            code.loadabsolute((byte)(Core.Constants.ScratchSpaceSize - 1));
            code.byte_string_literal(Name);
            code.uncover(2);
            code.app_local_put();
        }

        internal override void InvokeMethod(CodeBuilder code, Scope _, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            throw new ErrorDiagnosticException("E025");
        }
    }
}
