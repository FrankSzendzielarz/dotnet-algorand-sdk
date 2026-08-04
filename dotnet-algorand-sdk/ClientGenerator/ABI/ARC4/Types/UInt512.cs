using System;
using System.Collections.Generic;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class UInt512 : UInt, IEquatable<UInt512>
    {
        /// <summary>
        /// Create uint512 instance with empty constructor
        /// </summary>
        /// <param name="value"></param>
        public UInt512() : base(512)
        {
        }
        /// <summary>
        /// Create uint512 instance with value
        /// </summary>
        /// <param name="value"></param>
        public UInt512(object value) : base(512)
        {
            base.From(value);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UInt512);
        }

        public bool Equals(UInt512? other)
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

        public static bool operator ==(UInt512 left, UInt512 right)
        {
            return EqualityComparer<UInt512>.Default.Equals(left, right);
        }

        public static bool operator !=(UInt512 left, UInt512 right)
        {
            return !(left == right);
        }
    }
}
