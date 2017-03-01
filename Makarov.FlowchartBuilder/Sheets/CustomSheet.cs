// <copyright file="InfiniteSheet.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-05-14</date>
// <summary>Кастомный лист.</summary>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;
using Makarov.FlowchartBuilder.Extensions;

namespace Makarov.FlowchartBuilder.Sheets
{
    [SheetFamily("Custom")]
    [SheetName("Custom")]
    public sealed class CustomSheet : Sheet
    {
        #region Properties

        /// <summary>
        /// Иконка.
        /// </summary>
        public override Bitmap Thumbnail
        {
            get
            {
                var bmp = new Bitmap(ThumbnailWidth, ThumbnailHeight);

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    // Область для рисования.
                    const int innerOffsetX = 8;
                    const int innerOffsetY = 8;
                    int innerWidth = bmp.Width - innerOffsetX * 2;
                    int innerHeight = bmp.Height - innerOffsetY * 2;

                    float w = 210f / 500f * innerWidth;
                    float h = 297f / 500f * innerHeight;

                    // Рисуем лист.
                    using (var brush = new LinearGradientBrush(
                        new PointF((innerWidth - w) / 2f + innerOffsetX, (innerHeight - h) / 2f + innerOffsetY),
                        new PointF((innerWidth - w) / 2f + innerOffsetX + w, (innerHeight - h) / 2f + innerOffsetY + h),
                        Settings.Colors.SheetGradient,
                        Color.White))
                    {
                        gfx.FillRectangle(
                            brush,
                            (innerWidth - w) / 2f + innerOffsetX,
                            (innerHeight - h) / 2f + innerOffsetY,
                            w, h);
                    }

                    using (var pen = new Pen(Settings.Colors.ThumbnailBorder))
                    {
                        pen.DashPattern = new float[] {4, 2};
                        gfx.DrawRectangle(
                            pen,
                            (innerWidth - w)/2f + innerOffsetX,
                            (innerHeight - h)/2f + innerOffsetY,
                            w, h);
                    }
                }

                bmp.DrawBorder(Settings.Colors.ThumbnailBorder);
                return bmp;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Отрисовать лист.
        /// </summary>
        /// <param name="Scaled">Нужно ли применять масштабирование.</param>
        /// <returns>Отрисованный лист.</returns>
        public override Bitmap Draw(bool Scaled)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Клонирует лист.
        /// </summary>
        /// <returns>Клон листа.</returns>
        public override object Clone()
        {
            return new CustomSheet();
        }
        #endregion
    }
}