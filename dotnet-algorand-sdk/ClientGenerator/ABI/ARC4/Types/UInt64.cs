using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class UInt64 : UInt, IEquatable<UInt64>
    {
        /// <summary>
        /// Create uint64 instance with empty constructor
        /// </summary>
        /// <param name="value"></param>
        public UInt64() : base(64)
        {
        }
        /// <summary>
        /// Create uint64 instance with value
        /// </summary>
        /// <param name="value"></param>
        public UInt64(object value) : base(64)
        {
            base.From(value);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UInt64);
        }

        public bool Equals(UInt64? other)
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

        public static bool operator ==(UInt64 left, UInt64 right)
        {
            return EqualityComparer<UInt64>.Default.Equals(left, right);
        }

        public static bool operator !=(UInt64 left, UInt64 right)
        {
            return !(left == right);
        }
    }
}
