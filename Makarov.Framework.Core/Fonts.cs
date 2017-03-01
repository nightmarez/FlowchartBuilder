// <copyright file="Fonts.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-IX-27</date>
// <summary>Класс для работы со шрифтами.</summary>

using System.Drawing;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Класс для работы со шрифтами.
    /// </summary>
    public static class Fonts
    {
        /// <summary>
        /// Не удалось создать шрифт.
        /// </summary>
        public sealed class CantCreateFontException : MakarovFrameworkException
        {
            /// <param name="familyName">Семейство.</param>
            public CantCreateFontException(string familyName)
                : base(string.Format("Can't create font '{0}'.", familyName))
            { }
        }

        /// <summary>
        /// Безопасно создаёт шрифт.
        /// </summary>
        /// <param name="familyName">Семейство.</param>
        /// <param name="emSize">Размер.</param>
        /// <returns>Шрифт.</returns>
        public static Font SafeCreateFont(FontFamily familyName, float emSize)
        {
            // Допустимые стили шрифтов.
            var styles = new[]
                             {
                                 FontStyle.Regular,
                                 FontStyle.Italic,
                                 FontStyle.Bold,
                                 FontStyle.Underline,
                                 FontStyle.Strikeout
                             };

            // Проходим по всем стилям шрифтов...
            for (int i = 0; i < styles.Length; i++)
                try
                {
                    // Пытаемся создать шрифт.
                    return new Font(familyName, emSize, styles[i]);
                }
                catch
                {
                    // Если шрифт с заданным стилем создать не удалось и
                    // стили закончились, бросаем исключение.
                    if (i == styles.Length - 1)
                        break;

                    // Стили ещё остались - продолжаем пытаться создать шрифт...
                }

            throw new CantCreateFontException(familyName.Name);
        }

        /// <summary>
        /// Безопасно создаёт шрифт.
        /// </summary>
        /// <param name="familyName">Семейство.</param>
        /// <param name="emSize">Размер.</param>
        /// <returns>Шрифт.</returns>
        public static Font SafeCreateFont(string familyName, float emSize)
        {
            // Допустимые стили шрифтов.
            var styles = new[]
                             {
                                 FontStyle.Regular,
                                 FontStyle.Italic,
                                 FontStyle.Bold,
                                 FontStyle.Underline,
                                 FontStyle.Strikeout
                             };

            // Проходим по всем стилям шрифтов...
            for (int i = 0; i < styles.Length; i++)
                try
                {
                    // Пытаемся создать шрифт.
                    return new Font(familyName, emSize, styles[i]);
                }
                catch
                {
                    // Если шрифт с заданным стилем создать не удалось и
                    // стили закончились, бросаем исключение.
                    if (i == styles.Length - 1)
                        break;

                    // Стили ещё остались - продолжаем пытаться создать шрифт...
                }

            throw new CantCreateFontException(familyName);
        }
    }
}