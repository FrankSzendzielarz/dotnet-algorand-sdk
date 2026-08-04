using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class UInt32 : UInt, IEquatable<UInt32>
    {
        /// <summary>
        /// Create uint32 instance with empty constructor
        /// </summary>
        /// <param name="value"></param>
        public UInt32() : base(32)
        {
        }
        /// <summary>
        /// Create uint32 instance with value
        /// </summary>
        /// <param name="value"></param>
        public UInt32(object value) : base(32)
        {
            base.From(value);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UInt32);
        }

        public bool Equals(UInt32? other)
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

        public static bool operator ==(UInt32 left, UInt32 right)
        {
            return EqualityComparer<UInt32>.Default.Equals(left, right);
        }

        public static bool operator !=(UInt32 left, UInt32 right)
        {
            return !(left == right);
        }
    }
}
