using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Operators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class ExpressionStatement : CompilerState
    {
        internal ExpressionStatement(CompilerState parent) : base(parent) {
            base.ExpectingExpressionReturnValue = false;
        }




        internal override CompilerState EnterExpressionEvaluationScope(bool i)
        {
            return new ExpressionEvaluation(this,i);
        }

       
    

       
     

       
      
        internal override void CallMethodOrFunction(IMethodSymbol methodOrFunction, List<IParameterSymbol> nulledOptionals, Dictionary<string, string> literals = null)
        {
            CallSub(methodOrFunction, nulledOptionals, literals);
        }



        internal override void DeclareGlobalVariable(ISymbol symbol, Core.VariableType valueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareLocalVariable(ISymbol symbol, Core.VariableType machineValueType)
        {
            throw new ErrorDiagnosticException("E007");
        }

        internal override void DeclareScratchVariable(ISymbol symbol, Core.VariableType valueType)
        {

            AddScratchVariable(symbol,valueType);
        }

        internal override void DeclareValueTupleVariable(ISymbol symbol, SemanticModel model)
        {
            if (Parent != null)
            {
                Parent.DeclareValueTupleVariable(symbol, model);

            }
            else
            {
                throw new ErrorDiagnosticException("E008");
            }
        }

        internal override void ObjectCreation(ObjectCreationExpressionSyntax methodOrFunction, List<IParameterSymbol> nulledOptionals, SemanticModel semanticModel, Dictionary<string, string> literals = null)
        {
            if (Parent != null)
            {
                Parent.ObjectCreation(methodOrFunction,nulledOptionals, semanticModel,literals);
            }
            else
            {
                throw new ErrorDiagnosticException("E006");
            }
        }
        


        internal override CompilerState EnterClassScope()
        {
            throw new ErrorDiagnosticException("E009");
        }

        internal override CompilerState EnterCodeBlockScope()
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterLoopScope()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override CompilerState EnterByteArrayInitialiserScope()
        {
            return new ByteArrayInitialiser(this);
        }

        internal override CompilerState EnterFunctionScope(ISymbol method, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E009");
        }
        internal override CompilerState EnterInnerTransactionScope(ISymbol method, SemanticModel semanticModel)
        {
            throw new ErrorDiagnosticException("E009");
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
            ReadVariableCode(symbol);
        }
      

        internal override CompilerState Leave()
        {
            return Parent;
        }

        internal override void ReadFromVariable(ISymbol symbol)
        {
            ReadVariableCode(symbol);
        }
        internal override void YieldExpressionResult()
        {
            Duplicate();
        }

        internal override void StoreToVariable(ISymbol symbol)
        {

            StoreVariableCode(symbol);
        }
        internal override void BuildValueFromStack(ISymbol construct)
        {
            throw new ErrorDiagnosticException("E009");
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

        internal override CompilerState EnterIfStatementScope()
        {
            throw new ErrorDiagnosticException("E009");
        }

     
        internal override void Cast(string fromType, string toType)
        {
            try
            {
                var typecodegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(fromType, Code);
                if (typecodegen is NumericCodeGenerator codegen)
                {
                    switch (toType)
                    {
                        
                        case "byte":
                            codegen.CastToByte();
                            break;
                        case "sbyte":
                            codegen.CastToSbyte();
                            break;
                        case "int":
                            codegen.CastToInt();
                            break;
                        case "uint":
                            codegen.CastToUInt();
                            break;
                        case "long":
                            codegen.CastToLong();
                            break;
                        case "ulong":
                            codegen.CastToULong();
                            break;
                        case "short":
                            codegen.CastToShort();
                            break;
                        case "ushort":
                            codegen.CastToUShort();
                            break;
                        case "decimal":
                            codegen.CastToDecimal();
                            break;
                        case "object":
                            codegen.CastToObject();
                            break;


                        default:
                            throw new WarningDiagnosticException("W006");
                    }
                }
                
            }
            catch
            {
                throw new WarningDiagnosticException("W006");
            }
        }
    }
}
