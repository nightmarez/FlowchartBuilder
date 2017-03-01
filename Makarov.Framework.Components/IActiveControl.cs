// <copyright file="IActiveControl.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Активный контрол.</summary>

using System.Drawing;

namespace Makarov.Framework.Components
{
    /// <summary>
    /// Активный контрол.
    /// </summary>
    public interface IActiveControl
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        string ActiveCaption { get; set; }

        /// <summary>
        /// Текст всплывающей подсказки.
        /// </summary>
        string ActiveBaloon { get; set; }

        /// <summary>
        /// Иконка.
        /// </summary>
        Bitmap ActiveIcon { get; set; }

        /// <summary>
        /// Активен ли контрол.
        /// </summary>
        bool ActiveEnabled { get; set; }

        /// <summary>
        /// Нажат ли контрол.
        /// </summary>
        bool ActiveChecked { get; set; }
    }
}