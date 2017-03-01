// <copyright file="AbstractContainerGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-03-20</date>
// <summary>Абстрактный контейнер.</summary>

using System.Drawing;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Containers
{
    /// <summary>
    /// Абстрактный контейнер.
    /// </summary>
    [GlyphFamily("Containers")]
    [GlyphDefaultWidth(60)]
    [GlyphDefaultHeight(40)]
    public abstract class AbstractContainerGlyph : ContainerGlyph
    {
        /// <summary>
        /// Отрисовать картинку глифа.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawGlyph(Graphics gfx)
        {
            using (Brush brush = new SolidBrush(Color.FromArgb(5, Color.Blue)))
            {
                gfx.FillRectangle(
                    brush,
                    ScaledX - (ScaledWidth >> 1), ScaledY - (ScaledHeight >> 1),
                    ScaledWidth, ScaledHeight);
            }
        }

        /// <summary>
        /// Отрисовать картинку выделенного глифа.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawSelectedGlyph(Graphics gfx)
        {
            using (Brush brush = new SolidBrush(Color.FromArgb(15, Color.Blue)))
            {
                gfx.FillRectangle(
                    brush,
                    ScaledX - (ScaledWidth >> 1), ScaledY - (ScaledHeight >> 1),
                    ScaledWidth, ScaledHeight);
            }

            using (var pen = new Pen(Color.Silver))
            {
                pen.DashPattern = new float[] { 4, 2 };
                gfx.DrawRectangle(
                    pen,
                    ScaledX - (ScaledWidth >> 1), ScaledY - (ScaledHeight >> 1),
                    ScaledWidth, ScaledHeight);
            }
        }
    }
}
