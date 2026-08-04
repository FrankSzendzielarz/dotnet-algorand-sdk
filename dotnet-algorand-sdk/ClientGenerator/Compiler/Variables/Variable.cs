using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal abstract class Variable
    {
        internal string Name { get; private set; }

        internal Core.VariableType ValueType { get; set; }
        protected Variable(string name, Core.VariableType valueType)
        {
            Name = name;
            ValueType = valueType;
        }

        internal abstract void AddLoadCode(CodeBuilder code, Scope scope);
        internal abstract void AddSaveCode(CodeBuilder code, Scope scope);
        internal abstract void AddReferencedSaveCode(CodeBuilder code, Scope scope, SyntaxToken identifier, Core.StorageType storageType);
        internal abstract void AddReferencedLoadCode(CodeBuilder code, Scope scope, SyntaxToken identifier, Core.StorageType storageType);
        internal abstract void InvokeMethod(CodeBuilder code, Scope scope, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node=null);



    }
}
