// <copyright file="SubroutineGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-06-12</date>
// <summary>Глиф вызов процедуры.</summary>

using System.Drawing;
using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Flowchart
{
    /// <summary>
    /// Глиф вызов процедуры.
    /// </summary>
    [GlyphName("Subroutine")]
    [GlyphDefaultWidth(30)]
    [GlyphDefaultHeight(20)]
    public class SubroutineGlyph : AbstractFlowchartGlyph
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

        /// <summary>
        /// Отрисовать картинку глифа.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawGlyph(Graphics gfx)
        {
            base.DrawGlyph(gfx);

            using (var pen = new Pen(BorderColor, ScaledBorderWidth))
            {
                gfx.DrawLine(
                    pen,
                    ScaledX - ScaledWidth / 2 + ScaledWidth / 8, ScaledY - ScaledHeight / 2,
                    ScaledX - ScaledWidth / 2 + ScaledWidth / 8, ScaledY + ScaledHeight / 2);

                gfx.DrawLine(
                    pen,
                    ScaledX + ScaledWidth / 2 - ScaledWidth / 8, ScaledY - ScaledHeight / 2,
                    ScaledX + ScaledWidth / 2 - ScaledWidth / 8, ScaledY + ScaledHeight / 2);
            }
        }

        /// <summary>
        /// Иконка.
        /// </summary>
        public override Bitmap Thumbnail
        {
            get
            {
                Bitmap bmp = base.Thumbnail;

                using (GraphicsPath path = Path)
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.DrawLine(
                        Settings.Pens.ThumbnailBorder,
                        path.GetBounds().Left + path.GetBounds().Width / 8f, path.GetBounds().Top,
                        path.GetBounds().Left + path.GetBounds().Width / 8f, path.GetBounds().Top + path.GetBounds().Height);

                    gfx.DrawLine(
                        Settings.Pens.ThumbnailBorder,
                        path.GetBounds().Left + path.GetBounds().Width * 7f / 8f, path.GetBounds().Top,
                        path.GetBounds().Left + path.GetBounds().Width * 7f / 8f, path.GetBounds().Top + path.GetBounds().Height);
                }

                return bmp;
            }
        }
    }
}