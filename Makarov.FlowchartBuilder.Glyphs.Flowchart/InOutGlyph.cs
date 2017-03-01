// <copyright file="InOutGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-VI-12</date>
// <summary>Глиф ввода-вывода.</summary>

using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Flowchart
{
    /// <summary>
    /// Глиф ввода-вывода.
    /// </summary>
    [GlyphName("In/Out")]
    [GlyphDefaultWidth(30)]
    [GlyphDefaultHeight(20)]
    public class InOutGlyph : AbstractFlowchartGlyph
    {
        /// <summary>
        /// Центрированный путь для шейпа.
        /// </summary>
        protected override GraphicsPath CenteredPath
        {
            get
            {
                return new Framework.Graphics.Primitives.BeveledRectangle(
                    ScaledX, ScaledY,
                    ScaledWidth, ScaledHeight,
                    ScaledWidth >> 3).CreateCentered();
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

                return new Framework.Graphics.Primitives.BeveledRectangle(
                    offsetX, offsetY,
                    ThumbnailWidth - (offsetX << 1), ThumbnailHeight - (offsetY << 1),
                    ThumbnailWidth >> 3).Create();
            }
        }
    }
}