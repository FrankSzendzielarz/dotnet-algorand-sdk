using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class String : VariableArray<Byte>, IEquatable<String>
    {
        public String() : base()
        {
        }

        // ARC4 "string" is always a dynamic type, regardless of its content: VariableArray<T>.IsDynamic instead
        // computes dynamism from whether any *element* is itself dynamic, which for Byte elements (always
        // static) is always false. That's correct for e.g. a plain byte[], but wrong for string - and it matters
        // whenever a String is nested inside a Tuple/array (e.g. string[]) and its IsDynamic is queried to decide
        // between inlining it (static) or offset/tail-addressing it (dynamic): getting this wrong here produces
        // bytes the AVM's own ARC4 decoder rejects as "invalid tail pointer".
        public override bool IsDynamic => true;

        public override bool Equals(object? obj)
        {
            return Equals(obj as String);
        }

        public bool Equals(String? other)
        {
            return !(other is null) &&
                   IsDynamic == other.IsDynamic &&
                   EqualityComparer<List<Byte>>.Default.Equals(Value, other.Value) &&
                   IsDynamic == other.IsDynamic;
        }

        public override int GetHashCode()
        {
            int hashCode = 1616527590;
            hashCode = hashCode * -1521134295 + IsDynamic.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Byte>>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + IsDynamic.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(Value.Select(v => (byte)v.ToValue()).ToArray());
        }

        public static bool operator ==(String left, String right)
        {
            return EqualityComparer<String>.Default.Equals(left, right);
        }

        public static bool operator !=(String left, String right)
        {
            return !(left == right);
        }
    }
}
