using AVM.ClientGenerator.Compiler.CompiledCodeModel;
using AVM.ClientGenerator.Compiler.Exceptions;

namespace AVM.ClientGenerator.Compiler.Operators
{
    /// <summary>
    /// Generate arithmetic code to take two TEAL ulong represented integers
    /// and output unsigned byte masked result
    /// </summary>
    internal class ObjectOperatorCodeGenerator : TypeCodeGenerator
    {
        const ulong byteMask = 0x_00_00_00_00_00_00_00_ff;
        const ulong shiftOperatorCountLimiter = 0x_00_00_00_00_00_00_00_1f; //C# lang specification restricts int or int convertible types to lower 5 bits of shift count

        public ObjectOperatorCodeGenerator(CodeBuilder code) : base(code)
        {
        }
        internal override void Increment()
        {

            throw new ErrorDiagnosticException("E017");
        }

        internal override void Decrement()
        {
            throw new ErrorDiagnosticException("E017");
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
            throw new ErrorDiagnosticException("E017");
        }

        internal override void Addition()
        {
            throw new ErrorDiagnosticException("E017");

        }

        internal override void BitwiseAnd()
        {
            throw new ErrorDiagnosticException("E017");

        }

        internal override void BitwiseOr()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void BitwiseXor()
        {
            throw new ErrorDiagnosticException("E017");

        }

        internal override void Division()
        {

            throw new ErrorDiagnosticException("E017");
        }

       

        internal override void LeftShift()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void GreaterThan()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void GreaterThanOrEqual()
        {
            throw new ErrorDiagnosticException("E017");
        }
        internal override void LessThan()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void LessThanOrEqual()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void Multiplication()
        {
            throw new ErrorDiagnosticException("E017");

        }

        internal override void Remainder()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void RightShift()
        {
            throw new ErrorDiagnosticException("E017");

        }

        internal override void Subtraction()
        {

            throw new ErrorDiagnosticException("E017");



        }
    }
}
