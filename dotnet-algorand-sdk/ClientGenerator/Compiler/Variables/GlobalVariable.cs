using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class GlobalVariable : Variable
    {
        internal GlobalVariable(string name, Core.VariableType valueType) : base(name, valueType) { }

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
            code.byte_string_literal(Name);
            code.app_global_get();
        }

        internal override void AddSaveCode(CodeBuilder code, Scope _)
        {
            code.byte_string_literal(Name);
            code.swap();
            code.app_global_put();
        }
        internal override void InvokeMethod(CodeBuilder code, Scope _, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            throw new ErrorDiagnosticException("E025");
        }
    }
}
