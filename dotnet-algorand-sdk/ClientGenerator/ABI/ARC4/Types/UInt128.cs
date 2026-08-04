using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class UInt128 : UInt, IEquatable<UInt128>
    {
        /// <summary>
        /// Create uint128 instance with empty constructor
        /// </summary>
        /// <param name="value"></param>
        public UInt128() : base(128)
        {
        }
        /// <summary>
        /// Create uint128 instance with value
        /// </summary>
        /// <param name="value"></param>
        public UInt128(object value) : base(128)
        {
            base.From(value);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UInt128);
        }

        public bool Equals(UInt128? other)
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

        public static bool operator ==(UInt128 left, UInt128 right)
        {
            return EqualityComparer<UInt128>.Default.Equals(left, right);
        }

        public static bool operator !=(UInt128 left, UInt128 right)
        {
            return !(left == right);
        }
    }
}
