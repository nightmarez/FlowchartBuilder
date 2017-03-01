// <copyright file="Int32Extensions.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-28</date>
// <summary>Расширитель класса Int32.</summary>

namespace Makarov.FlowchartBuilder.Extensions
{
    /// <summary>
    /// Расширитель класса Int32.
    /// </summary>
    public static class Int32Extensions
    {
        #region Convertions
        /// <summary>
        /// Конвертирует миллиметры в текущие единицы.
        /// </summary>
        /// <param name="mm">Миллиметры.</param>
        /// <returns>Текущие единицы.</returns>
        public static float MMToCurr(this int mm)
        {
            return UnitsConverter.MMToCurr(mm);
        }

        /// <summary>
        /// Конвертирует текущие единицы в миллиметры.
        /// </summary>
        /// <param name="curr">Текущие единицы.</param>
        /// <returns>Миллиметры.</returns>
        public static float CurrToMM(this int curr)
        {
            return UnitsConverter.CurrToMM(curr);
        }

        /// <summary>
        /// Конвертирует текущие единицы в пиксели.
        /// </summary>
        /// <param name="curr">Текущие единицы.</param>
        /// <returns>Пиксели.</returns>
        public static int CurrToPx(this int curr)
        {
            return UnitsConverter.CurrToPx(curr);
        }

        /// <summary>
        /// Конвертирует пиксели в текущие единицы.
        /// </summary>
        /// <param name="px">Пиксели.</param>
        /// <returns>Текущие единицы.</returns>
        public static float PxToCurr(this int px)
        {
            return UnitsConverter.PxToCurr(px);
        }

        /// <summary>
        /// Конвертирует пиксели в дюймы.
        /// </summary>
        /// <param name="px">Пиксели.</param>
        /// <returns>Дюймы.</returns>
        public static float PxToInch(this int px)
        {
            return UnitsConverter.PxToInch(px);
        }

        /// <summary>
        /// Конвертирует дюймы в пиксели.
        /// </summary>
        /// <param name="inch">Дюймы.</param>
        /// <returns>Пиксели.</returns>
        public static int InchToPx(this int inch)
        {
            return UnitsConverter.InchToPx(inch);
        }

        /// <summary>
        /// Конвертирует дюймы в миллиметры.
        /// </summary>
        /// <param name="inch">Дюймы.</param>
        /// <returns>Миллиметры.</returns>
        public static float InchToMM(this int inch)
        {
            return UnitsConverter.InchToMM(inch);
        }

        /// <summary>
        /// Конвертирует миллиметры в дюймы.
        /// </summary>
        /// <param name="mm">Миллиметры.</param>
        /// <returns>Дюймы.</returns>
        public static float MMToInch(this int mm)
        {
            return UnitsConverter.MMToInch(mm);
        }

        /// <summary>
        /// Конвертирует миллиметры в пиксели.
        /// </summary>
        /// <param name="mm">Миллиметры.</param>
        /// <returns>Пиксели.</returns>
        public static int MMToPx(this int mm)
        {
            return UnitsConverter.MMToPx(mm);
        }

        /// <summary>
        /// Конвертирует пиксели в миллиметры.
        /// </summary>
        /// <param name="px">Пиксели.</param>
        /// <returns>Миллиметры.</returns>
        public static float PxToMM(this int px)
        {
            return UnitsConverter.PxToMM(px);
        }

        /// <summary>
        /// Конвертирует дюймы в текущие единицы.
        /// </summary>
        /// <param name="inch">Дюймы.</param>
        /// <returns>Текущие единицы.</returns>
        public static float InchToCurr(this int inch)
        {
            return UnitsConverter.InchToCurr(inch);
        }

        /// <summary>
        /// Конвертирует текущие единицы в дюймы.
        /// </summary>
        /// <param name="curr">Текущие единицы.</param>
        /// <returns>Дюймы.</returns>
        public static float CurrToInch(this int curr)
        {
            return UnitsConverter.CurrToInch(curr);
        }
        #endregion

        #region Scaling
        /// <summary>
        /// Масштабировать в текущий масштаб листа.
        /// </summary>
        /// <param name="val">Значение.</param>
        /// <returns>Масштабированное значение.</returns>
        public static int Scale(this int val)
        {
            return (int)(val * Core.Instance.CurrentDocument.DocumentSheet.ScaleFactor);
        }

        /// <summary>
        /// Демасштабировать из текущего масштаба листа.
        /// </summary>
        /// <param name="val">Значение.</param>
        /// <returns>Не масштабированное значение.</returns>
        public static int Unscale(this int val)
        {
            return (int)(val / Core.Instance.CurrentDocument.DocumentSheet.ScaleFactor);
        }
        #endregion
    }
}