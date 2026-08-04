
using AVM.ClientGenerator.Compiler.Predefineds;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Variables
{
    internal class IntegerScratchVariable : ScratchVariable
    {
        internal IntegerScratchVariable(string name) : base(name,Core.VariableType.UInt64) { }

        internal override void InvokeMethod(CodeBuilder code, Scope scope, string identifier, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals, InvocationExpressionSyntax node = null)
        {
            if (byte.TryParse(Name, out byte index))
            {
                IntegerPredefineds predefineds = new IntegerPredefineds(code, scope, index, nulledOptionals, literals);
                var type = predefineds.GetType();

                var method = type.GetMethod(identifier);
                if (method != null)
                {
                    method.Invoke(predefineds, new object[] { });
                }
                else
                {
                    throw new Exception($"Compiler error. The integer method {identifier} does not exist. ");
                };
            }
            else
            {
                throw new Exception($"Invalid scratch var index {Name} ");
            }
        }


       
    }
}
