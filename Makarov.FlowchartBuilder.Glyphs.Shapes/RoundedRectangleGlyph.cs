// <copyright file="RoundedRectangleGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-05-20</date>
// <summary>Прямоугольник со скруглёнными углами.</summary>

using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Shapes
{
    /// <summary>
    /// Прямоугольник со скруглёнными углами.
    /// </summary>
    [GlyphName("Rounded Rectangle")]
    public sealed class RoundedRectangleGlyph : AbstractShapeGlyph
    {
        /// <summary>
        /// Центрированный путь для шейпа.
        /// </summary>
        protected override GraphicsPath CenteredPath
        {
            get
            {
                return new Framework.Graphics.Primitives.RoundedRectangle(
                    ScaledX, ScaledY,
                    ScaledWidth, ScaledHeight, 15).CreateCentered();
            }
        }

        /// <summary>
        /// Путь для шейпа.
        /// </summary>
        protected override GraphicsPath Path
        {
            get
            {
                int offsetX = ThumbnailWidth >> 3;
                int offsetY = ThumbnailHeight >> 2;

                return new Framework.Graphics.Primitives.RoundedRectangle(
                    offsetX, offsetY,
                    ThumbnailWidth - (offsetX << 1), ThumbnailHeight - (offsetY << 1), 15).Create();
            }
        }
    }
}