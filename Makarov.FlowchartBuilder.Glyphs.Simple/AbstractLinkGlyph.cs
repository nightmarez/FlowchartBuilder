// <copyright file="AbstractLinkGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-03</date>
// <summary>Абстрактная связь.</summary>

using System.Drawing;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Simple
{
    /// <summary>
    /// Абстрактный глиф.
    /// </summary>
    [GlyphFamily("Links")]
    [GlyphDefaultWidth(30)]
    [GlyphDefaultHeight(30)]
    public abstract class AbstractLinkGlyph : BlockGlyph
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        protected AbstractLinkGlyph()
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