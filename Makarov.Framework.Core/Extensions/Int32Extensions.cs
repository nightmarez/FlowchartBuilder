// <copyright file="Int32Extensions.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-06-30</date>
// <summary>Расширитель класса Int32.</summary>

using System;

namespace Makarov.Framework.Core.Extensions
{
    /// <summary>
    /// Расширитель класса Int32.
    /// </summary>
    public static class Int32Extensions
    {
        #region TimeSpan
        /// <summary>
        /// Количество дней.
        /// </summary>
        public static TimeSpan AsDays(this int d)
        {
            return new TimeSpan(d, 0, 0, 0);
        }

        /// <summary>
        /// Количество часов.
        /// </summary>
        public static TimeSpan AsHours(this int h)
        {
            return new TimeSpan(0, h, 0, 0);
        }

        /// <summary>
        /// Количество минут.
        /// </summary>
        public static TimeSpan AsMinutes(this int m)
        {
            return new TimeSpan(0, 0, m, 0);
        }

        /// <summary>
        /// Количество секунд.
        /// </summary>
        public static TimeSpan AsSeconds(this int s)
        {
            return new TimeSpan(0, 0, 0, s);
        }
        #endregion

        #region Predicates
        /// <summary>
        /// Чётное ли число.
        /// </summary>
        public static bool IsEven(this int x)
        {
            return x % 2 == 0;
        }

        /// <summary>
        /// Нечётное ли число.
        /// </summary>
        public static bool IsOdd(this int x)
        {
            return x % 2 != 0;
        }
        #endregion
    }
}