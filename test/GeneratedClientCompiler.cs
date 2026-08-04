using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace test
{
    /// <summary>
    /// Compiles freshly-generated smart-contract client source code in-memory to prove it is valid,
    /// standalone C#, then publishes it into the checked-in test/Generated folder so it is compiled
    /// as part of every normal test build rather than only when the generator test happens to run.
    /// </summary>
    public static class GeneratedClientCompiler
    {
        /// <summary>
        /// Compiles ARC-generated proxy/reference source code into an in-memory assembly, referencing every
        /// assembly already loaded in the current process (which includes the SDK, AVM.ClientGenerator
        /// runtime types and BCL assemblies used by the generated code).
        /// </summary>
        public static Assembly CompileGeneratedClient(string sourceCode, string assemblyName)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode, new CSharpParseOptions(LanguageVersion.Latest));

            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                .Select(a => (MetadataReference)MetadataReference.CreateFromFile(a.Location))
                .ToList();

            var compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using var ms = new MemoryStream();
            var emitResult = compilation.Emit(ms);
            var errors = emitResult.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error).ToList();
            Assert.That(emitResult.Success, Is.True, "Generated client failed to compile:" + Environment.NewLine + string.Join(Environment.NewLine, errors));

            ms.Seek(0, SeekOrigin.Begin);
            return Assembly.Load(ms.ToArray());
        }

        /// <summary>
        /// Proves that freshly generated proxy/reference source is valid, standalone C# by compiling it, then - only
        /// once compilation succeeds - publishes it into the checked-in test/Generated folder so it is
        /// compiled as part of every normal test build rather than only when this generator test happens to run.
        /// </summary>
        public static void CompileAndPublishGeneratedClient(string fileName, string sourceCode, [CallerFilePath] string testFilePath = "")
        {
            CompileGeneratedClient(sourceCode, Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid().ToString("N"));

            var generatedDir = Path.Combine(Path.GetDirectoryName(testFilePath)!, "Generated");
            Directory.CreateDirectory(generatedDir);
            File.WriteAllText(Path.Combine(generatedDir, fileName), sourceCode);
        }
    }
}
