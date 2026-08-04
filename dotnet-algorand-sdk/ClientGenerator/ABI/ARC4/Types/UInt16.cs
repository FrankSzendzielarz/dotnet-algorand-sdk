using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class UInt16 : UInt, IEquatable<UInt16>
    {
        /// <summary>
        /// Create uint16 instance with empty constructor
        /// </summary>
        /// <param name="value"></param>
        public UInt16() : base(16)
        {
        }
        /// <summary>
        /// Create uint16 instance with value
        /// </summary>
        /// <param name="value"></param>
        public UInt16(object value) : base(16)
        {
            base.From(value);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UInt16);
        }

        public bool Equals(UInt16? other)
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

        public static bool operator ==(UInt16 left, UInt16 right)
        {
            return EqualityComparer<UInt16>.Default.Equals(left, right);
        }

        public static bool operator !=(UInt16 left, UInt16 right)
        {
            return !(left == right);
        }
    }
}
