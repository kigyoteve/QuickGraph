using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    static class HashCodeHelper
    {
        const Int32 FNV1_prime_32 = 16777619;
        const Int32 FNV1_basis_32 = unchecked((int)2166136261);
        const Int64 FNV1_prime_64 = 1099511628211;
        const Int64 FNV1_basis_64 = unchecked((int)14695981039346656037);

        private static Int32 Fold(Int32 hash, byte value)
        {
            return (hash * FNV1_prime_32) ^ (Int32)value;
        }

        private static Int32 Fold(Int32 hash, Int32 value)
        {
            return Fold(Fold(Fold(Fold(hash,
                (byte)value),
                (byte)(((UInt32)value) >> 8)),
                (byte)(((UInt32)value) >> 16)),
                (byte)(((UInt32)value) >> 24));
        }

        /// <summary>
        /// Combines two hashcodes in a strong way.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Int32 Combine(Int32 x, Int32 y)
        {
            return Fold(Fold(FNV1_basis_32, x), y);
        }

        /// <summary>
        /// Combines four hashcodes in a strong way.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public static Int32 Combine(Int32 x, Int32 y, Int32 z, Int32 w)
        {
            return Fold(Fold(Fold(Fold(FNV1_basis_32, x), y), z), w);
        }
    }
}
