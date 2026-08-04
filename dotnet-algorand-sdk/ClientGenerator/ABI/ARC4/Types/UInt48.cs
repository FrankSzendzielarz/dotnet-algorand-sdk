using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class UInt48 : UInt, IEquatable<UInt48>
    {
        /// <summary>
        /// Create uint48 instance with empty constructor
        /// </summary>
        /// <param name="value"></param>
        public UInt48() : base(48)
        {
        }
        /// <summary>
        /// Create uint48 instance with value
        /// </summary>
        /// <param name="value"></param>
        public UInt48(object value) : base(48)
        {
            base.From(value);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UInt48);
        }

        public bool Equals(UInt48? other)
        {
            return !(other is null) &&
                   IsDynamic == other.IsDynamic &&
                   BitWidth == other.BitWidth &&
                   Value.Equals(other.Value) &&
                   IsDynamic == other.IsDynamic;
        }

        public override int GetHashCode()
        {
            int hashCode = 1571719810;
            hashCode = hashCode * -1521134295 + IsDynamic.GetHashCode();
            hashCode = hashCode * -1521134295 + BitWidth.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + IsDynamic.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool operator ==(UInt48 left, UInt48 right)
        {
            return EqualityComparer<UInt48>.Default.Equals(left, right);
        }

        public static bool operator !=(UInt48 left, UInt48 right)
        {
            return !(left == right);
        }
    }
}
