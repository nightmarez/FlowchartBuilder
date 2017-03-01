// <copyright file="OperationGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-06-12</date>
// <summary>Глиф операции.</summary>

using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Flowchart
{
    /// <summary>
    /// Глиф операции.
    /// </summary>
    [GlyphName("Operation")]
    [GlyphDefaultWidth(30)]
    [GlyphDefaultHeight(20)]
    public class OperationGlyph : AbstractFlowchartGlyph
    {
        /// <summary>
        /// Центрированный путь для шейпа.
        /// </summary>
        protected override GraphicsPath CenteredPath
        {
            get
            {
                return new Framework.Graphics.Primitives.Rectangle(
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
                int offsetY = ThumbnailHeight >> 2;

                return new Framework.Graphics.Primitives.Rectangle(
                    offsetX, offsetY,
                    ThumbnailWidth - (offsetX << 1), ThumbnailHeight - (offsetY << 1)).Create();
            }
        }
    }
}