// <copyright file="GroupGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-03-20</date>
// <summary>Группировка.</summary>

using System.Drawing;
using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs.Containers
{
    /// <summary>
    /// Группировка.
    /// </summary>
    [GlyphName("Group")]
    public sealed class GroupGlyph : AbstractContainerGlyph
    {
        public override Bitmap Thumbnail
        {
            get
            {
                Bitmap bmp = base.Thumbnail;

                const int offset = 5;

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.LightGray)))
                        gfx.FillRectangle(
                            brush,
                            offset, offset,
                            ThumbnailWidth - (offset << 1),
                            ThumbnailHeight - (offset << 1));

                    using (var borderPen = new Pen(Settings.Colors.ThumbnailBorder))
                    {
                        borderPen.DashStyle = DashStyle.Dash;

                        gfx.DrawRectangle(
                            borderPen,
                            offset, offset,
                            ThumbnailWidth - (offset << 1),
                            ThumbnailHeight - (offset << 1));
                    }
                }

                return bmp;
            }
        }
    }
}

