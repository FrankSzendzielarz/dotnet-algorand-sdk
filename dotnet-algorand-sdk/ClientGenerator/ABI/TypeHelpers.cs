using AVM.ClientGenerator.Compiler;
using AVM.ClientGenerator.Compiler.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AVM.ClientGenerator.ABI
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TypeHelpers
    {

        private static string checkArrayType(string arrayComponent, string csType)
        {
            if (arrayComponent.Trim().StartsWith("[")) arrayComponent = "[]";
            return csType + arrayComponent;
        }
        private static string escapeStringLiteral(string value)
        {
            return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        // elementAbiSpec is the ABI type string of a single array element (e.g. "uint80", "ufixed64x2",
        // "byte[16]") - it's plumbed into the generated FixedArray/VariableArray constructor call so that at
        // runtime the array can build each element via WireType.FromABIDescription rather than a parameterless
        // `new T()`, which the parametric wire types (UInt, UFixed) can't support since their encoding depends on
        // values (bitwidth, precision) that only the ABI spec string carries.
        private static string checkAbiArrayType(string arrayComponent, string csType, string elementAbiSpec = null)
        {
            var arC = arrayComponent.Trim();
            string specArg = string.IsNullOrEmpty(elementAbiSpec) ? "" : $"\"{escapeStringLiteral(elementAbiSpec)}\"";
            if (arC.Equals("[]")) return $"AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<{csType}>({specArg})";
            if (arC.StartsWith("[")) return $"AVM.ClientGenerator.ABI.ARC4.Types.FixedArray<{csType}>({arC.TrimStart('[').TrimEnd(']')}{(specArg.Length > 0 ? ", " + specArg : "")})";
            return $"{csType}()";
        }

        private static string removeArrayComponent(string methodABIType)
        {
            if (methodABIType.EndsWith("]"))
            {
                if (methodABIType.Length >= 2)
                {
                    return methodABIType.Substring(0, methodABIType.LastIndexOf('['));
                }
                else
                {
                    throw new Exception("Invalid array component");
                }
            }
            return methodABIType;
        }

        internal class TupleSubTypes : IEnumerable<string>
        {
            private string methodABITypeString;

            internal TupleSubTypes(string methodABITypeString)
            {
                this.methodABITypeString = methodABITypeString;
            }

            private int scanForMatchingCloseBracket(int scanPos)
            {
                int c = 0;
                do
                {
                    if (methodABITypeString[scanPos] == '(') c++;
                    if (methodABITypeString[scanPos] == ')') c--;
                    scanPos++;
                }
                while (c > 0 && c < methodABITypeString.Length);

                if (scanPos >= methodABITypeString.Length && c > 0) throw new Exception("Mismatched opening and closing bracket in tuple definition.");

                return scanPos;
            }

            public IEnumerator<string> GetEnumerator()
            {
                int i = 0;

                while (i < methodABITypeString.Length)
                {
                    int typeLength;

                    if (methodABITypeString[i] == '(')
                    {
                        typeLength = scanForMatchingCloseBracket(i) - i;

                        // A tuple element can itself be an array of tuples (e.g. "(byte[4],uint64)[]" or
                        // "(byte[4],uint64)[3]") - scanForMatchingCloseBracket only finds the matching ')', so an
                        // array suffix immediately following it has to be consumed here too. Without this, the
                        // "[]"/"[N]" is left dangling and gets misread as the start of the next element on the
                        // following GetEnumerator iteration.
                        int suffixPos = i + typeLength;
                        while (suffixPos < methodABITypeString.Length && methodABITypeString[suffixPos] == '[')
                        {
                            int closeBracket = methodABITypeString.IndexOf(']', suffixPos);
                            if (closeBracket == -1)
                            {
                                throw new Exception("Mismatched opening and closing bracket in tuple definition.");
                            }
                            suffixPos = closeBracket + 1;
                        }
                        typeLength = suffixPos - i;

                    }
                    else
                    {
                        int nextComma = methodABITypeString.IndexOf(',', i);
                        if (nextComma == -1)
                        {
                            typeLength = methodABITypeString.Length - i;
                        }
                        else
                        {
                            typeLength = nextComma - i;
                        }
                        if (typeLength <= 0)
                        {
                            throw new Exception("Invalid tuple format - empty type.");
                        }
                    }


                    yield return methodABITypeString.Substring(i, typeLength);

                    i = i + typeLength + 1;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }


        internal static void TypeToStructs(string structName, string methodABITypeString, List<string> structs)
        {
            if (methodABITypeString[0] != '(' || methodABITypeString[methodABITypeString.Length - 1] != ')')
                throw new Exception($"Invalid struct type declaration {methodABITypeString}.");

            if (methodABITypeString.Length <= 2)
                throw new Exception($"Empty tuples are invalid.");

            string methodABIType = methodABITypeString.Substring(1, methodABITypeString.Length - 2);

            TupleSubTypes subTypes = new TupleSubTypes(methodABIType);


            StringBuilder structBody = new StringBuilder();
            structBody.AppendLine($"\t\tpublic struct {structName}");
            structBody.AppendLine($"\t\t{{");
            int c = 0;
            foreach (var t in subTypes)
            {
                var p = ABITypeToCSType(structName + $"{c}", t, structs, false);
                string prop = $"\t\t\t{p.Item1}\n\t\t\tpublic {p.Item2} field{c} {{get;set;}}";
                structBody.AppendLine(prop);
                c++;
            }
            structBody.AppendLine("\t\t}");

            structs.Add(structBody.ToString());
        }





        internal static (string bitwidthDecorator, string dotnetArgInputType, string dotnetABIType, bool useNew) ABITypeToCSType(string parentStructName, string methodABITypeString, List<string> structs, bool isReturn)
        {
            try
            {
                methodABITypeString = methodABITypeString.ToLower().Trim();

                string methodABIType = removeArrayComponent(methodABITypeString);

                string arrayComponent = methodABITypeString.Replace(methodABIType, "");

                // methodABIType still ends in "]" for a multi-dimensional ABI array (e.g. "byte[][]", the ARC4
                // dynamic-array-of-dynamic-byte-arrays type), since removeArrayComponent only strips one level.
                // Recurse on the element type (one array level down) and wrap it in another array level, rather
                // than falling through to the "unknown type -> nested struct" default below, which would emit a
                // reference to a Structs.* type that was never actually defined.
                if (methodABIType.EndsWith("]"))
                {
                    var inner = ABITypeToCSType(parentStructName, methodABIType, structs, isReturn);
                    string csType = checkArrayType(arrayComponent, inner.dotnetArgInputType);
                    int callParenIndex = inner.dotnetABIType.IndexOf('(');
                    string innerWireTypeName = callParenIndex >= 0 ? inner.dotnetABIType.Substring(0, callParenIndex) : inner.dotnetABIType;
                    string abiType = checkAbiArrayType(arrayComponent, innerWireTypeName, methodABIType);
                    return (inner.bitwidthDecorator, csType, abiType, false);
                }

                if (methodABIType.StartsWith("uint"))
                {
                    int bitwidth = Int32.Parse(methodABIType.Remove(0, 4));

                    if (bitwidth < 8 || bitwidth > 512)
                        throw new Exception($"Unsupported bitwidth {bitwidth} ");
                    string bitwidthDecorator;
                    if (isReturn)
                    {
                        bitwidthDecorator = $"[return:AbiBitWidth({bitwidth})] ";
                    }
                    else
                    {
                        bitwidthDecorator = $"[AbiBitWidth({bitwidth})] ";
                    }

                    if (bitwidth == 8) return ("", checkArrayType(arrayComponent, "byte"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.Byte", "byte"), false);
                    if (bitwidth == 16) return ("", checkArrayType(arrayComponent, "ushort"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt16", "uint16"), false);
                    if (bitwidth == 24) return (bitwidthDecorator, checkArrayType(arrayComponent, "uint"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt24", "uint24"), false);
                    if (bitwidth == 32) return ("", checkArrayType(arrayComponent, "uint"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt32", "uint32"), false);
                    if (bitwidth == 48) return (bitwidthDecorator, checkArrayType(arrayComponent, "uint"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt48", "uint48"), false);
                    if (bitwidth == 64) return ("", checkArrayType(arrayComponent, "ulong"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt64", "uint64"), false);
                    if (bitwidth == 128) return ("", checkArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt128"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt128", "uint128"), false);
                    if (bitwidth == 256) return ("", checkArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt256"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt256", "uint256"), false);
                    if (bitwidth == 512) return ("", checkArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt512"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt512", "uint512"), false);
                    if (bitwidth > 64 && bitwidth <= 512) return (bitwidthDecorator, checkArrayType(arrayComponent, $"System.Numerics.BigInteger"), checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UInt", $"uint{bitwidth}"), false);

                    throw new Exception($"Unsupported bitwidth{bitwidth}.");
                }

                if (methodABIType.StartsWith("int"))
                {
                    //int bitwidth = Int32.Parse(methodABIType.Remove(0, 3));

                    //if (bitwidth < 8 || bitwidth > 512)
                    //    throw new Exception($"Unsupported bitwidth {bitwidth} ");
                    //string bitwidthDecorator;
                    //if (isReturn)
                    //{
                    //    bitwidthDecorator = $"[return:AbiBitWidth({bitwidth})] ";
                    //}
                    //else
                    //{
                    //    bitwidthDecorator = $"[AbiBitWidth({bitwidth})] ";
                    //}

                    //if (bitwidth == 8) return ("", checkArrayType(arrayComponent, "sbyte"), false);
                    //if (bitwidth == 16) return ("", checkArrayType(arrayComponent, "short"), false);
                    //if (bitwidth == 24) return (bitwidthDecorator, checkArrayType(arrayComponent, "int"), false);
                    //if (bitwidth == 32) return ("", checkArrayType(arrayComponent, "int"), false);
                    //if (bitwidth == 48) return (bitwidthDecorator, checkArrayType(arrayComponent, "int"), false);
                    //if (bitwidth == 64) return ("", checkArrayType(arrayComponent, "ulong"), false);
                    //if (bitwidth > 64 && bitwidth <= 512) return (bitwidthDecorator, checkArrayType(arrayComponent, $"System.Numerics.BigInteger"), false);

                    // throw new Exception($"Unsupported bitwidth{bitwidth}.");
                    throw new Exception($"Integers are currently not supported. Please use unsigned integers.");
                }

                if (methodABIType == "datetime")
                {
                    return ("", checkArrayType(arrayComponent, "ulong"), checkAbiArrayType(arrayComponent, $"AVM.ClientGenerator.ABI.ARC4.Types.UInt64", "uint64"), false);
                }

                if (methodABIType == "byte" || methodABIType == "sbyte")
                {
                    return ("", checkArrayType(arrayComponent, methodABIType), checkAbiArrayType(arrayComponent, $"AVM.ClientGenerator.ABI.ARC4.Types.Byte", "byte"), false);

                }
                if (methodABIType == "bigint" || methodABIType == "ubigint")
                {
                    return ("", checkArrayType(arrayComponent, "System.Numerics.BigInteger"), checkAbiArrayType(arrayComponent, $"AVM.ClientGenerator.ABI.ARC4.Types.UInt"), false);
                }

                if (methodABIType == "bool")
                {
                    return ("", checkArrayType(arrayComponent, methodABIType), checkAbiArrayType(arrayComponent, $"AVM.ClientGenerator.ABI.ARC4.Types.Bool", "bool"), false);

                }

                if (methodABIType.StartsWith("ufixed"))
                {
                    // ufixed ABI types are described as ufixed<N>x<M> where N is the bitwidth and M is the precision.
                    string[] ufixedParts = methodABIType.Substring("ufixed".Length).Split('x');
                    if (ufixedParts.Length != 2 || !uint.TryParse(ufixedParts[0], out uint ufixedBitwidth) || !uint.TryParse(ufixedParts[1], out uint ufixedPrecision))
                        throw new Exception($"Invalid ufixed ABI type {methodABIType}");

                    string ufixedAbiType = string.IsNullOrEmpty(arrayComponent.Trim())
                        ? $"AVM.ClientGenerator.ABI.ARC4.Types.UFixed({ufixedBitwidth}, {ufixedPrecision})"
                        : checkAbiArrayType(arrayComponent, "AVM.ClientGenerator.ABI.ARC4.Types.UFixed", methodABIType);

                    return ("", checkArrayType(arrayComponent, "System.Numerics.BigInteger"), ufixedAbiType, false);

                }
                if (methodABIType.StartsWith("decimal"))
                {
                    return ("", checkArrayType(arrayComponent, "System.Decimal"), checkAbiArrayType(arrayComponent, $"AVM.ClientGenerator.ABI.ARC4.Types.UFixed"), false);
                }

                if (methodABIType == "address" || methodABIType == "account")
                {
                    return ("", checkArrayType(arrayComponent, "Algorand.Address"), checkAbiArrayType(arrayComponent, $"AVM.ClientGenerator.ABI.ARC4.Types.Address", "address"), false);

                }

                if (methodABIType == "asset")
                {
                    return ("", checkArrayType(arrayComponent, "ulong"), checkAbiArrayType(arrayComponent, $"AVM.ClientGenerator.ABI.ARC4.Types.Asset", "asset"), false);

                }
                if (methodABIType == "application")
                {
                    return ("", checkArrayType(arrayComponent, "ulong"), checkAbiArrayType(arrayComponent, $"AVM.ClientGenerator.ABI.ARC4.Types.Application", "application"), false);

                }
                if (methodABIType == "string")
                {
                    return ("", checkArrayType(arrayComponent, "string"), checkAbiArrayType(arrayComponent, $"AVM.ClientGenerator.ABI.ARC4.Types.String", "string"), false);
                }
                if (methodABIType == "void")
                {
                    return ("", "void", "", false);
                }

                if (methodABIType.StartsWith("("))
                {
                    TypeToStructs(ARC4.MethodDescription.FormatStructName(parentStructName), methodABIType, structs);
                    return ("", checkArrayType(arrayComponent, parentStructName), "", false);
                }

                if (methodABIType.StartsWith("ref:"))
                {
                    return ("", "object", "", false);
                }

                // when we get here, it means we do the inner recursion to other struct. parentStructName carries
                // the original (unlowered) type text - e.g. "SomeStruct" or "SomeStruct[]" for a bare/array
                // reference to an already-registered struct - since methodABIType above has been lowercased and
                // is only used for the type-name comparisons, not for reconstructing the reference itself.
                if (arrayComponent.Length > 0)
                {
                    string parentBare = parentStructName.Trim();
                    if (parentBare.EndsWith("]"))
                    {
                        int idx = parentBare.LastIndexOf('[');
                        if (idx >= 0) parentBare = parentBare.Substring(0, idx);
                    }
                    // useNew=false: an array-typed property can't be default-initialized with `new T[]()` (that's
                    // invalid C# - needs a size or initializer), unlike the scalar struct case below.
                    return ("", checkArrayType(arrayComponent, "Structs." + ARC4.MethodDescription.FormatStructName(parentBare)), "", false);
                }

                return ("", "Structs." + ARC4.MethodDescription.FormatStructName(parentStructName), "", true);
                //throw new Exception($"Unknown type  {methodABIType}");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                throw new Exception($"The ABI type is not valid {methodABITypeString} : {(ex.InnerException != null ? ex.InnerException.Message : ex.Message)}", ex);
            }
        }



        internal static Dictionary<string, string> predefinedTypeConversions = new Dictionary<string, string>
        {
            { "void", ABI.ABIType["void"] },
            { "bool", ABI.ABIType["bool"] },
            { typeof(bool).Name, ABI.ABIType["bool"] },
            { "byte", ABI.ABIType["byte"] },
            { "System.Byte", ABI.ABIType["byte"]},
            { "Byte", ABI.ABIType["byte"]},
            { "sbyte", ABI.ABIType["byte"] },
            { typeof(SByte).Name, ABI.ABIType["byte"]  },
            { "char", ABI.ABIType["byte"]},
            { typeof(Char).Name, ABI.ABIType["byte"]},
            {"int", ABI.ABIType["uint32"]},
            { typeof(Int32).Name, ABI.ABIType["uint32"] },
            { "uint", ABI.ABIType["uint32"]},
            { typeof(UInt32).Name, ABI.ABIType["uint32"]},
            { "long", ABI.ABIType["uint64"] },
            { typeof(Int64).Name, ABI.ABIType["uint64"]},
            { "ulong", ABI.ABIType["uint64"]},
            { typeof(UInt64).Name, ABI.ABIType["uint64"]},
            { "short", ABI.ABIType["uint16"]},
            { typeof(Int16).Name, ABI.ABIType["uint16"] },
            { "ushort", ABI.ABIType["uint16"]},
            { typeof(UInt16).Name , ABI.ABIType["uint16"]},
            { "System.Numerics.BigInteger", ABI.ABIType["bigint"] },
            { "decimal", ABI.ABIType["decimal"] },
            { typeof(Decimal).Name, ABI.ABIType["decimal"]},
            { "byte[]", ABI.ABIType["string"]},
            { typeof(byte[]).Name, ABI.ABIType["string"]},
            { "string", ABI.ABIType["string"]},
            { "String", ABI.ABIType["string"]},
        };


        internal static (string bitwidthAttrib, string type, string abiType, bool useNew) GetCSType(string parentStructName, string abiType, string sourceType, List<string> structs, bool isReturn)
        {
            if (String.IsNullOrWhiteSpace(sourceType))
            {
                return ABITypeToCSType(parentStructName, abiType, structs, isReturn);
            }

            string structTypeName = $"Structs.{ARC4.MethodDescription.FormatStructName(sourceType)}";

            string arrayComponent = "";
            if (!string.IsNullOrEmpty(abiType))
            {
                string trimmed = abiType.Trim();
                if (trimmed.EndsWith("]"))
                {
                    int idx = trimmed.LastIndexOf('[');
                    if (idx >= 0) arrayComponent = trimmed.Substring(idx);
                }
            }

            if (string.IsNullOrEmpty(arrayComponent))
            {
                return ("", structTypeName, "", false);
            }

            // Named ARC56 structs aren't WireType instances, so arrays of them are wrapped in StructArray<T>
            // (see AVM.ClientGenerator.ABI.ARC4.Types.StructArray) to plug into the existing encode/decode pipeline.
            bool isFixedLength = arrayComponent.Length > 2;
            string fixedLength = isFixedLength ? arrayComponent.TrimStart('[').TrimEnd(']') : "0";
            string wrapperExpr = $"AVM.ClientGenerator.ABI.ARC4.Types.StructArray<{structTypeName}>(x => {structTypeName}.Parse(x)) {{ IsFixedLength = {(isFixedLength ? "true" : "false")}, FixedLength = {fixedLength} }}";
            return ("", $"{structTypeName}[]", wrapperExpr, false);
        }

        internal static string CSTypeToAbiType(TypeSyntax type, SemanticModel semanticModel)
        {
            string abiType = ABI.ABIType["unsupported"];

            var ts = semanticModel.GetTypeInfo(type).Type;
            if (ts != null)
            {
                return CSTypeToAbiType(ts);
            }

            return abiType;
        }

        internal static string TransactionReferenceToInnerTransaction(string parmType)
        {
            string innerTransaction;
            switch (parmType)
            {
                case "txn":
                    innerTransaction = "InnerTransaction";
                    break;
                case "pay":
                    innerTransaction = "Payment";
                    break;
                case "keyreg":
                    innerTransaction = "KeyRegistration";
                    break;
                case "axfer":
                    innerTransaction = "AssetTransfer";
                    break;
                case "acfg":
                    innerTransaction = "AssetConfiguration";
                    break;
                case "afrz":
                    innerTransaction = "AssetFreeze";
                    break;
                case "appl":
                    innerTransaction = "AppCall";
                    break;

                case "AVM.ClientGenerator.Core.TransactionReference":
                    innerTransaction = "InnerTransaction";
                    break;
                case "AVM.ClientGenerator.Core.PaymentTransactionReference":
                    innerTransaction = "Payment";
                    break;
                case "AVM.ClientGenerator.Core.KeyRegistrationTransactionReference":
                    innerTransaction = "KeyRegistration";
                    break;
                case "AVM.ClientGenerator.Core.AssetAcceptTransactionReference":
                    innerTransaction = "AssetAccept";
                    break;
                case "AVM.ClientGenerator.Core.AssetClawbackTransactionReference":
                    innerTransaction = "AssetClawback";
                    break;
                case "AVM.ClientGenerator.Core.AssetConfigurationTransactionReference":
                    innerTransaction = "AssetConfiguration";
                    break;
                case "AVM.ClientGenerator.Core.AssetFreezeTransactionReference":
                    innerTransaction = "AssetFreeze";
                    break;
                case "AVM.ClientGenerator.Core.AssetTransferTransactionReference":
                    innerTransaction = "AssetTransfer";
                    break;
                case "AVM.ClientGenerator.Core.AppCallTransactionReference":
                    innerTransaction = "AppCall";
                    break;
                default:
                    innerTransaction = "";
                    break;

            }

            return innerTransaction;
        }
        internal static string determineTransactionType(string parmType)
        {
            string outputParmType;
            switch (parmType)
            {
                case "txn":
                    outputParmType = "Transaction";
                    break;
                case "pay":
                    outputParmType = "PaymentTransaction";
                    break;
                case "keyreg":
                    outputParmType = "KeyRegistrationTransaction";
                    break;
                case "axfer":
                    outputParmType = "AssetTransferTransaction";
                    break;
                case "acfg":
                    outputParmType = "AssetConfigurationTransaction";
                    break;
                case "afrz":
                    outputParmType = "AssetFreezeTransaction";
                    break;
                case "appl":
                    outputParmType = "ApplicationCallTransaction";
                    break;
                case "AVM.ClientGenerator.Core.TransactionReference":
                    outputParmType = "Transaction";
                    break;
                case "AVM.ClientGenerator.Core.PaymentTransactionReference":
                    outputParmType = "PaymentTransaction";
                    break;
                case "AVM.ClientGenerator.Core.KeyRegistrationTransactionReference":
                    outputParmType = "KeyRegistrationTransaction";
                    break;
                case "AVM.ClientGenerator.Core.AssetAcceptTransactionReference":
                    outputParmType = "AssetAcceptTransaction";
                    break;
                case "AVM.ClientGenerator.Core.AssetClawbackTransactionReference":
                    outputParmType = "AssetClawbackTransaction";
                    break;
                case "AVM.ClientGenerator.Core.AssetConfigurationTransactionReference":
                    outputParmType = "AssetConfigurationTransaction";
                    break;
                case "AVM.ClientGenerator.Core.AssetFreezeTransactionReference":
                    outputParmType = "AssetFreezeTransaction";
                    break;
                case "AVM.ClientGenerator.Core.AssetTransferTransactionReference":
                    outputParmType = "AssetTransferTransaction";
                    break;
                case "AVM.ClientGenerator.Core.AppCallTransactionReference":
                    outputParmType = "ApplicationCallTransaction";
                    break;
                default:
                    outputParmType = "Transaction";
                    break;

            }

            return outputParmType;
        }

        internal static string CSTypeToAbiType(ITypeSymbol ts)
        {
            string abiType = ABI.ABIType["unsupported"];
            var arrayType = ts as IArrayTypeSymbol;
            if (arrayType != null)
            {
                if (predefinedTypeConversions.TryGetValue(arrayType.ElementType.ToString(), out string at))
                {
                    return $"{at}[]";
                }

                if (Utilities.IsAbiStruct(ts))
                {
                    return $"byte[][]";
                }

            }
            else
            {
                if (predefinedTypeConversions.TryGetValue(ts.ToString(), out string at))
                {
                    return $"{at}";
                }

                if (Utilities.IsAbiStruct(ts))
                {
                    return $"byte[]";
                }
            }





            //check if it's a transaction array reference type
            if (ApplicationRefVariable.IsApplicationRef(ts)) return ABI.ABIType["application"];
            if (AssetRefVariable.IsAssetRef(ts)) return ABI.ABIType["asset"];
            if (AccountRefVariable.IsAccountRef(ts)) return ABI.ABIType["account"];

            //check if it's a transaction type
            switch (ts.ToString())
            {
                case "AVM.ClientGenerator.Core.TransactionReference":
                    return ABI.ABIType["txn"];
                case "AVM.ClientGenerator.Core.PaymentTransactionReference":
                    return ABI.ABIType["pay"];
                case "AVM.ClientGenerator.Core.KeyRegistrationTransactionReference":
                    return ABI.ABIType["keyreg"];
                case "AVM.ClientGenerator.Core.AssetAcceptTransactionReference":
                    return ABI.ABIType["axfer"];
                case "AVM.ClientGenerator.Core.AssetClawbackTransactionReference":
                    return ABI.ABIType["axfer"];
                case "AVM.ClientGenerator.Core.AssetConfigurationTransactionReference":
                    return ABI.ABIType["acfg"];
                case "AVM.ClientGenerator.Core.AssetFreezeTransactionReference":
                    return ABI.ABIType["afrz"];
                case "AVM.ClientGenerator.Core.AssetTransferTransactionReference":
                    return ABI.ABIType["axfer"];
                case "AVM.ClientGenerator.Core.AppCallTransactionReference":
                    return ABI.ABIType["appl"];
            }
            return abiType.ToString();
        }

        internal static string CSTypeToAbiType(Type ts)
        {

            string abiType = ABI.ABIType["unsupported"];
            var arrayType = ts as IArrayTypeSymbol;
            if (arrayType != null)
            {
                if (predefinedTypeConversions.TryGetValue(arrayType.ElementType.ToString(), out string at))
                {
                    return $"{at}[]";
                }

                if (Utilities.IsAbiStruct(ts))
                {
                    return $"byte[][]";
                }

            }
            else
            {
                if (predefinedTypeConversions.TryGetValue(ts.ToString(), out string at))
                {
                    return $"{at}";
                }

                if (Utilities.IsAbiStruct(ts))
                {
                    return $"byte[]";
                }
            }

            //check if it's a transaction type
            switch (ts.ToString())
            {
                case "AVM.ClientGenerator.Core.TransactionReference":
                    return ABI.ABIType["txn"];
                case "AVM.ClientGenerator.Core.PaymentTransactionReference":
                    return ABI.ABIType["pay"];
                case "AVM.ClientGenerator.Core.KeyRegistrationTransactionReference":
                    return ABI.ABIType["keyreg"];
                case "AVM.ClientGenerator.Core.AssetAcceptTransactionReference":
                    return ABI.ABIType["axfer"];
                case "AVM.ClientGenerator.Core.AssetClawbackTransactionReference":
                    return ABI.ABIType["axfer"];
                case "AVM.ClientGenerator.Core.AssetConfigurationTransactionReference":
                    return ABI.ABIType["acfg"];
                case "AVM.ClientGenerator.Core.AssetFreezeTransactionReference":
                    return ABI.ABIType["afrz"];
                case "AVM.ClientGenerator.Core.AssetTransferTransactionReference":
                    return ABI.ABIType["axfer"];
                case "AVM.ClientGenerator.Core.AppCallTransactionReference":
                    return ABI.ABIType["appl"];
            }
            return abiType.ToString();
        }
    }
}
