namespace AVM.ClientGenerator.Compiler.Exceptions
{
    internal class ErrorDiagnosticException : DiagnosticException
    {
        internal ErrorDiagnosticException(string diagnostic) : base(diagnostic) { }
        
    }
}
