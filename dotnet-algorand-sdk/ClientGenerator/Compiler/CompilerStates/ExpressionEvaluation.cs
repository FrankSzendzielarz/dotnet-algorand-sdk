using AVM.ClientGenerator.Compiler.Exceptions;
using AVM.ClientGenerator.Compiler.Operators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AVM.ClientGenerator.Compiler.CompilerStates
{
    internal class ExpressionEvaluation : CompilerState
    {
        protected bool isVoid;
        internal ExpressionEvaluation(CompilerState parent, bool isVoid) : base(parent) {
            this.isVoid = isVoid;
        }



        internal override CompilerState EnterExpressionEvaluationScope(bool isVoid)
        {
            return new ExpressionEvaluation(this,isVoid);
        }

        internal override string ConditionalAndLeft()
        {
            var exitFalselabel = Code.ReserveLabel();
            Code.dup();
            Code.bz(exitFalselabel);
            Code.pop();
            return exitFalselabel;


        }
        internal override void ConditionalAndRight(string exitFalseLabel)
        {

            Code.AddLabel(exitFalseLabel);

        }
        internal override string ConditionalOrLeft()
        {
            var exitTrueLabel = Code.ReserveLabel();
            Code.dup();
            Code.bnz(exitTrueLabel);
            Code.pop();
            return exitTrueLabel;

        }
        internal override void ConditionalOrRight(string exitTrueLabel)
        {
            Code.AddLabel(exitTrueLabel);
        }

        internal override void UnaryOperator(TealTypeUtils.UnaryModifier modifier, string typeName)
        {

            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                switch (modifier)
                {
                    case TealTypeUtils.UnaryModifier.LogicalNegate:
                        codegen.LogicalNegate();
                        break;
                    case TealTypeUtils.UnaryModifier.Minus:
                        codegen.Negate();
                        break;
                    case TealTypeUtils.UnaryModifier.OnesComplement:
                        codegen.OnesComplement();
                        break;


                      
                }
            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }


           
        }
        internal override void Addition(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.Addition();
            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }

        internal override void Subtraction(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.Subtraction();
            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }

        internal override void Multiplication(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.Multiplication();
            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }
        internal override void Division(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.Division();
            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }
        internal override void Remainder(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.Remainder();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }

        internal override void LeftShift(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.LeftShift();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }

        internal override void RightShift(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.RightShift();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }

        internal override void BitwiseAnd(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.BitwiseAnd();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }
        internal override void BitwiseOr(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.BitwiseOr();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }

        internal override void LessThan(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.LessThan();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }

        internal override void ExclusiveOr(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.BitwiseXor();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }

        internal override void LessThanOrEquals(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.LessThanOrEqual();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }

        internal override void GreaterThan(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.GreaterThan();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }
        internal override void GreaterThanOrEquals(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.GreaterThanOrEqual();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }


        internal override void EqualsExpression(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.EqualsExpression();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
        }
        internal override void NotEquals(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.NotEquals();

            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
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
            if (!Parent.ExpectingExpressionReturnValue)
            {
                //if the expression return type is not 'void'
                if (!isVoid)
                {
                    Code.pop();//discard expression result
                }
            }
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
            return new IfStatement(this);
        }

        internal override void Increment(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.Increment();
            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }

        }
        internal override void Decrement(string typeName)
        {
            try
            {
                var codegen = TypeCodeGeneratorFactory.GetOperatorCodeGenerator(typeName, Code);
                codegen.Decrement();
            }
            catch
            {
                throw new ErrorDiagnosticException("E017");
            }
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
