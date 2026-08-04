using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public abstract class WireType
    {
        public abstract string GetDescription();
        public abstract bool IsDynamic { get; }
        public abstract byte[] Encode();
        public abstract uint Decode(byte[] data);
        /// <summary>
        /// Create instance of the object from the value
        /// </summary>
        /// <param name="instance">C# variable</param>
        /// <returns></returns>
        public abstract bool From(object instance);
        /// <summary>
        /// Converts WireType to c# value
        /// </summary>
        /// <returns></returns>
        public abstract object ToValue();

        public static WireType FromABIDescription(string description)
        {
            //The ToARC4Proxy will produce a *class* for each method call. 
            //The class has a property for each argument.
            //Each property initializes the WireType by calling FromABIDescription with the abidescription, which serves as documentation too.
            //There is a Transaction property that returns the invokable transaction, which can either be executed or passed in as an argument to another app call.
            
            
            string abiType=description.Trim().ToLowerInvariant();

            return IsArrayType(abiType) ??
                   IsUInt(abiType) ??
                   IsByte(abiType) ??
                   IsBool(abiType) ??
                   IsUFixed(abiType) ??
                   IsAddress(abiType) ??
                   IsString(abiType) ??
                   IsAsset(abiType) ??
                   IsApplication(abiType) ??
                   IsTuple(abiType); // allow nulls as a) there may be future expansions and b) the 'wire type' might be a Transaction
                   


        }

        private static object CreateGeneric(Type genericType, Type specificType,string elementSpec)
        {
            Type constructedType = genericType.MakeGenericType(specificType);
            // Try a (string) ctor first (backwards compatibility) otherwise fall back to parameterless
            var ctorWithString = constructedType.GetConstructor(new Type[] { typeof(string) });
            if (ctorWithString != null)
            {
                return ctorWithString.Invoke(new object[] { elementSpec });
            }
            // Parameterless ctor
            return Activator.CreateInstance(constructedType);
        }

        private static object CreateGeneric(Type genericType, Type specificType, int length, string elementSpec)
        {
            Type constructedType = genericType.MakeGenericType(specificType);
            // Try (uint,string) or (int,string)
            var ctorLenString = constructedType.GetConstructor(new Type[] { typeof(uint), typeof(string) })
                               ?? constructedType.GetConstructor(new Type[] { typeof(int), typeof(string) });
            if (ctorLenString != null)
            {
                return ctorLenString.Invoke(new object[] { (uint)length, elementSpec });
            }
            // Try (uint) or (int)
            var ctorLen = constructedType.GetConstructor(new Type[] { typeof(uint) })
                         ?? constructedType.GetConstructor(new Type[] { typeof(int) });
            if (ctorLen != null)
            {
                return ctorLen.Invoke(new object[] { (uint)length });
            }
            // Fallback to parameterless
            return Activator.CreateInstance(constructedType);
        }

        private static WireType IsArrayType(string abiType)
        {
            if (abiType.EndsWith("]"))
            {
                if (abiType[abiType.Length - 2] != '[')
                {
                    //[N] - fixed array
                    int arraySizePosition = abiType.LastIndexOf('[');
                    string baseType = abiType.Substring(0,arraySizePosition);
                    WireType baseWireType = FromABIDescription(baseType);
                    // A null base type means the array element isn't a raw ABI type but a reference to a named
                    // ARC56 struct (e.g. "myStruct[]") - propagate null so callers (e.g.
                    // ClientGeneratorARC56.defineStructs) fall back to their own "struct usage" handling instead
                    // of crashing on GetType() of a null WireType.
                    if (baseWireType == null) return null;
                    string size = abiType.Substring(arraySizePosition + 1, abiType.Length - arraySizePosition - 2);
                    var ret=CreateGeneric(typeof(FixedArray<>), baseWireType.GetType(), int.Parse(size),baseType) as WireType;
                    return ret; // return the constructed fixed array wire type

                }
                else
                {
                    //[] - variable
                    string baseType = abiType.Substring(0, abiType.Length - 2);
                    WireType baseWireType = FromABIDescription(baseType);
                    if (baseWireType == null) return null;
                    return CreateGeneric(typeof(VariableArray<>), baseWireType.GetType(),baseType) as WireType;
                }
            }
            return null;
        }

        private static WireType IsUInt(string abiType)
        {
            if (abiType == "uint16")
            {
                return new UInt16();
            }
            if (abiType == "uint24")
            {
                return new UInt24();
            }
            if (abiType == "uint32")
            {
                return new UInt32();
            }
            if (abiType == "uint48")
            {
                return new UInt48();
            }
            if (abiType == "uint64")
            {
                return new UInt64();
            }
            if (abiType == "uint128")
            {
                return new UInt128();
            }
            if (abiType == "uint256")
            {
                return new UInt256();
            }
            if (abiType == "uint512")
            {
                return new UInt512();
            }
            if (abiType.StartsWith("uint"))
            {
                return new UInt(uint.Parse(abiType.Replace("uint","") ));
            }
            return null;
        }

        private static WireType IsByte(string abiType)
        {
            if (abiType.StartsWith("byte"))
            {
                return new Byte();
            }
            return null;
        }

        private static WireType IsBool(string abiType)
        {
            if (abiType.StartsWith("bool"))
            {
                return new Bool();
            }
            return null;
        }

        private static WireType IsUFixed(string abiType)
        {
            if (abiType.StartsWith("ufixed"))
            {
                //The ufixed abiType is described by a string of this format: ufixed<N>x<M>
                //so we need to get N and M to instantiate the UFixed class.
                string sizes = abiType.Substring(6);
                string[] parts = sizes.Split('x');
                return new UFixed(uint.Parse(parts[0]), uint.Parse(parts[1]));
            }
            return null;
        }

        private static WireType IsAddress(string abiType)
        {
            if (abiType.StartsWith("address"))
            {
                return new Address();
            }
            return null;
        }

        private static WireType IsString(string abiType)
        {
            if (abiType.StartsWith("string"))
            {
                return new String();
            }
            return null;
        }


        private static WireType IsAsset(string abiType)
        {
            if (abiType.StartsWith("asset"))
            {
                return new Asset();
            }
            return null;
        }

        private static WireType IsApplication(string abiType)
        {
            if (abiType.StartsWith("application"))
            {
                return new Application();
            }
            return null;
        }

        private static WireType IsTuple(string abiType)
        {
            //Tuples are described as a list of types separated by commas and enclosed in parentheses. 
            //It is important to note that the tuple can contain other tuples, which is why
            //simply splitting by commas is not enough. We need to keep track of the parentheses.
            if (abiType.StartsWith("(") && abiType.EndsWith(")"))
            {
                List<string> elements = ParseTuple(abiType);
                Tuple tuple = new Tuple();
                foreach (string element in elements)
                {
                    tuple.Value.Add(FromABIDescription(element));
                }
                return tuple;
            }
            return null;
        }

        private static List<string> ParseTuple(string tuple)
        {
            List<string> elements = new List<string>();
            int nestedLevel = 0;
            string currentElement = "";
            
            foreach (char c in tuple)
            {
                if (c == '(')
                {
                    if (nestedLevel > 0)
                    {
                        currentElement += c;
                    }
                    nestedLevel++;
                }
                else if (c == ')')
                {
                    nestedLevel--;
                    if (nestedLevel > 0)
                    {
                        currentElement += c;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(currentElement))
                        {
                            elements.Add(currentElement);
                            currentElement = "";
                        }
                    }
                }
                else if (c == ',' && nestedLevel == 1)
                {
                    elements.Add(currentElement);
                    currentElement = "";
                }
                else
                {
                    if (nestedLevel > 0)
                    {
                        currentElement += c;
                    }
                }
            }

            return elements;
        }
    }
}
