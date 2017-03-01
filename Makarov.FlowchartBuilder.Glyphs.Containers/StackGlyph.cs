// <copyright file="StackGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-03-20</date>
// <summary>Стек.</summary>

using System.Drawing;
using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Containers
{
    /// <summary>
    /// Стек.
    /// </summary>
    [GlyphName("Stack")]
    public sealed class StackGlyph : AbstractContainerGlyph
    {
        public override Bitmap Thumbnail
        {
            get
            {
                Bitmap bmp = base.Thumbnail;

                const int offset = 5;

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    int outerWidth = ThumbnailWidth - (offset << 1);
                    int outerHeight = ThumbnailHeight - (offset << 1);

                    using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.LightGray)))
                        gfx.FillRectangle(
                            brush,
                            offset, offset,
                            outerWidth, outerHeight);

                    gfx.DrawRectangle(
                        Settings.Pens.ThumbnailBorder,
                        offset, offset,
                        outerWidth, outerHeight);

                    const int count = 3;
                    const int maxCount = 5;
                    const int innerOffset = 2;

                    int innerWidth = outerWidth - (innerOffset << 1);
                    int innerHeight = (outerHeight - (maxCount + 1)*innerOffset)/maxCount;

                    for (int j = 0; j < count; j++)
                        if (j < maxCount)
                        {
                            var rect = new Rectangle(
                                offset + innerOffset,
                                offset + (j + 1)*innerOffset + j*innerHeight,
                                innerWidth, innerHeight);

                            using (Brush gradientBrush = new LinearGradientBrush(
                                rect,
                                Settings.Colors.ThumbnailGradientStart,
                                Settings.Colors.ThumbnailObjectGradientEnd, 90f))
                            {
                                gfx.FillRectangle(gradientBrush, rect);
                            }

                            gfx.DrawRectangle(Settings.Pens.ThumbnailBorder, rect);
                        }
                }

                return bmp;
            }
        }
    }
}
