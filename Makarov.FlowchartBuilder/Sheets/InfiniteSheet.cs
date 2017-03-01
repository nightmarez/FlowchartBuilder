// <copyright file="InfiniteSheet.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-29</date>
// <summary>Лист бесконечного размера.</summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Sheets
{
    [SheetFamily("Custom")]
    [SheetName("Infinite")]
    public sealed class InfiniteSheet : Sheet
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
                    int innerWidth = bmp.Width;
                    int innerHeight = bmp.Height;

                    // Рисуем лист.
                    using (var brush = new LinearGradientBrush(
                        new PointF(0, 0),
                        new PointF(innerWidth, innerHeight),
                        Settings.Colors.SheetGradient,
                        Color.White))
                    {
                        gfx.FillRectangle(
                            brush,
                            0, 0,
                            innerWidth, innerHeight);
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
        /// <param name="scaled">Нужно ли применять масштабирование.</param>
        /// <returns>Отрисованный лист.</returns>
        public override Bitmap Draw(bool scaled)
        {
            float tmpScaleFactor = ScaleFactor;

            if (!scaled)
                ScaleFactor = 1f;

            int w = Core.Instance.MainWindow.InnerRectangle.Width;
            int h = Core.Instance.MainWindow.InnerRectangle.Height;

            // Битмап для отрисовки листа.
            var bmp = new Bitmap(w, h);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                // Заливаем лист градиентом.
                using (var brush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(w, h),
                    Settings.Colors.Sheet,
                    Settings.Colors.SheetGradient))
                {
                    gfx.FillRectangle(brush, 0, 0, w, h);
                }

                // Рисуем сетку.
                if (Settings.Environment.Grid)
                    using (var pen = (Pen)Settings.Pens.SheetGrid.Clone())
                    {
                        pen.DashPattern = new[] { 4f, 1f };
                        float step = scaled
                                         ? Settings.Environment.CurrentUnits.BigStep.CurrToPx().Scale()
                                         : Settings.Environment.CurrentUnits.BigStep.CurrToPx();

                        for (float i = step; i < w; i += step)
                            gfx.DrawLine(pen, i, 0, i, h);

                        for (float i = step; i < h; i += step)
                            gfx.DrawLine(pen, 0, i, w, i);
                    }

                // Рисуем глифы в обратном порядке.
                var glyphs = new List<AbstractGlyph>(Glyphs);
                glyphs.Reverse();
                foreach (AbstractGlyph glyph in glyphs)
                    glyph.Draw(gfx);

                // Рисуем рамку выделения.
                if (_selectionStart.HasValue)
                {
                    int x = _selectionStart.Value.X <= _selectionEnd.X
                                ? _selectionStart.Value.X
                                : _selectionEnd.X;

                    int y = _selectionStart.Value.Y <= _selectionEnd.Y
                                ? _selectionStart.Value.Y
                                : _selectionEnd.Y;

                    int width = Math.Abs(_selectionEnd.X - _selectionStart.Value.X);
                    int height = Math.Abs(_selectionEnd.Y - _selectionStart.Value.Y);

                    using (var brush = new SolidBrush(Settings.Colors.Selection))
                        gfx.FillRectangle(brush, x, y, width, height);

                    gfx.DrawRectangle(Settings.Pens.SelectionBorder, x, y, width, height);
                }
            }

            if (!scaled)
                ScaleFactor = tmpScaleFactor;

            // Возвращаем отрисованный лист.
            return bmp;
        }

        /// <summary>
        /// Клонирует лист.
        /// </summary>
        /// <returns>Клон листа.</returns>
        public override object Clone()
        {
            return new InfiniteSheet();
        }
        #endregion
    }
}