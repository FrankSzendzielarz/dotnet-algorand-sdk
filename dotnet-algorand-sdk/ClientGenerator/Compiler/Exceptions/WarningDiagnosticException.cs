namespace AVM.ClientGenerator.Compiler.Exceptions
{
    internal class WarningDiagnosticException : DiagnosticException
    {
        internal WarningDiagnosticException(string diagnostic) : base(diagnostic) { }
    }
}
