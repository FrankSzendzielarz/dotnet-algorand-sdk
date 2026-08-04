using System;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace IlRepackMergeTask
{
    public class ILRepackMerge : Task
    {
        [Required]
        public ITaskItem[] InputAssemblies { get; set; }

        [Required]
        public string OutputFile { get; set; }

        // Directories to resolve *unmerged* external references against (e.g. Microsoft.Bcl.AsyncInterfaces,
        // a RestSharp dependency that isn't itself internalized/merged) - without these, ILRepack's
        // reference-fixing step throws Mono.Cecil.AssemblyResolutionException for any type reference
        // that points outside the merged assembly set.
        public ITaskItem[] SearchDirectories { get; set; }

        public override bool Execute()
        {
            var options = new ILRepacking.RepackOptions
            {
                OutputFile = OutputFile,
                InputAssemblies = InputAssemblies.Select(a => a.ItemSpec).ToArray(),
                TargetKind = ILRepacking.ILRepack.Kind.SameAsPrimaryAssembly,
                Internalize = true,
                UnionMerge = true,
                DebugInfo = true,
                AllowDuplicateResources = false,
                Parallel = false,
                // ILRepack.Lib.MSBuild's own <ILRepack> task never exposes this option, which is
                // the entire reason this task exists - see ILRepack.Targets for why.
                NoRepackRes = true,
                SearchDirectories = (SearchDirectories ?? Array.Empty<ITaskItem>())
                    .Select(d => d.ItemSpec)
                    .Distinct()
                    .ToList(),
            };
            new ILRepacking.ILRepack(options).Repack();
            return true;
        }
    }
}
