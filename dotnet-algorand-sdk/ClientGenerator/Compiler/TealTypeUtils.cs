using Algorand;
using Algorand.AVM.ClientGenerator.ABI.ARC56;
using AVM.ClientGenerator.ABI.ARC4;
using AVM.ClientGenerator.Compiler.Variables;

using AVM.ClientGenerator.Core.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;


namespace AVM.ClientGenerator.Compiler
{
    public static class TealTypeUtils
    {
        internal static List<string> PredefinedIntegerStoredTypes = new List<string>() { "bool", typeof(Boolean).Name, "byte", typeof(Byte).Name, "sbyte", typeof(SByte).Name, "char", typeof(Char).Name, "int", typeof(Int32).Name, "uint", typeof(UInt32).Name, "long", typeof(Int64).Name, "ulong", typeof(UInt64).Name, "short", typeof(Int16).Name, "ushort", typeof(UInt16).Name };
        internal static List<string> PredefinedByteSliceStoreTypes = new List<string>() { "decimal", typeof(Decimal).Name, "byte[]", "int[]", "uint[]", "byte[]", "sbyte[]", "short[]", "ushort[]", "long[]", "ulong[]", "string", "String", "AVM.ClientGenerator.Core.AssetReference[]", typeof(BigInteger).FullName };
        internal static List<string> PredefinedSignedIntegerTypes = new List<string>() { "sbyte", typeof(SByte).Name, "int", typeof(Int32).Name, "long", typeof(Int64).Name, "ushort", typeof(UInt16).Name };

        internal static bool IsFixed(this ABIEncodingType abiEncoding)
        {
            switch (abiEncoding)
            {
                case ABIEncodingType.FixedByteArray:
                case ABIEncodingType.FixedInteger:
                    return true;
                default:
                    return false;

            }
        }


        internal static Dictionary<string, (ABIEncodingType encodingType, int byteSize)> TypeEncodings = new Dictionary<string, (ABIEncodingType, int)>
        {
            {"bool", (ABIEncodingType.FixedByteArray,1) },
            { typeof(Boolean).FullName, (ABIEncodingType.FixedByteArray,1)},
            { typeof(Boolean).Name, (ABIEncodingType.FixedByteArray,1)},
            { "byte", (ABIEncodingType.FixedInteger,1)},
            { typeof(Byte).FullName, (ABIEncodingType.FixedInteger,1)},
            { typeof(Byte).Name, (ABIEncodingType.FixedInteger,1)},
            { "sbyte", (ABIEncodingType.FixedInteger,1)},
            { typeof(SByte).FullName, (ABIEncodingType.FixedInteger,1)},
            { typeof(SByte).Name, (ABIEncodingType.FixedInteger,1)},
            { "char", (ABIEncodingType.FixedInteger,1)},
            { typeof(Char).FullName, (ABIEncodingType.FixedInteger,1)},
            { typeof(Char).Name, (ABIEncodingType.FixedInteger,1)},
            { "int", (ABIEncodingType.FixedInteger,4)},
            { typeof(Int32).FullName, (ABIEncodingType.FixedInteger,4)},
            { typeof(Int32).Name, (ABIEncodingType.FixedInteger,4)},
            { "uint", (ABIEncodingType.FixedInteger,4)},
            { typeof(UInt32).FullName, (ABIEncodingType.FixedInteger,4)},
            { typeof(UInt32).Name, (ABIEncodingType.FixedInteger,4)},
            { "long", (ABIEncodingType.FixedInteger,8)},
            { typeof(Int64).FullName, (ABIEncodingType.FixedInteger,8)},
            { typeof(Int64).Name, (ABIEncodingType.FixedInteger,8)},
            { "ulong", (ABIEncodingType.FixedInteger,8)},
            { typeof(UInt64).FullName, (ABIEncodingType.FixedInteger,8)},
            { typeof(UInt64).Name, (ABIEncodingType.FixedInteger,8)},
            { "short", (ABIEncodingType.FixedInteger,2)},
            { typeof(Int16).FullName, (ABIEncodingType.FixedInteger,2)},
            { typeof(Int16).Name, (ABIEncodingType.FixedInteger,2)},
            { "ushort", (ABIEncodingType.FixedInteger,2)},
            { typeof(UInt16).FullName, (ABIEncodingType.FixedInteger,2)},
            { typeof(UInt16).Name, (ABIEncodingType.FixedInteger,2)},
            //{  typeof(BigInteger).FullName, (ABIEncodingType.VariableByteArray,0)},
            {  "decimal", (ABIEncodingType.VariableByteArray,0)},
            {  typeof(Decimal).FullName, (ABIEncodingType.VariableByteArray,0)},
            {  typeof(Decimal).Name, (ABIEncodingType.VariableByteArray,0)},
            {  "string", (ABIEncodingType.VariableByteArray,0)},
            {  "String", (ABIEncodingType.VariableByteArray,0)},
            {  typeof(String).FullName, (ABIEncodingType.VariableByteArray,0)},
            {  typeof(Algorand.Algod.Model.Transactions.PaymentTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetTransferTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.ApplicationCallTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.ApplicationNoopTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.ApplicationClearStateTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.ApplicationCloseOutTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.ApplicationCreateTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.ApplicationDeleteTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.ApplicationOptInTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.ApplicationUpdateTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetAcceptTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetChangeTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetClawbackTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetConfigurationTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetCreateTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetDestroyTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetFreezeTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetMovementsTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.AssetUpdateTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.KeyRegisterOfflineTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.KeyRegisterOnlineTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.KeyRegistrationTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.StateProofTransaction).FullName, (ABIEncodingType.Transaction,0)},
            {  typeof(Algorand.Algod.Model.Transactions.HeartBeatTransaction).FullName, (ABIEncodingType.Transaction,0)},
        };

        internal enum UnaryModifier
        {
            LogicalNegate,
            OnesComplement,
            Minus,
            None
        };

        internal static bool GetABIMethodDetails(ISymbol symbol, out AVM.ClientGenerator.Core.OnCompleteType callType, out string selector)
        {
            callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp;//not relevant for smart sigs

            var ABImethod = symbol
                            .GetAttributes()
                            .Where(a => a.AttributeClass.Name == nameof(SmartContractMethodAttribute) || a.AttributeClass.Name == nameof(SmartSignatureMethodAttribute))
                            .FirstOrDefault();

            if (ABImethod != null)
            {
                if (ABImethod.ConstructorArguments.Any(kv => kv.Type.Name == nameof(Core.OnCompleteType)))
                {
                    var callTypeConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Core.OnCompleteType)).First();
                    callType = (Core.OnCompleteType)callTypeConst.Value;
                }

                var optionalLabelConst = ABImethod.ConstructorArguments.Where(kv => kv.Type.Name == "String").First();
                selector = (string)optionalLabelConst.Value;

                return true;
            }
            else
            {
                callType = default(Core.OnCompleteType);
                selector = default(string);
                return false;
            }
        }

        internal static bool HasSupportedOperators(this Microsoft.CodeAnalysis.TypeInfo type)
        {
            var typeSymbol = type.Type.ToString();
            return PredefinedIntegerStoredTypes.Contains(typeSymbol) || PredefinedByteSliceStoreTypes.Contains(typeSymbol);
        }

        internal static bool IsIntegral(this Microsoft.CodeAnalysis.TypeInfo type)
        {
            var typeSymbol = type.Type.ToString();
            return PredefinedIntegerStoredTypes.Contains(typeSymbol);
        }

        internal static bool IsIntegral(this ITypeSymbol type)
        {
            var typeSymbol = type.ToString();
            return (PredefinedIntegerStoredTypes.Contains(typeSymbol));
        }

        internal static bool IsSignedIntegral(this ITypeSymbol type)
        {
            var typeSymbol = type.ToString();
            return (PredefinedSignedIntegerTypes.Contains(typeSymbol));
        }

        internal static string GetMethodSelector(this IMethodSymbol method, SemanticModel model)
        {
            var methodSyntaxRef = method.DeclaringSyntaxReferences.FirstOrDefault();
            if (methodSyntaxRef != null)
            {
                SyntaxNode syntaxNode = methodSyntaxRef.GetSyntax();
                MethodDeclarationSyntax methodDeclarationSyntax = syntaxNode as MethodDeclarationSyntax;
                if (methodDeclarationSyntax != null)
                {
                    MethodDescription md = MethodDescription.FromMethod(methodDeclarationSyntax, model);
                    if (Enumerable.SequenceEqual(md.Selector, md.ToARC4MethodSelector()))
                    {
                        return md.Selector.ToHex();
                    }
                    else
                    {
                        return Encoding.UTF8.GetString(md.Selector);
                    }
                }

            }
            return "";
        }

        internal static string ToHex(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        internal static byte[] ReverseIfLittleEndian(this byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                return bytes.Reverse().ToArray();
            }
            else
            {
                return bytes;
            }
        }

        private static byte[] bigIntegerToBytes(BigInteger value)
        {
            byte[] bytes;
            if (value < 0)
            {

                bytes = value.ToByteArray().Reverse().ToArray();
                bytes = Enumerable.Repeat((byte)0xff, 64 - bytes.Length).Concat(bytes).ToArray();

            }
            else
            {
                bytes = value.ToByteArray().Reverse().ToArray();
            }


            return bytes;
        }

        private static Dictionary<string, Func<object, byte[]>> runTimeConversions = new Dictionary<string, Func<object, byte[]>>()
        {
            { typeof(bool).Name, (o)=> (bool)o ? new byte[]{ 0x80} :new byte[]{ 0x00} },
            {typeof(byte).Name,(o)=> new byte[] {(byte)o }  },
            {typeof(sbyte).Name, (o)=> new byte[] {unchecked((byte)(sbyte)o) }  },
            {typeof(char).Name, (o)=> BitConverter.GetBytes((char)o) },
            {typeof(int).Name, (o)=> BitConverter.GetBytes((int)o).ReverseIfLittleEndian() },
            {typeof(uint).Name, (o)=> BitConverter.GetBytes((uint)o).ReverseIfLittleEndian() },
            {typeof(long).Name, (o)=> BitConverter.GetBytes((long)o).ReverseIfLittleEndian() },
            {typeof(ulong).Name, (o)=> BitConverter.GetBytes((ulong)o).ReverseIfLittleEndian()  },
            {typeof(short).Name, (o)=> BitConverter.GetBytes((short)o).ReverseIfLittleEndian()  },
            {typeof(ushort).Name , (o)=> BitConverter.GetBytes((ushort)o).ReverseIfLittleEndian()  },
            {typeof(System.Numerics.BigInteger).Name,(o)=>  bigIntegerToBytes((System.Numerics.BigInteger)o)  }         ,
            {typeof(byte[]).Name,(o)=> (byte[])o },
            {typeof(string).Name,(o)=> Encoding.UTF8.GetBytes((string)o) },
            {typeof(decimal).Name,(o)=>GetDecimalBytes((decimal)o) }
        };



        public static byte[] ToByteArray(object runtimeValue)
        {

            var valueAsWireType = runtimeValue as ABI.ARC4.Types.WireType;
            if (valueAsWireType != null)
            {
                return valueAsWireType.Encode();
            }

            Type runtimeType = runtimeValue.GetType();
            string typeName = runtimeType.Name;

            if (Utilities.IsAbiStruct(runtimeType))
            {
                return EncodeStruct(runtimeType, runtimeValue);
            }
            else
            {
                if (runTimeConversions.TryGetValue(typeName, out Func<object, byte[]> func))
                {
                    return func(runtimeValue);
                }
                else
                {
                    throw new Exception($"Unsupported type {typeName}.");
                }
            }
        }



        private static byte[] encodeToVariables(byte[] fieldValueToAdd)
        {
            short length = (short)fieldValueToAdd.Length; //don't care about overflow as the max length is 4096 anyway
            var bigEndianLength = BitConverter.GetBytes(length).ReverseIfLittleEndian();
            return bigEndianLength.Concat(fieldValueToAdd).ToArray();

        }

        internal static byte[] EncodeStruct(Type structType, object runtimeValue)
        {
            /*  Fixed types:
             *      Ints, bools and the like
             *      Fixed size arrays
             *      
             *  Variable types:
             *      all arrays not decorated as fixed size
             *      structs
             *      strings
             */

            List<byte> fixedTypes = new List<byte>();
            List<byte> variableTypes = new List<byte>();
            foreach (var field in structType.GetFields())
            {
                int? fieldFixedByteSize = field.GetAttribute<ABIFixedSizeAttribute>()?.Bytes;

                object fieldValue = field.GetValue(runtimeValue);
                Type fieldType = field.FieldType;

                byte[] encodedElement = EncodeElement(fieldValue, fieldType, fieldFixedByteSize, out bool isFixed);
                if (encodedElement != null)
                {
                    if (isFixed)
                    {
                        fixedTypes.AddRange(encodedElement);
                    }
                    else
                    {
                        variableTypes.AddRange(encodeToVariables(encodedElement));
                    }
                }

            }


            return fixedTypes.Concat(variableTypes).ToArray();

        }
        //TODO move this stuff to the Clients area
        internal static byte[] EncodeArgument(object runtimeValue)
        {
            if (runtimeValue is AVMObjectType AVMObjectTypeInstance)
            {
                return AVMObjectTypeInstance.ToByteArray();
            }
            if (runtimeValue is Address addr)
            {
                return addr.Bytes;
            }

            var valueAsWireType = runtimeValue as ABI.ARC4.Types.WireType;
            if (valueAsWireType != null)
            {
                return valueAsWireType.Encode();
            }
            Type runtimeType = runtimeValue.GetType();
            return EncodeElement(runtimeValue, runtimeType, null, out bool _);
        }

        internal static byte[] EncodeElement(object elementValue, Type elementType, int? fieldFixedByteSize, out bool isFixed)
        {
            if (Utilities.IsAbiStruct(elementType))
            {
                byte[] encodedStruct = EncodeStruct(elementType, elementValue);
                isFixed = false;
                return encodedStruct;
            }
            else if (elementType.IsArray)
            {
                List<byte> bytes = new List<byte>();
                Array ar = elementValue as Array;
                var arrayElementType = elementType.GetElementType();
                foreach (var arrayElement in ar)
                {
                    byte[] arrayElementBytes = EncodeElement(arrayElement, arrayElementType, null, out bool isElementFixed);
                    if (isElementFixed)
                    {
                        bytes.AddRange(arrayElementBytes);
                    }
                    else
                    {
                        bytes.AddRange(encodeToVariables(arrayElementBytes));
                    }
                }

                if (fieldFixedByteSize != null)
                {
                    isFixed = true;
                    if (bytes.Count < fieldFixedByteSize.Value)
                    {
                        return Enumerable.Repeat((byte)0, fieldFixedByteSize.Value - bytes.Count).Concat(bytes).ToArray();
                    }
                    else
                    {
                        return bytes.Take(fieldFixedByteSize.Value).ToArray();
                    }

                }
                else
                {
                    isFixed = false;
                    return bytes.ToArray();
                }


            }
            else if (TypeEncodings.ContainsKey(elementType.FullName))
            {
                var encodingInfo = TypeEncodings[elementType.FullName];
                var encoding = encodingInfo.encodingType;

                if (encoding == ABIEncodingType.FixedInteger || encoding == ABIEncodingType.FixedByteArray)
                {
                    byte[] bytes = ToByteArray(elementValue);
                    isFixed = true;
                    return bytes;
                }
                else if (encoding == ABIEncodingType.VariableByteArray)
                {
                    var abi = new AVM.ClientGenerator.ABI.ARC4.Types.VariableArray<AVM.ClientGenerator.ABI.ARC4.Types.Byte>();
                    abi.From(elementValue);
                    byte[] bytes = abi.Encode();
                    //byte[] bytes = ToByteArray(elementValue);
                    isFixed = false;
                    return bytes;
                }
                else if (encoding == ABIEncodingType.Transaction)
                {
                    //ignore
                    isFixed = true;
                    return null;
                }

                else throw new Exception($"Currently unsupported type for client encoding {elementType.Name}");
            }
            else
            {
                throw new Exception($"Unsupported type for client encoding {elementType.Name}");
            }

        }

        internal static bool IsInnerTransaction(ITypeSymbol argType)
        {
            if (argType == null) return false;
            if (argType.ToString() == "AVM.ClientGenerator.Core.InnerTransaction") return true;
            return IsInnerTransaction(argType.BaseType);
        }





        internal static Core.VariableType DetermineType(SemanticModel semanticModel, TypeSyntax type)
        {
            switch (type)
            {
                case NullableTypeSyntax nullableTypeSyntax:
                    return Core.VariableType.Unsupported;
                case OmittedTypeArgumentSyntax omittedTypeArgumentSyntax:
                    return Core.VariableType.Unsupported;
                case PointerTypeSyntax pointerTypeSyntax:
                    return Core.VariableType.Unsupported;
                case RefTypeSyntax refTypeSyntax:
                    return Core.VariableType.Unsupported;
                case TupleTypeSyntax refTypeSyntax:
                    break;
                case NameSyntax nameSyntax:
                    break;
                case PredefinedTypeSyntax predefinedTypeSyntax:
                    break;
                case ArrayTypeSyntax arrayTypeSyntax:
                    break;
                default:
                    break;
            }

            var ts = semanticModel.GetTypeInfo(type).Type;

            if (Utilities.IsAbiStruct(ts as INamedTypeSymbol))
            {
                return Core.VariableType.ABIStruct;
            }

            string typeSymbol;
            if (ts == null) return Core.VariableType.Unsupported;
            if (!ts.IsTupleType)
            {
                typeSymbol = ts.ToString();
                if (ts.NullableAnnotation == NullableAnnotation.Annotated)
                {
                    // Remove the '?' suffix if the type is nullable
                    typeSymbol = typeSymbol.TrimEnd('?');
                }
            }

            else
                return Core.VariableType.ValueTuple; //currently only group inner transactions

            if (IsInnerTransaction(ts))
                return Core.VariableType.InnerTransaction;


            if (ApplicationRefVariable.IsApplicationRef(ts) ||
                AssetRefVariable.IsAssetRef(ts) ||
                TransactionRefVariable.IsTxRef(ts)
                ) return Core.VariableType.UlongReference;

            if (
                AccountRefVariable.IsAccountRef(ts)
             ) return Core.VariableType.ByteArrayReference;


            if (PredefinedIntegerStoredTypes.Contains(typeSymbol)) return Core.VariableType.UInt64;
            if (PredefinedByteSliceStoreTypes.Contains(typeSymbol)) return Core.VariableType.ByteSlice;

            return Core.VariableType.Unsupported;

        }


        private static bool ApplyUnaryModifier(bool input, UnaryModifier um)
        {
            switch (um)
            {
                case UnaryModifier.LogicalNegate:
                    return !input;
                case UnaryModifier.OnesComplement:
                    return !input; //This can't happen - throw exception?
                case UnaryModifier.Minus:
                    return !input; //This can't happen
                default:
                    return input;

            }
        }

        private static ulong ApplyUnaryModifier(ulong input, UnaryModifier um)
        {
            switch (um)
            {
                case UnaryModifier.LogicalNegate:
                    return ~input; //This can't happen - throw exception?
                case UnaryModifier.OnesComplement:
                    return ~input;
                case UnaryModifier.Minus:
                    throw new Exception("Error - negative operator on unsigned long");
                default:
                    return input;

            }
        }

        private static sbyte ApplyUnaryModifier(sbyte input, UnaryModifier um)
        {
            switch (um)
            {
                case UnaryModifier.LogicalNegate:
                    return (sbyte)~input; //This can't happen - throw exception?
                case UnaryModifier.OnesComplement:
                    return (sbyte)~input;
                case UnaryModifier.Minus:
                    return (sbyte)-input;
                default:
                    return input;

            }
        }

        private static int ApplyUnaryModifier(int input, UnaryModifier um)
        {
            switch (um)
            {
                case UnaryModifier.LogicalNegate:
                    return ~input; //This can't happen - throw exception?
                case UnaryModifier.OnesComplement:
                    return ~input;
                case UnaryModifier.Minus:
                    return -input;
                default:
                    return input;

            }
        }

        private static decimal ApplyUnaryModifier(decimal input, UnaryModifier um)
        {
            switch (um)
            {
                case UnaryModifier.LogicalNegate:
                    return 0;//This can't happen
                case UnaryModifier.OnesComplement:
                    return 0;//This can't happen
                case UnaryModifier.Minus:
                    return -input;
                default:
                    return input;

            }
        }

        private static uint ApplyUnaryModifier(uint input, UnaryModifier um)
        {
            switch (um)
            {
                case UnaryModifier.LogicalNegate:
                    return ~input; //This can't happen - throw exception?
                case UnaryModifier.OnesComplement:
                    return ~input;
                case UnaryModifier.Minus:
                    throw new Exception("Error - negative operator on unsigned int");
                default:
                    return input;

            }
        }

        private static long ApplyUnaryModifier(long input, UnaryModifier um)
        {
            switch (um)
            {
                case UnaryModifier.LogicalNegate:
                    return ~input; //This can't happen - throw exception?
                case UnaryModifier.OnesComplement:
                    return ~input;
                case UnaryModifier.Minus:
                    return -input;
                default:
                    return input;

            }
        }

        private static short ApplyUnaryModifier(short input, UnaryModifier um)
        {
            switch (um)
            {
                case UnaryModifier.LogicalNegate:
                    return (short)~input; //This can't happen - throw exception?
                case UnaryModifier.OnesComplement:
                    return (short)~input;
                case UnaryModifier.Minus:
                    return (short)-input;
                default:
                    return input;

            }
        }

        private static ushort ApplyUnaryModifier(ushort input, UnaryModifier um)
        {
            switch (um)
            {
                case UnaryModifier.LogicalNegate:
                    return (ushort)~input; //This can't happen - throw exception?
                case UnaryModifier.OnesComplement:
                    return (ushort)~input;
                case UnaryModifier.Minus:
                    throw new Exception("Error - negative operator on unsigned short");
                default:
                    return input;

            }
        }



        private static Dictionary<string, Func<object, UnaryModifier, ulong>> PredefinedIntegerConversions =
            new Dictionary<string, Func<object, UnaryModifier, ulong>>()
            {
                {   "bool",                 (v,um)=> { unchecked { bool x=(bool)v; return Convert.ToUInt64(ApplyUnaryModifier(x,um)); } }                 },
                {   typeof(Boolean).Name,   (v,um)=> { unchecked { bool x=(bool)v; return Convert.ToUInt64(ApplyUnaryModifier(x,um)); } }                 },
                {   "byte",                 (v,um)=> { unchecked { byte x=(byte)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   typeof(Byte).Name,      (v,um)=> { unchecked { byte x=(byte)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   "sbyte",                (v,um)=> { unchecked { sbyte x=(sbyte)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   typeof(SByte).Name,     (v,um)=> { unchecked { sbyte x=(SByte)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   "char",                 (v,um)=> { unchecked { char x=(char)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   typeof(Char).Name,      (v,um)=> { unchecked { char x=(char)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   "int",                  (v,um)=> { unchecked { int x=(int)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   typeof(Int32).Name,     (v,um)=> { unchecked { int x=(int)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   "uint",                 (v,um)=> { unchecked { uint x=(uint)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   typeof(UInt32).Name,    (v,um)=> { unchecked { uint x=(uint)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   "long",                 (v,um)=> { unchecked { long x=(long)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   typeof(Int64).Name,     (v,um)=> { unchecked { long x=(long)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   "ulong",                (v,um)=> { unchecked { ulong x=(ulong)v; return ApplyUnaryModifier(x,um); } }                 },
                {   typeof(UInt64).Name,    (v,um)=> { unchecked { ulong x=(ulong)v; return ApplyUnaryModifier(x,um); } }                 },
                {   "short",                (v,um)=> { unchecked { short x=(short)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   typeof(Int16).Name,     (v,um)=> { unchecked { short x=(short)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   "ushort",               (v,um)=> { unchecked { ushort x=(ushort)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
                {   typeof(UInt16).Name,    (v,um)=> { unchecked { ushort x=(ushort)v; return (ulong)ApplyUnaryModifier(x,um); } }                 },
            };




        internal static byte[] GetDecimalBytes(decimal v)
        {
            int[] ints = Decimal.GetBits(v);
            var lo = BitConverter.GetBytes(ints[0]);
            var mid = BitConverter.GetBytes(ints[1]);
            var hi = BitConverter.GetBytes(ints[2]);
            var flags = BitConverter.GetBytes(ints[3]);
            if (BitConverter.IsLittleEndian)
            {
                lo = lo.Reverse().ToArray();
                mid = mid.Reverse().ToArray();
                hi = hi.Reverse().ToArray();
                flags = flags.Reverse().ToArray();
            }
            return lo.Concat(mid).Concat(hi).Concat(flags).ToArray();
        }

        internal static bool GetRepresentedNumeric(ITypeSymbol convertedType, object value, UnaryModifier unaryModifier, out ulong? representedValue, out byte[] representedValueBytes)
        {
            representedValue = null;
            representedValueBytes = null;
            string typeString = convertedType.ToString();
            if (typeString == "decimal" || typeString == typeof(Decimal).Name)
            {
                representedValueBytes = GetDecimalBytes(ApplyUnaryModifier((decimal)value, unaryModifier));

            }
            else
            {

                if (!PredefinedIntegerStoredTypes.Contains(convertedType.ToString()) || value == null) return false;

                representedValue = PredefinedIntegerConversions[convertedType.ToString()](value, unaryModifier);
            }



            return true;



        }

        internal static ulong MinusOne()
        {
            return PredefinedIntegerConversions["long"]((long)1, UnaryModifier.Minus);
        }
        internal static int? FixedSize(ISymbol fieldTypeSymbol)
        {
            int? fixedLength = 0;
            var fixedDeclaration = fieldTypeSymbol?.GetAttributes().Where(a => a.AttributeClass.Name == nameof(ABIFixedSizeAttribute)).FirstOrDefault();
            if (fixedDeclaration != null)
            {
                var fixedLengthArg = fixedDeclaration.ConstructorArguments.Where(kv => kv.Type.Name == nameof(Int32)).FirstOrDefault();
                if (fixedLengthArg.Value != null)
                {
                    fixedLength = (int)fixedLengthArg.Value;
                }
            }

            return fixedLength;
        }

        internal static (ABIEncodingType Encoding, int ByteWidth) DetermineEncodingType(ITypeSymbol typeSymbol)
        {

            if (typeSymbol == null) return (ABIEncodingType.Unsupported, 0);

            if (typeSymbol is IArrayTypeSymbol ats)
            {
                return (ABIEncodingType.VariableByteArray, 0);
            }

            string typeSymbolName;
            if (!typeSymbol.IsTupleType)
                typeSymbolName = typeSymbol.ToString();
            else
                return (ABIEncodingType.Unsupported, 0);



            if (ApplicationRefVariable.IsApplicationRef(typeSymbol)) return (ABIEncodingType.FixedInteger, 8);   //Apps are not refs in this system, but app ids
            if (AssetRefVariable.IsAssetRef(typeSymbol)) return (ABIEncodingType.FixedInteger, 8);               //Assets are asset ids (ulongs)
            if (AccountRefVariable.IsAccountRef(typeSymbol)) return (ABIEncodingType.FixedByteArray, 4);            //Accounts are identified by their address
            if (TransactionRefVariable.IsTxRef(typeSymbol)) return (ABIEncodingType.Transaction, 0);        //Transaction type - unsupported or ignored
            //TODO typeSymbolName needs to be checked to work for FullName or Name, especially for BigInteger
            if (TypeEncodings.ContainsKey(typeSymbolName)) return TypeEncodings[typeSymbolName];
            if (Utilities.IsAbiStruct(typeSymbol as INamedTypeSymbol))
            {
                return (ABIEncodingType.VariableByteArray, 0);
            }

            return (ABIEncodingType.Unsupported, 0);
        }


        internal static (ABIEncodingType Encoding, int ByteWidth) DetermineEncodingType(SemanticModel semanticModel, TypeSyntax type, int? fixedLength)
        {
            switch (type)
            {
                case NullableTypeSyntax nullableTypeSyntax:
                    return (ABIEncodingType.Unsupported, 0);
                case OmittedTypeArgumentSyntax omittedTypeArgumentSyntax:
                    return (ABIEncodingType.Unsupported, 0);
                case PointerTypeSyntax pointerTypeSyntax:
                    return (ABIEncodingType.Unsupported, 0);
                case RefTypeSyntax refTypeSyntax:
                    return (ABIEncodingType.Unsupported, 0);
                case TupleTypeSyntax refTypeSyntax:
                    return (ABIEncodingType.Unsupported, 0);
                case NameSyntax nameSyntax:
                    break;
                case PredefinedTypeSyntax predefinedTypeSyntax:
                    break;
                case ArrayTypeSyntax arrayTypeSyntax:
                    if (fixedLength != null && fixedLength > 0)
                        return (ABIEncodingType.FixedByteArray, fixedLength.Value);
                    else
                        return (ABIEncodingType.VariableByteArray, 0);  //always dynamic so always strip the length

                default:
                    break;
            }

            var ts = semanticModel.GetTypeInfo(type).Type;
            string typeSymbol;

            if (ts == null) return (ABIEncodingType.Unsupported, 0);
            if (!ts.IsTupleType)
                typeSymbol = ts.ToString(); //TODO - check this for FullName or Name especially for BigInteger
            else
                return (ABIEncodingType.Unsupported, 0);

            if (ApplicationRefVariable.IsApplicationRef(ts)) return (ABIEncodingType.FixedInteger, 8);   //Apps are not refs in this system, but app ids
            if (AssetRefVariable.IsAssetRef(ts)) return (ABIEncodingType.FixedInteger, 8);               //Assets are asset ids (ulongs)
            if (AccountRefVariable.IsAccountRef(ts)) return (ABIEncodingType.FixedByteArray, 4);            //Accounts are identified by their address
            if (TransactionRefVariable.IsTxRef(ts)) return (ABIEncodingType.Transaction, 0);        //Transaction type - unsupported or ignored
            if (TypeEncodings.ContainsKey(typeSymbol)) return TypeEncodings[typeSymbol];
            if (Utilities.IsAbiStruct(ts as INamedTypeSymbol))
            {
                return (ABIEncodingType.VariableByteArray, 0);
            }

            return (ABIEncodingType.Unsupported, 0);
        }


    }
}
