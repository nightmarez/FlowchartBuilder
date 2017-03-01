// <copyright file="StarGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-05-20</date>
// <summary>Звезда.</summary>

using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Shapes
{
    /// <summary>
    /// Звезда.
    /// </summary>
    [GlyphName("Star")]
    public sealed class StarGlyph : AbstractShapeGlyph
    {
        /// <summary>
        /// Центрированный путь для шейпа.
        /// </summary>
        protected override GraphicsPath CenteredPath
        {
            get
            {
                return new Framework.Graphics.Primitives.Star(
                    ScaledX, ScaledY,
                    ScaledWidth, ScaledHeight).CreateCentered();
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

                return new Framework.Graphics.Primitives.Star(
                    offsetX, offsetY,
                    ThumbnailWidth - (offsetX << 1), ThumbnailHeight - (offsetY << 1)).Create();
            }
        }
    }
}