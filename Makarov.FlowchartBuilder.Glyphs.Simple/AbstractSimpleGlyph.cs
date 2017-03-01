// <copyright file="AbstractSimpleGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-06-12</date>
// <summary>Абстрактный глиф.</summary>

using System.Drawing;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Simple
{
    /// <summary>
    /// Абстрактный глиф.
    /// </summary>
    [GlyphFamily("Common")]
    [GlyphDefaultWidth(30)]
    [GlyphDefaultHeight(30)]
    public abstract class AbstractSimpleGlyph : BlockGlyph
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        protected AbstractSimpleGlyph()
        {
            BackgroundColor = Color.Silver;
            ForegroundColor = Color.WhiteSmoke;
        }

        /// <summary>
        /// Отрисовать заголовок.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawText(Graphics gfx)
        {
            // Текст рисовать не нужно.
        }
    }
}