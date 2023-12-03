//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023.
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------
namespace System
{
    public static class MathExtensions
    {
        public static byte Range(this byte n, byte minValue, byte maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static decimal Range(this decimal n, decimal minValue, decimal maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static double Range(this double n, double minValue, double maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static float Range(this float n, float minValue, float maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static int Range(this int n, int minValue, int maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static long Range(this long n, long minValue, long maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static sbyte Range(this sbyte n, sbyte minValue, sbyte maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static short Range(this short n, short minValue, short maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static uint Range(this uint n, uint minValue, uint maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static ulong Range(this ulong n, ulong minValue, ulong maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }

        public static ushort Range(this ushort n, ushort minValue, ushort maxValue)
        {
            return Math.Max(minValue, Math.Min(maxValue, n));
        }
    }
}
