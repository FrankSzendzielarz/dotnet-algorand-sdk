using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class SmartContractLibraryFunction : CompilerState
    {
        protected IMethodSymbol methodSymbol;
        internal SmartContractLibraryFunction(CompilerState parent, IMethodSymbol func, SemanticModel semanticModel) : base(parent)
        {
            //Root scope for method
            Scope = Scope.NewChildScope();
            Scope.Label = func;

            //New Code target associated with the new scope
            var code= new SmartContractLibraryMethod(Scope);
            Code = code;

            //Add this code to the CompilationGroup
            CompilationGroup.LibraryMethods.Add(code);
            
            //Needed for when leaving the compiler state 
            methodSymbol = func;

            //label the library function
            Code.AddEmptyLine();
            var name= code.GetLibraryMethodLabel();
            Code.Name = name;
            Code.AddLabel(name);
            

            List<Variable> variables = new List<Variable>();
            foreach (var param in func.Parameters)
            {
                if (param.RefKind == RefKind.None || param.RefKind == RefKind.In)
                {
                    if (AssetRefVariable.IsAssetRef(param.Type))
                    {
                        variables.Add(Scope.AddAssetRefVariable(param));
                    }
                    else
                    if (AccountRefVariable.IsAccountRef(param.Type))
                    {
                        variables.Add(Scope.AddAccountRefVariable(param));
                    }
                    else
                    if (ApplicationRefVariable.IsApplicationRef(param.Type))
                    {
                        variables.Add(Scope.AddApplicationRefVariable(param));
                    }
                    else
                    if (TransactionRefVariable.IsTxRef(param.Type))
                    {
                        variables.Add(Scope.AddTransactionRefVariable(param));
                    }
                    else
                    {
                        Core.VariableType machineValueType = TealTypeUtils.DetermineType(semanticModel, (param.DeclaringSyntaxReferences.First().GetSyntax() as ParameterSyntax).Type);
                        var sv = AddScratchVariable(param, machineValueType);
                        variables.Add(sv);
                    }
                }

            }
            variables.Reverse(); //reverse order because of stack
            foreach (var variable in variables)
            {
                variable.AddSaveCode(Code, Scope);
            }
        }

      


        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            CallSub(methodOrFunction, nulledOptionals, literals);
        }

        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E007");
        }
        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            return new ByteArrayInitialiser(this);
        }
        internal override CompilerState EnterGoto()
        {
            return new Goto(this);
        }
        internal override CompilerState EnterIfStatementScope()
        {
            return new IfStatement(this);
        }
        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {
            AddScratchVariable(symbol, valueType);
        }
        internal override void YieldExpressionResult()
        {
            //Do nothing
        }

        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this,i);
        }
        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            return new CodeBlock(this);
        }

        internal override CompilerState EnterLoopScope()
        {
            return new Loop(this);
        }


        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {

            return new LocalFunction(this, method as IMethodSymbol, semanticModel);
        }
        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
        {

            return new InnerTransactionLocalFunction(this, method as IMethodSymbol, semanticModel);
        }

        internal override CompilerState EnterSmartContractProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterSmartSignatureProgramScope(IMethodSymbol methodSymbol)
        {
            throw new ErrorDiagnosticException("E009");
        }

       

        internal override void IdentifierNameSyntax(ISymbol symbol)
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override void Return(bool hasReturnValue)
        {
            Code.retsub();
        }

        internal override CompilerState Leave()
        {
            //handle out vars by pushing them onto the stack. they are then loaded by the calling scope
            List<ScratchVariable> scratchVariables = new List<ScratchVariable>();
            foreach (var param in methodSymbol.Parameters)
            {
                if (param.RefKind == RefKind.Out)
                {
                    ReadFromVariable(param);
                }
            }



            //ret out
            Code.retsub();
            return Parent;
        }

        internal override void ReadFromVariable(ISymbol symbol)
        {
            ReadVariableCode(symbol);
        }

        internal override void StoreToVariable(ISymbol symbol)
        {
            StoreVariableCode(symbol);
        }
        internal override void AddLiteralByteConstant(byte[] value)
        {
            PushLiteralByteArrayToStack(value);

        }
        internal override void AddLiteralNumeric(ulong value)
        {
            PushLiteralNumericToStack(value);
        }
        internal override void AddLiteralString(string value)
        {
            PushLiteralStringToStack(value);
        }

        internal override CompilerState EnterOutArgumentScope(ArgumentSyntax node, SemanticModel model)
        {
            return new OutArgument(this);
        }

    }
}
