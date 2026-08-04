using AVM.ClientGenerator.Compiler;
using AVM.ClientGenerator.Compiler.Variables;
using AVM.ClientGenerator.Core.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using AVM.ClientGenerator.ABI.ARC32;

namespace AVM.ClientGenerator.Clients
{
    public static class ProxyGenerator
    {

        internal static Dictionary<string, string> returnTypeConversions = new Dictionary<string, string>()
        {
            { "bool", "return BitConverter.ToBoolean(ReverseIfLittleEndian(lastLogReturnData), 0);"},
            { "byte","return ReverseIfLittleEndian(lastLogReturnData)[0];" },
            { "sbyte","return (sbyte)(ReverseIfLittleEndian(lastLogReturnData)[0]);"},
            { "char", "return BitConverter.ToChar(ReverseIfLittleEndian(lastLogReturnData), 0);"},
            { "int", "return BitConverter.ToInt32(ReverseIfLittleEndian(lastLogReturnData), 0);"},
            { "uint", "return BitConverter.ToUInt32(ReverseIfLittleEndian(lastLogReturnData), 0);"},
            { "long", "return BitConverter.ToInt64(ReverseIfLittleEndian(lastLogReturnData), 0);"},
            { "ulong", "return BitConverter.ToUInt64(ReverseIfLittleEndian(lastLogReturnData), 0);"},
            { "short", "return BitConverter.ToInt16(ReverseIfLittleEndian(lastLogReturnData), 0);"},
            { "ushort","return BitConverter.ToUInt16(ReverseIfLittleEndian(lastLogReturnData), 0);"},
            { "System.Numerics.BigInteger","return returnValueObj.Value;" }   ,
            { "byte[]","return returnValueObj.ToByteArray();" },
            { "string","return returnValueObj.ToString();" },
            { "string[]","return returnValueObj.ToStringArray();" },
            { "ulong[]","return returnValueObj.Value.Select(v => (ulong)v.ToValue()).ToArray();" },
            { "bool[]","return returnValueObj.Value.Select(v => (bool)v.ToValue()).ToArray();" },
            { "decimal","return GetDecimalFromBytes(lastLogReturnData);" },
            { typeof(Decimal).Name,"return GetDecimalFromBytes(lastLogReturnData);" },
            { "AVM.ClientGenerator.ABI.ARC4.Types.UInt512", "return returnValueObj;" },
            { "AVM.ClientGenerator.ABI.ARC4.Types.UInt256", "return returnValueObj;" },
            { "AVM.ClientGenerator.ABI.ARC4.Types.UInt128", "return returnValueObj;" },
            { "AVM.ClientGenerator.ABI.ARC4.Types.UInt64", "return returnValueObj;" },
            { "AVM.ClientGenerator.ABI.ARC4.Types.UInt32", "return returnValueObj;" },
            { "AVM.ClientGenerator.ABI.ARC4.Types.UInt16", "return returnValueObj;" },
            { "AVM.ClientGenerator.ABI.ARC4.Types.UInt8", "return returnValueObj;" },
            { "Algorand.Address", "return new Algorand.Address(returnValueObj.ToByteArray());" }
        };

        internal static Dictionary<string, string> storageConversions = new Dictionary<string, string>()
        {

            { "System.Numerics.BigInteger","return new System.Numerics.BigInteger(result.Last());" }   ,
            { "byte[]","return result;" },
            { "string","return Encoding.UTF8.GetString(result);" }
        };






        public static string Generate(SemanticModel semanticModel, ClassDeclarationSyntax programClass, string namespaceName, out string className)
        {




            semanticModel = semanticModel ?? throw new ArgumentNullException(nameof(semanticModel));
            programClass = programClass ?? throw new ArgumentNullException(nameof(programClass));

            var classSymbol = semanticModel.GetDeclaredSymbol(programClass);
            if (classSymbol == null)
            {
                className = String.Empty;
                return null;
            }

            if (Utilities.IsSmartContract(classSymbol as INamedTypeSymbol))
            {
                className = $"{programClass.Identifier.Text}Proxy";

                AppDescription appDescription = AppDescription.GenerateContractDescription(semanticModel, programClass);
                var code = appDescription.ToProxy(namespaceName);
                return code;
                //Code code = new Code(indent: 0);
                //code.AddOpeningLine("using System;");
                //code.AddOpeningLine("using Algorand.Algod;");
                //code.AddOpeningLine("using Algorand.Algod.Model;");
                //code.AddOpeningLine("using Algorand.Algod.Model.Transactions;");
                //code.AddOpeningLine("using AVM.ClientGenerator;");
                //code.AddOpeningLine("using Algorand;");
                //code.AddOpeningLine("using AVM.ClientGenerator.Core;");
                //code.AddOpeningLine("using System.Collections.Generic;");
                //code.AddOpeningLine("using System.Linq;");
                //code.AddOpeningLine("using System.Text;");
                //code.AddOpeningLine("using System.Threading.Tasks;");

                //code.AddOpeningLine("");
                //code.AddOpeningLine($"namespace {namespaceName}");
                //code.AddOpeningLine("{");
                //code.AddOpeningLine("");
                //code.AddClosingLine("}");
                //var proxyBody = code.AddChild();
                //proxyBody.AddOpeningLine("");


                ////Add any leading structured trivia
                //if (programClass.HasStructuredTrivia)
                //{
                //    var classTrivia = programClass.GetLeadingTrivia()
                //                                      .Select(i => i.GetStructure())
                //                                      .OfType<DocumentationCommentTriviaSyntax>()
                //                                      .FirstOrDefault();
                //    if (classTrivia != null) proxyBody.AddOpeningLine(classTrivia.ToFullString().Trim());
                //}

                //proxyBody.AddOpeningLine($"public class {className} : ProxyBase");
                //proxyBody.AddOpeningLine("{");
                //proxyBody.AddClosingLine("}");

                //var ctor = proxyBody.AddChild();
                //ctor.AddOpeningLine("");
                //ctor.AddOpeningLine($"public {className}(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId) ");
                //ctor.AddOpeningLine("{");
                //ctor.AddClosingLine("}");



                //defineMethods(semanticModel, programClass, proxyBody);
                //defineFields(semanticModel, programClass, proxyBody);

                //return code.ToString();
            }
            else
            if (Utilities.IsSmartSignature(classSymbol as INamedTypeSymbol))
            {
                className = $"{programClass.Identifier.Text}SmartSignatureGenerator";

                Code code = new Code(indent: 0);
                code.AddOpeningLine("using System;");
                code.AddOpeningLine("using Algorand.Algod;");
                code.AddOpeningLine("using Algorand.Algod.Model;");
                code.AddOpeningLine("using Algorand.Algod.Model.Transactions;");
                code.AddOpeningLine("using AVM.ClientGenerator;");
                code.AddOpeningLine("using Algorand;");
                code.AddOpeningLine("using AVM.ClientGenerator.Core;");
                code.AddOpeningLine("using System.Collections.Generic;");
                code.AddOpeningLine("using System.Linq;");
                code.AddOpeningLine("using System.Text;");
                code.AddOpeningLine("using System.Threading.Tasks;");

                code.AddOpeningLine("");
                code.AddOpeningLine($"namespace {namespaceName}");
                code.AddOpeningLine("{");
                code.AddOpeningLine("");
                code.AddClosingLine("}");
                var proxyBody = code.AddChild();
                proxyBody.AddOpeningLine("");


                //Add any leading structured trivia
                if (programClass.HasStructuredTrivia)
                {
                    var classTrivia = programClass.GetLeadingTrivia()
                                                      .Select(i => i.GetStructure())
                                                      .OfType<DocumentationCommentTriviaSyntax>()
                                                      .FirstOrDefault();
                    if (classTrivia != null) proxyBody.AddOpeningLine(classTrivia.ToFullString().Trim());
                }

                proxyBody.AddOpeningLine($"public class {className} : SignatureBase");
                proxyBody.AddOpeningLine("{");
                proxyBody.AddClosingLine("}");

                var ctor = proxyBody.AddChild();
                ctor.AddOpeningLine("LogicsigSignature smartSig;");
                ctor.AddOpeningLine("");
                ctor.AddOpeningLine($"public {className}(LogicsigSignature logicSig) : base(logicSig) ");
                ctor.AddOpeningLine("{");
                ctor.AddClosingLine("}");



                defineSignatureMethods(semanticModel, programClass, proxyBody);


                return code.ToString();
            }
            else
            {
                className = String.Empty;
                return null;
            }

        }

        //private static void defineFields(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, Code proxyBody)
        //{
        //    var fieldDeclarations = smartContractClass
        //                                      .DescendantNodes()
        //                                      .OfType<VariableDeclarationSyntax>()
        //                                      .SelectMany(s => s.Variables)
        //                                      .Select(s => (syntax: s, symbol: semanticModel.GetDeclaredSymbol(s), attribute: semanticModel.GetDeclaredSymbol(s)?
        //                                                    .GetAttributes()
        //                                                    .Where(a => a.AttributeClass.Name == nameof(StorageAttribute))
        //                                                    .FirstOrDefault())
        //                                             )
        //                                      .Where(s => s.attribute != null);

        //    foreach (var field in fieldDeclarations)
        //    {
        //        var st = field.attribute.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.StorageType)).First();
        //        Core.StorageType storageType = (Core.StorageType)st.Value;

        //        switch (storageType)
        //        {

        //            case Core.StorageType.Global: addStateVar(field.syntax, field.symbol, proxyBody, semanticModel, false); break;
        //            case Core.StorageType.Local: addStateVar(field.syntax, field.symbol, proxyBody, semanticModel, true); break;
        //            default:
        //                throw new Exception("Unsupported field type");

        //        }
        //    }
        //}


        //private static void addStateVar(VariableDeclaratorSyntax syntax, ISymbol symbol, Code code, SemanticModel semanticModel, bool local)
        //{
        //    code = code.AddChild();
        //    if (syntax.HasStructuredTrivia)
        //    {
        //        var trivia = syntax.GetLeadingTrivia()
        //                                    .Select(i => i.GetStructure())
        //                                    .OfType<DocumentationCommentTriviaSyntax>()
        //                                    .FirstOrDefault();
        //        if (trivia!=null)
        //            code.AddOpeningLine(trivia.ToFullString().Trim());
        //    }

        //    string returnType = (syntax.Parent as VariableDeclarationSyntax).Type.ToString();
        //    string fieldName = syntax.Identifier.ValueText;
        //    string retline = $"return ({returnType})result;";
        //    var machineValueType = TealTypeUtils.DetermineType(semanticModel, (syntax.Parent as VariableDeclarationSyntax).Type);
        //    string typeCall;
        //    switch (machineValueType)
        //    {
        //        case Core.VariableType.ByteSlice: 
        //            typeCall = local? "GetLocalByteSlice(caller,key)" : "GetGlobalByteSlice(key)";
        //            if (storageConversions.TryGetValue(returnType, out retline))
        //            { }
        //            else
        //            {
        //                retline = "return; // <unknown return conversion>";
        //            }

        //            break;
        //        case Core.VariableType.UInt64: typeCall = local? "GetLocalUInt(caller,key)": "GetGlobalUInt(key)"; break;
        //        default:
        //            throw new Exception("Unsupported field type.");
        //    }


        //    if (local)
        //    {
        //        code.AddOpeningLine($"public async Task<{returnType}> {fieldName}(Account caller)");
        //    }
        //    else
        //    {
        //        code.AddOpeningLine($"public async Task<{returnType}> {fieldName}()");
        //    }
        //    code.AddOpeningLine("{");
        //    var body = code.AddChild();
        //    body.AddOpeningLine($"var key=\"{fieldName}\";");
        //    body.AddOpeningLine($"var result= await base.{typeCall};");
        //    body.AddOpeningLine(retline);
        //    code.AddClosingLine("}");
        //}

        //private static void defineMethods(SemanticModel semanticModel, ClassDeclarationSyntax smartContractClass, Code proxyBody)
        //{

        //    var methodSyntaxes = smartContractClass
        //                            .DescendantNodes()
        //                            .OfType<MethodDeclarationSyntax>();

        //    var methodSymbols = methodSyntaxes
        //                            .Select(ms => (ms, msym: semanticModel.GetDeclaredSymbol(ms)));

        //    foreach (var methodSymbol in methodSymbols)
        //    {
        //        var ABImethod = methodSymbol
        //                        .msym
        //                        .GetAttributes()
        //                        .Where(a => a.AttributeClass.Name == nameof(SmartContractMethodAttribute))
        //                        .FirstOrDefault();

        //        if (ABImethod != null)
        //        {
        //            var callTypeConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.OnCompleteType)).First();
        //            var callType = (Core.OnCompleteType)callTypeConst.Value;
        //            var selectorConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();
        //            var selector = (string)selectorConst.Value;

        //            if (selector == null)
        //            {
        //                // the selector override is not specified so we must use the same selector method as 
        //                // the compiler does...
        //                selector = methodSymbol.msym.GetMethodSelector(semanticModel);
        //            }

        //            var returnType = methodSymbol.msym.ReturnType;
        //            var methodName = methodSymbol.msym.Name;
        //            List<IParameterSymbol> transactionParameters = new List<IParameterSymbol>();
        //            List<IParameterSymbol> appRefParameters = new List<IParameterSymbol>();
        //            List<IParameterSymbol> acctRefParameters = new List<IParameterSymbol>();
        //            List<IParameterSymbol> assetRefParameters = new List<IParameterSymbol>();
        //            List<IParameterSymbol> argParameters = new List<IParameterSymbol>();

        //            var refToCurrentAppCall = methodSymbol.msym.Parameters.Where(p => TransactionRefVariable.IsTxRef(p.Type)).LastOrDefault();


        //            foreach (var parm in methodSymbol.msym.Parameters)
        //            {
        //                var parmType = parm.Type;
        //                if (TransactionRefVariable.IsTxRef(parmType))
        //                {
        //                    if (!refToCurrentAppCall.Equals(parm,SymbolEqualityComparer.Default))
        //                    {
        //                        transactionParameters.Add(parm);

        //                    }
        //                    continue;
        //                }

        //                if (ApplicationRefVariable.IsApplicationRef(parmType))
        //                {

        //                    appRefParameters.Add(parm);

        //                    continue;
        //                }

        //                if (AccountRefVariable.IsAccountRef(parmType))
        //                {
        //                    acctRefParameters.Add(parm);
        //                    continue;
        //                }

        //                if (AssetRefVariable.IsAssetRef(parmType))
        //                {
        //                    assetRefParameters.Add(parm);
        //                    continue;
        //                }

        //                argParameters.Add(parm);

        //            }

        //            var transactionParameterDefinitions = transactionParameters.Select(p => defineTransactionParameter(p));
        //            var appRefParameterDefinitions = appRefParameters.Select(p => defineAppRefParameter(p));
        //            var accountRefParameterDefinitions = acctRefParameters.Select(p => defineAcctRefParameter(p));
        //            var assetRefParameterDefinitions = assetRefParameters.Select(p => defineAssetRefParameter(p));
        //            var argParameterDefinitions = argParameters.Select(p => defineArgParameter(p));
        //            var allParameters = transactionParameterDefinitions.Concat(appRefParameterDefinitions).Concat(accountRefParameterDefinitions).Concat(assetRefParameterDefinitions).Concat(argParameterDefinitions);

        //            string parameters = String.Join(",", allParameters);
        //            string txNameList;
        //            if (transactionParameters.Count > 0) txNameList = "new List<Transaction> {" + string.Join(",", transactionParameters.Select(p => p.Name)) + "}";
        //            else
        //                txNameList = "null";

        //            string argsList = "new List<object> {" + string.Join(",", new List<string> { "abiHandle" }.Concat(argParameters.Select(p => p.Name))) + "}";


        //            string appsList;
        //            if (appRefParameters.Count > 0) appsList = "new List<ulong> {" + string.Join(",", appRefParameters.Select(p => p.Name)) + "}";
        //            else
        //                appsList = "null";

        //            string assetsList;
        //            if (assetRefParameters.Count > 0) assetsList = "new List<ulong> {" + string.Join(",", assetRefParameters.Select(p => p.Name)) + "}";
        //            else
        //                assetsList = "null";

        //            string accountsList;
        //            if (acctRefParameters.Count > 0) accountsList = "new List<Address> {" + string.Join(",", acctRefParameters.Select(p => p.Name)) + "}";
        //            else
        //                accountsList = "null";

        //            string methodReturnType;
        //            if (returnType.ToString() != "void")
        //            {
        //                methodReturnType = $"Task<{returnType}>";
        //            }
        //            else
        //            {
        //                methodReturnType = "Task";
        //            }

        //            var abiMethod = proxyBody.AddChild();
        //            var abiMethodForTransactions = proxyBody.AddChild();
        //            //Add any leading structured trivia
        //            if (methodSymbol.ms.HasStructuredTrivia)
        //            {
        //                var trivia = methodSymbol.ms.GetLeadingTrivia()
        //                                            .Select(i => i.GetStructure())
        //                                            .OfType<DocumentationCommentTriviaSyntax>()
        //                                            .FirstOrDefault();
        //                if (trivia!=null)
        //                    abiMethod.AddOpeningLine(trivia.ToFullString().Trim());
        //            }

        //            abiMethod.AddOpeningLine($"public async {methodReturnType} {methodName} (Account sender, ulong? fee, {parameters},string note, List<BoxRef> boxes)".Replace(",,", ",").Replace(", ,",","));
        //            abiMethod.AddOpeningLine("{");
        //            abiMethod.AddClosingLine("}");

        //            abiMethodForTransactions.AddOpeningLine($"public async Task<List<Transaction>> {methodName}_Transactions (Account sender, ulong? fee, {parameters},string note, List<BoxRef> boxes)".Replace(",,", ",").Replace(", ,", ","));
        //            abiMethodForTransactions.AddOpeningLine("{");
        //            abiMethodForTransactions.AddClosingLine("}");

        //            var abiMethodBody = abiMethod.AddChild();
        //            abiMethodBody.AddOpeningLine($"var abiHandle = Encoding.UTF8.GetBytes(\"{selector}\");");
        //            abiMethodBody.AddOpeningLine($"var result = await base.CallApp({txNameList}, fee, AVM.ClientGenerator.Core.OnCompleteType.{callType}, 1000, note, sender,  {argsList}, {appsList}, {assetsList},{accountsList},boxes);");
        //            if (returnType.ToString() != "void")
        //            {
        //                if (returnTypeConversions.TryGetValue(returnType.ToString(), out string retline))
        //                {
        //                    abiMethodBody.AddOpeningLine(retline);
        //                }
        //                else
        //                {
        //                    abiMethodBody.AddOpeningLine("return; // <unknown return conversion>");
        //                }
        //            }

        //            var abiMethodBodyForTransactions = abiMethodForTransactions.AddChild();
        //            abiMethodBodyForTransactions.AddOpeningLine($"var abiHandle = Encoding.UTF8.GetBytes(\"{selector}\");");
        //            abiMethodBodyForTransactions.AddOpeningLine($"return await base.MakeTransactionList({txNameList}, fee, AVM.ClientGenerator.Core.OnCompleteType.{callType}, 1000, note, sender,  {argsList}, {appsList}, {assetsList},{accountsList},boxes);");


        //        }


        //    }
        //}

        private static void defineSignatureMethods(SemanticModel semanticModel, ClassDeclarationSyntax signatureClass, Code proxyBody)
        {

            var methodSyntaxes = signatureClass
                                    .DescendantNodes()
                                    .OfType<MethodDeclarationSyntax>();

            var methodSymbols = methodSyntaxes
                                    .Select(ms => (ms, msym: semanticModel.GetDeclaredSymbol(ms)));

            foreach (var methodSymbol in methodSymbols)
            {
                var ABImethod = methodSymbol
                                .msym
                                .GetAttributes()
                                .Where(a => a.AttributeClass.Name == nameof(SmartSignatureMethodAttribute))
                                .FirstOrDefault();

                if (ABImethod != null)
                {

                    var selectorConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();
                    var selector = (string)selectorConst.Value;

                    if (selector == null)
                    {
                        // the selector override is not specified so we must use the same selector method as 
                        // the compiler does...
                        selector = methodSymbol.msym.GetMethodSelector(semanticModel);
                    }

                    var returnType = methodSymbol.msym.ReturnType;
                    var methodName = methodSymbol.msym.Name;

                    List<string> parms = new List<string>();
                    List<string> names = new List<string>();
                    foreach (var parm in methodSymbol.msym.Parameters)
                    {
                        var parmType = parm.Type;
                        if (TransactionRefVariable.IsTxRef(parmType))
                        {
                            //ignore these 
                            continue;
                        }

                        if (ApplicationRefVariable.IsApplicationRef(parmType))
                        {
                            names.Add(parm.Name);
                            parms.Add(defineAppRefParameter(parm));
                            continue;
                        }

                        if (AccountRefVariable.IsAccountRef(parmType))
                        {
                            names.Add(parm.Name);
                            parms.Add(defineAcctRefParameter(parm));
                            continue;
                        }

                        if (AssetRefVariable.IsAssetRef(parmType))
                        {
                            names.Add(parm.Name);
                            parms.Add(defineAssetRefParameter(parm));
                            continue;
                        }
                        names.Add(parm.Name);
                        parms.Add(defineArgParameter(parm));

                    }
                    string parameters = String.Join(",", parms);

                    string argsList = "new List<object> {" + string.Join(",", new List<string> { "abiHandle" }.Concat(names)) + "}";


                    var abiMethod = proxyBody.AddChild();


                    //Add any leading structured trivia
                    if (methodSymbol.ms.HasStructuredTrivia)
                    {
                        var trivia = methodSymbol.ms.GetLeadingTrivia()
                                                    .Select(i => i.GetStructure())
                                                    .OfType<DocumentationCommentTriviaSyntax>()
                                                    .FirstOrDefault();
                        if (trivia != null)
                            abiMethod.AddOpeningLine(trivia.ToFullString().Trim());
                    }

                    abiMethod.AddOpeningLine($"public void {methodName} ({parameters})".Replace(",,", ","));
                    abiMethod.AddOpeningLine("{");
                    abiMethod.AddClosingLine("}");


                    var abiMethodBody = abiMethod.AddChild();
                    abiMethodBody.AddOpeningLine($"var abiHandle = Encoding.UTF8.GetBytes(\"{selector}\");");
                    abiMethodBody.AddOpeningLine($"base.UpdateSmartSignature( {argsList} );");



                }


            }
        }



        private static string defineArgParameter(IParameterSymbol p)
        {
            return $"{p.Type} {p.Name}";
        }

        private static string defineAssetRefParameter(IParameterSymbol p)
        {
            return $"ulong {p.Name}";
        }

        private static string defineAcctRefParameter(IParameterSymbol p)
        {
            return $"Address {p.Name}";
        }

        //private static string defineTransactionParameter(IParameterSymbol p)
        //{
        //    string parmType = p.Type.ToString();
        //    string outputParmType = TypeHelpers.determineTransactionType(parmType);

        //    return $"{outputParmType} {p.Name}";
        //}


        private static string defineAppRefParameter(IParameterSymbol p)
        {
            return $"ulong {p.Name}";
        }
    }
}
