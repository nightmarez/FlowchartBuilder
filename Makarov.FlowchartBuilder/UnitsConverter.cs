// <copyright file="UnitsConverter.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-18</date>
// <summary>Конвертер единиц измерения.</summary>

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Конвертер единиц измерения.
    /// </summary>
    public static class UnitsConverter
    {
        /// <summary>
        /// Конвертирует миллиметры в текущие единицы.
        /// </summary>
        /// <param name="mm">Миллиметры.</param>
        /// <returns>Текущие единицы.</returns>
        public static float MMToCurr(float mm)
        {
            return mm * Settings.Environment.CurrentUnits.InMM;
        }

        /// <summary>
        /// Конвертирует текущие единицы в миллиметры.
        /// </summary>
        /// <param name="curr">Текущие единицы.</param>
        /// <returns>Миллиметры.</returns>
        public static float CurrToMM(float curr)
        {
            return curr * Settings.Environment.CurrentUnits.MMInThis;
        }

        /// <summary>
        /// Конвертирует текущие единицы в пиксели.
        /// </summary>
        /// <param name="curr">Текущие единицы.</param>
        /// <returns>Пиксели.</returns>
        public static int CurrToPx(float curr)
        {
            return InchToPx(CurrToInch(curr));
        }

        /// <summary>
        /// Конвертирует пиксели в текущие единицы.
        /// </summary>
        /// <param name="px">Пиксели.</param>
        /// <returns>Текущие единицы.</returns>
        public static float PxToCurr(float px)
        {
            return MMToCurr(PxToMM(px));
        }

        /// <summary>
        /// Конвертирует пиксели в дюймы.
        /// </summary>
        /// <param name="px">Пиксели.</param>
        /// <returns>Дюймы.</returns>
        public static float PxToInch(float px)
        {
            return px / Settings.Environment.DPI;
        }

        /// <summary>
        /// Конвертирует дюймы в пиксели.
        /// </summary>
        /// <param name="inch">Дюймы.</param>
        /// <returns>Пиксели.</returns>
        public static int InchToPx(float inch)
        {
            return (int)(inch * Settings.Environment.DPI);
        }

        /// <summary>
        /// Конвертирует дюймы в миллиметры.
        /// </summary>
        /// <param name="inch">Дюймы.</param>
        /// <returns>Миллиметры.</returns>
        public static float InchToMM(float inch)
        {
            return inch * Core.Instance.Units["Inches"].MMInThis;
        }

        /// <summary>
        /// Конвертирует миллиметры в дюймы.
        /// </summary>
        /// <param name="mm">Миллиметры.</param>
        /// <returns>Дюймы.</returns>
        public static float MMToInch(float mm)
        {
            return mm * Core.Instance.Units["Inches"].InMM;
        }

        /// <summary>
        /// Конвертирует миллиметры в пиксели.
        /// </summary>
        /// <param name="mm">Миллиметры.</param>
        /// <returns>Пиксели.</returns>
        public static int MMToPx(float mm)
        {
            return InchToPx(MMToInch(mm));
        }

        /// <summary>
        /// Конвертирует пиксели в миллиметры.
        /// </summary>
        /// <param name="px">Пиксели.</param>
        /// <returns>Миллиметры.</returns>
        public static float PxToMM(float px)
        {
            return InchToMM(PxToInch(px));
        }

        /// <summary>
        /// Конвертирует дюймы в текущие единицы.
        /// </summary>
        /// <param name="inch">Дюймы.</param>
        /// <returns>Текущие единицы.</returns>
        public static float InchToCurr(float inch)
        {
            return MMToCurr(InchToMM(inch));
        }

        /// <summary>
        /// Конвертирует текущие единицы в дюймы.
        /// </summary>
        /// <param name="curr">Текущие единицы.</param>
        /// <returns>Дюймы.</returns>
        public static float CurrToInch(float curr)
        {
            return MMToInch(CurrToMM(curr));
        }
    }
}