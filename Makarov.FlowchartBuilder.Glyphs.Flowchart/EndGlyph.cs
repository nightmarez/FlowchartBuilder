// <copyright file="BeginEndGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-II-17</date>
// <summary>Глиф конца.</summary>

using System;
using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Flowchart
{
    /// <summary>
    /// Глиф конца.
    /// </summary>
    [GlyphName("End")]
    [GlyphDefaultWidth(30)]
    [GlyphDefaultHeight(15)]
    public class EndGlyph : AbstractFlowchartGlyph
    {
        /// <summary>
        /// Центрированный путь для шейпа.
        /// </summary>
        protected override GraphicsPath CenteredPath
        {
            get
            {
                var min = (int)Math.Min(ScaledWidth * ScaleFactor, ScaledHeight * ScaleFactor);

                return new Framework.Graphics.Primitives.RoundedRectangle(
                    ScaledX, ScaledY,
                    ScaledWidth, ScaledHeight, min).CreateCentered();
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
                int offsetY = ThumbnailHeight / 3;

                int width = ThumbnailWidth - (offsetX << 1);
                int height = ThumbnailHeight - (offsetY << 1);
                int min = Math.Min(width, height);

                return new Framework.Graphics.Primitives.RoundedRectangle(
                    offsetX, offsetY,
                    width, height, min).Create();
            }
        }
    }
}