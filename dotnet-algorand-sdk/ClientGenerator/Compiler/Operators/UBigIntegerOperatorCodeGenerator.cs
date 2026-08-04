using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;
using System;

namespace AVM.ClientGenerator.Compiler.Operators
{
    /// <summary>
    /// This class is obsolete, but I am leaving it in for potential use by optimisers.
    /// </summary>
    [Obsolete]
    internal class UBigIntegerOperatorCodeGenerator : TypeCodeGenerator
    {
        

        public UBigIntegerOperatorCodeGenerator(CodeBuilder code) : base(code)
        {
        }

        internal override void Increment()
        {
            _code.byte_literal_constant("0x01");
            _code.b_plus();

        }

        internal override void Decrement()
        {
            _code.byte_literal_constant("0x01");
            _code.b_minus();
        }


        internal override void Addition()
        {
            _code.b_plus();
        }

        internal override void BitwiseAnd()
        {
            _code.b_bitwise_and();
        }

        internal override void BitwiseOr()
        {
            _code.b_bitwise_or();
        }
        internal override void BitwiseXor()
        {
            _code.b_bitwise_xor();

        }
        internal override void Division()
        {
            _code.b_divide();
        }

        internal override void LeftShift()
        {
            throw new ErrorDiagnosticException("E006");
        }
        internal override void GreaterThan()
        {
            _code.b_greater_than();
        }

        internal override void GreaterThanOrEqual()
        {
            _code.b_greater_than_or_equal();
        }
        internal override void LessThan()
        {
            _code.b_less_than();
        }

        internal override void LessThanOrEqual()
        {
            _code.b_less_than_or_equal();
        }
        internal override void Multiplication()
        {
            _code.b_multiply();
        }

        internal override void Remainder()
        {
            _code.b_modulo();
        }

        internal override void RightShift()
        {
            throw new ErrorDiagnosticException("E006");
        }

        internal override void Subtraction()
        {
            _code.b_minus();
        }

        internal override void LogicalNegate()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void Negate()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void OnesComplement()
        {
            _code.b_bitwise_not();
        }
    }
}
