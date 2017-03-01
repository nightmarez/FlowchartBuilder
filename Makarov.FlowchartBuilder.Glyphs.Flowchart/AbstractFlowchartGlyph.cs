// <copyright file="AbstractFlowchartGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-II-15</date>
// <summary>Абстрактный глиф.</summary>

using System.Drawing;
using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Flowchart
{
    /// <summary>
    /// Абстрактный глиф.
    /// </summary>
    [GlyphFamily("Flowchart")]
    [GlyphDefaultWidth(30)]
    [GlyphDefaultHeight(30)]
    public abstract class AbstractFlowchartGlyph : BlockGlyph
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        protected AbstractFlowchartGlyph()
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

        /// <summary>
        /// Центрированный путь для шейпа.
        /// </summary>
        protected abstract GraphicsPath CenteredPath
        { 
            get;
        }

        /// <summary>
        /// Путь для шейпа.
        /// </summary>
        protected abstract GraphicsPath Path
        { 
            get;
        }

        /// <summary>
        /// Отрисовать картинку глифа.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        protected override void DrawGlyph(Graphics gfx)
        {
            using (GraphicsPath path = CenteredPath)
            {
                using (Brush brush = new LinearGradientBrush(
                    new PointF(ScaledX - ScaledWidth / 2, ScaledY - ScaledHeight / 2),
                    new PointF(ScaledX + ScaledWidth / 2, ScaledY + ScaledHeight / 2),
                    ForegroundColor, BackgroundColor))
                {
                    gfx.FillPath(brush, path);
                }

                using (var pen = new Pen(BorderColor, ScaledBorderWidth))
                {
                    gfx.DrawPath(pen, path);
                }
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

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    using (GraphicsPath path = Path)
                    {
                        using (Brush brush = new LinearGradientBrush(
                            new PointF(0, 0),
                            new PointF(ThumbnailWidth, ThumbnailHeight),
                            Settings.Colors.ThumbnailObjectGradientStart,
                            Settings.Colors.ThumbnailObjectGradientEnd))
                        {
                            gfx.FillPath(brush, path);
                        }

                        gfx.DrawPath(Settings.Pens.ThumbnailBorder, path);
                        DrawThumbnailBorder(gfx);
                    }
                }

                return bmp;
            }
        }
    }
}