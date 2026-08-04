using System;
using System.Collections.Generic;
using System.Numerics;

namespace AVM.ClientGenerator.ABI.ARC4.Types
{
    public class UInt256 : UInt, IEquatable<UInt256>
    {
        public static UInt256 FromValue(BigInteger value)
        {
            var ret = new UInt256();
            if (!ret.From(value))
            {
                throw new Exception("Errod deserializing value");
            }
            return ret;
        }
        public static UInt256 FromValue(ulong value)
        {
            var ret = new UInt256();
            if (!ret.From(value))
            {
                throw new Exception("Errod deserializing value");
            }
            return ret;
        }
        /// <summary>
        /// Create uint256 instance with empty constructor
        /// </summary>
        /// <param name="value"></param>
        public UInt256() : base(256)
        {
        }
        /// <summary>
        /// Create uint256 instance with value
        /// </summary>
        /// <param name="value"></param>
        public UInt256(object value) : base(256)
        {
            base.From(value);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UInt256);
        }

        public bool Equals(UInt256? other)
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
            return this.Value.ToString();
        }

        public static bool operator ==(UInt256 left, UInt256 right)
        {
            return EqualityComparer<UInt256>.Default.Equals(left, right);
        }

        public static bool operator !=(UInt256 left, UInt256 right)
        {
            return !(left == right);
        }
    }
}
