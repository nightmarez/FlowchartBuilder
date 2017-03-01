// <copyright file="RandomGenerator.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-I-27</date>
// <summary>Генератор случайных чисел (Синглетон).</summary>

using System;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Генератор случайных чисел (Синглетон).
    /// </summary>
    public sealed class RandomGenerator : Singleton<RandomGenerator>
    {
        /// <summary>
        /// Минимальное значение больше максимального.
        /// </summary>
        public sealed class MinGreaterThanMaxException : MakarovFrameworkException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            public MinGreaterThanMaxException()
                : base("Min value greater than max value.")
            { }

            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="message">Сообщение.</param>
            /// <param name="innerException">Внутреннее исключение.</param>
            public MinGreaterThanMaxException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }

        /// <summary>
        /// Системный генератор случайных чисел.
        /// </summary>
        private readonly Random _rnd;

        /// <summary>
        /// Конструктор.
        /// </summary>
        private RandomGenerator()
        {
            // Инициируем системный генератор случайных чисел.
            unchecked
            {
                var now = DateTime.Now;

                _rnd = new Random(
                    (int)now.Ticks + 
                    now.Millisecond * 60 * 60 * 100 +
                    now.Second * 60 * 60 +
                    now.Minute * 60 +
                    now.Hour);
            }
        }

        /// <summary>
        /// Возвращает неотрицательное целое случайное число.
        /// </summary>
        /// <returns>Неотрицательное целое случайное число.</returns>
        public int Next()
        {
            return _rnd.Next();
        }

        /// <summary>
        /// Возвращает целое случайное число.
        /// </summary>
        /// <param name="maxValue">Верхняя граница.</param>
        /// <returns>Целое случайное число.</returns>
        public int Next(int maxValue)
        {
            return _rnd.Next(maxValue);
        }

        /// <summary>
        /// Возвращает целое случайное число из диапазона [minValue; maxValue).
        /// </summary>
        /// <param name="minValue">Нижняя граница.</param>
        /// <param name="maxValue">Верхняя граница.</param>
        /// <returns>Целое случайное число.</returns>
        public int Next(int minValue, int maxValue)
        {
            // Проверяем, не больше ли минимальное значение максимального.
            if (minValue > maxValue)
                throw new MinGreaterThanMaxException();

            return _rnd.Next(minValue, maxValue);
        }

        /// <summary>
        /// Возвращает целое случайное число из диапазона.
        /// </summary>
        public int Next(Range range)
        {
            return _rnd.Next(range.MinValue, range.MaxValue + 1);
        }

        /// <summary>
        /// Возвращает случайное число типа byte.
        /// </summary>
        /// <returns>Случайное число типа byte.</returns>
        public byte NextByte()
        {
            return (byte)_rnd.Next(256);
        }

        /// <summary>
        /// Возвращает случайное число типа double в интервале [0.0;1.0].
        /// </summary>
        /// <returns>Случайное число типа double в интервале [0.0;1.0].</returns>
        public double NextDouble()
        {
            return _rnd.NextDouble();
        }

        /// <summary>
        /// Возвращает случайное число типа float в интервале [0.0;1.0].
        /// </summary>
        /// <returns>Случайное число типа float в интервале [0.0;1.0].</returns>
        public float NextFloat()
        {
            return (float)_rnd.NextDouble();
        }

        /// <summary>
        /// Возвращает случайное число типа double в интервале [0.0;maxValue].
        /// </summary>
        /// <param name="maxValue">Верхняя граница.</param>
        /// <returns>Случайное число типа double в интервале [0.0;maxValue].</returns>
        public double NextDouble(double maxValue)
        {
            return _rnd.NextDouble() * maxValue;
        }

        /// <summary>
        /// Возвращает случайное число типа float в интервале [0.0;maxValue].
        /// </summary>
        /// <param name="maxValue">Верхняя граница.</param>
        /// <returns>Случайное число типа float в интервале [0.0;maxValue].</returns>
        public float NextFloat(float maxValue)
        {
            return (float)(_rnd.NextDouble() * maxValue);
        }

        /// <summary>
        /// Возвращает случайное число типа double в интервале [minValue;maxValue].
        /// </summary>
        /// <param name="minValue">Нижняя граница.</param>
        /// <param name="maxValue">Верхняя граница.</param>
        /// <returns>Случайное число типа double в интервале [minValue;maxValue].</returns>
        public double NextDouble(double minValue, double maxValue)
        {
            // Проверяем, не больше ли минимальное значение максимального.
            if (minValue > maxValue)
                throw new MinGreaterThanMaxException();

            return _rnd.NextDouble() * (maxValue - minValue) + minValue;
        }

        /// <summary>
        /// Возвращает случайное число типа float в интервале [minValue;maxValue].
        /// </summary>
        /// <param name="minValue">Нижняя граница.</param>
        /// <param name="maxValue">Верхняя граница.</param>
        /// <returns>Случайное число типа float в интервале [minValue;maxValue].</returns>
        public float NextFloat(float minValue, float maxValue)
        {
            // Проверяем, не больше ли минимальное значение максимального.
            if (minValue > maxValue)
                throw new MinGreaterThanMaxException();

            return (float)(_rnd.NextDouble() * (maxValue - minValue) + minValue);
        }
    }
}