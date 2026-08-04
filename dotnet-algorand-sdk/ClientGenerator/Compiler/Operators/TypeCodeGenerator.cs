using AVM.ClientGenerator.Compiler.CompiledCodeModel;

namespace AVM.ClientGenerator.Compiler.Operators
{



    /// <summary>
    /// Type code generators handle operations on specific types.
    /// </summary>
    internal abstract class TypeCodeGenerator 
    {
        protected CodeBuilder _code;
        internal TypeCodeGenerator(CodeBuilder code) { _code = code; }
        
        internal abstract void Multiplication();
        internal abstract void Division();
        internal abstract void Remainder();
        internal abstract void Addition();
        internal abstract void Subtraction();
        internal abstract void LeftShift();
        internal abstract void RightShift();
        internal abstract void BitwiseAnd();
        internal abstract void BitwiseOr();
        internal abstract void BitwiseXor();
        internal abstract void LessThan();
        internal abstract void GreaterThan();
        internal abstract void LessThanOrEqual();
        internal abstract void GreaterThanOrEqual();
        internal virtual void EqualsExpression()
        {
            _code.equals();

        }
        internal virtual void NotEquals()
        {
            _code.not_equals();
            
        }

        internal abstract void LogicalNegate();
        

        internal abstract void Negate();
        internal abstract void OnesComplement();
        internal abstract void Increment();
        internal abstract void Decrement();
    }
}
