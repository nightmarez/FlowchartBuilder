// <copyright file="PieGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-05-20</date>
// <summary>Фрагмент эллипса.</summary>

using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Shapes
{
    /// <summary>
    /// Фрагмент эллипса.
    /// </summary>
    [GlyphName("Pie")]
    public sealed class PieGlyph : AbstractShapeGlyph
    {
        /// <summary>
        /// Центрированный путь для шейпа.
        /// </summary>
        protected override GraphicsPath CenteredPath
        {
            get
            {
                return new Framework.Graphics.Primitives.Pie(
                    ScaledX, ScaledY,
                    ScaledWidth, ScaledHeight,
                    180, 180).CreateCentered();
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
                int offsetY = ThumbnailHeight >> 3;

                return new Framework.Graphics.Primitives.Pie(
                    offsetX, offsetY,
                    ThumbnailWidth - (offsetX << 1), ThumbnailHeight - (offsetY << 1),
                    180, 180).Create();
            }
        }
    }
}