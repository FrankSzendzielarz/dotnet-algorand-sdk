using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Resources;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AVM.ClientGenerator
{

    /// <summary>
    /// This adopts the convention of a four digit diagnostics code prefix, with three suffixes per code: Title, Description and MessageFormat.
    /// 
    /// For example, the resx file may contain:
    /// 
    /// I000Description	     | Generate Algorand TEAL.              | Optional longer error description
    /// I000MessageFormat    | Generate '{0}' into Algorand TEAL.   | The format-able message the diagnostic displays.
    /// I000Title            | TEAL Generator                       | The title of the diagnostic.
    /// 
    /// The FIRST char of the four char code represents severity:
    /// 
    ///     I INFO
    ///     W WARNING
    ///     E ERROR
    /// 
    /// Diagnostics rules are built from these.
    /// </summary>
    internal static class DiagnosticDescriptors
    {
        internal static ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; private set; }

        private static Dictionary<string, DiagnosticDescriptor> DiagnosticRules { get;  set; }

        static DiagnosticDescriptors()
        {
            ResourceSet allResources = Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true);
            
            var resourceDict= allResources.Cast<DictionaryEntry>().ToDictionary(r => r.Key.ToString(), r => r.Value.ToString());

            var allRules = resourceDict
                .Where(de => de.Key.Length > 4)
                .GroupBy(de => de.Key.Substring(0, 4))
                .Select(resg => ResourceGroupToRule(resg));


            SupportedDiagnostics = allRules.ToImmutableArray();

            DiagnosticRules = allRules.ToDictionary(k => k.Id, k => k);



        }

        private static DiagnosticDescriptor ResourceGroupToRule(IGrouping<string, KeyValuePair<string, string>> resg)
        {
            string ruleId = resg.Key;

            var ruleTitle = new LocalizableResourceString($"{ruleId}Title", Resources.ResourceManager, typeof(Resources));
            var ruleFormat = new LocalizableResourceString($"{ruleId}MessageFormat", Resources.ResourceManager, typeof(Resources));
            var ruleDescription = new LocalizableResourceString($"{ruleId}Description", Resources.ResourceManager, typeof(Resources));

            DiagnosticSeverity severity;

            switch (ruleId[0])
            {
                case 'I': severity = DiagnosticSeverity.Info; break;
                case 'W': severity = DiagnosticSeverity.Warning; break;
                case 'E': severity = DiagnosticSeverity.Error; break;
                default: severity = DiagnosticSeverity.Info; break;
            }

            return new DiagnosticDescriptor(ruleId, ruleTitle , ruleFormat, "Algorand", severity, isEnabledByDefault: true, description: ruleDescription);

            
        }

        internal static Diagnostic Create(string id, Location location,  [CallerLineNumber] int line = 0, [CallerMemberName] string caller = "", params object[] messageArgs)
        {
            if (DiagnosticRules.TryGetValue(id, out DiagnosticDescriptor rule))
            {
                string compilerReference = $"Compiler reference: line {line} of {caller}.";
                return Diagnostic.Create(rule, location, messageArgs.ToList().Append(compilerReference).ToArray());
            }
            else
            {
                return null;
            }

            
        }
        
    }
}
