// <copyright file="Math.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-II-15</date>
// <summary>Математические функции.</summary>

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Математические функции.
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// Переводит градусы в радианы.
        /// </summary>
        public static float DegToRad(this float deg)
        {
            return (float)System.Math.PI / 180f * deg;
        }

        /// <summary>
        /// Переводит градусы в радианы.
        /// </summary>
        public static double DegToRad(this double deg)
        {
            return System.Math.PI / 180.0 * deg;
        }

        /// <summary>
        /// Переводит радианы в градусы.
        /// </summary>
        public static float RadToDeg(this float rad)
        {
            return 180f / (float)System.Math.PI * rad;
        }

        /// <summary>
        /// Переводит радианы в градусы.
        /// </summary>
        public static double RadToDeg(this double rad)
        {
            return 180.0 / System.Math.PI * rad;
        }
    }
}