using System;

namespace AVM.ClientGenerator.Compiler.Exceptions
{
    internal abstract class DiagnosticException : Exception
    {
        internal string Diagnostic { get; private set; }

        protected DiagnosticException(string diagnostic)
        {
            Diagnostic = diagnostic;
        }

    }
}
