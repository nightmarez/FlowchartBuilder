// <copyright file="Range.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-07</date>
// <summary>Диапазон значений.</summary>

using System;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Диапазон значений.
    /// </summary>
    public sealed class Range
    {
        public sealed class RangeException : MakarovFrameworkException
        {
            public RangeException(string message)
                : base(message ?? string.Empty)
            { }
        }

        public Range(int value)
            : this(value, value)
        { }

        public Range(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;

            if (maxValue < minValue)
                throw new RangeException("maxValue must be greater or equal than minValue.");
        }

        /// <summary>
        /// Минимальное значение диапазона.
        /// </summary>
        public int MinValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Максимальное значение диапазона.
        /// </summary>
        public int MaxValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Количество элементов в диапазоне.
        /// </summary>
        public int Count
        {
            get { return MaxValue - MinValue + 1; }
        }

        /// <summary>
        /// Можно ли объединить диапазоны.
        /// </summary>
        public bool CanBeCombinedWith(Range range)
        {
            return !(MaxValue < range.MinValue - 1 ||
                     MinValue > range.MaxValue + 1);
        }

        /// <summary>
        /// Объединяет пересекающиеся или рядом лежащие диапазоны в один.
        /// </summary>
        public Range Combine(Range range)
        {
            if (!CanBeCombinedWith(range))
                throw new RangeException("Ranges can't be combined.");

            return new Range(
                System.Math.Min(MinValue, range.MinValue),
                System.Math.Max(MaxValue, range.MaxValue));
        }

        public void ForEach(Action<int> action)
        {
            for (int i = MinValue; i <= MaxValue; i++)
                action(i);
        }

        public static Range operator +(Range range1, Range range2)
        {
            return range1.Combine(range2);
        }
    }
}