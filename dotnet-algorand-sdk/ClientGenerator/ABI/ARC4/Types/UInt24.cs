using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class UInt24 : UInt, IEquatable<UInt24>
    {
        /// <summary>
        /// Create uint24 instance with empty constructor
        /// </summary>
        /// <param name="value"></param>
        public UInt24() : base(24)
        {
        }
        /// <summary>
        /// Create uint24 instance with value
        /// </summary>
        /// <param name="value"></param>
        public UInt24(object value) : base(24)
        {
            base.From(value);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UInt24);
        }

        public bool Equals(UInt24? other)
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

        public static bool operator ==(UInt24 left, UInt24 right)
        {
            return EqualityComparer<UInt24>.Default.Equals(left, right);
        }

        public static bool operator !=(UInt24 left, UInt24 right)
        {
            return !(left == right);
        }
    }
}
