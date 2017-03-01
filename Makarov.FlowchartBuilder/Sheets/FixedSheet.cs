// <copyright file="FixedSheet.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-29</date>
// <summary>Лист фиксированного размера.</summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Sheets
{
    [SheetWidth(210)]
    [SheetHeight(297)]
    public abstract class FixedSheet : Sheet, ISize, IScaledSize
    {
        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        protected FixedSheet()
        {
            if (DefaultWidthInMM <= 0 || DefaultHeightInMM <= 0)
                throw new InvalidSheetSizeException(DefaultWidthInMM, DefaultHeightInMM);

            Width = DefaultWidthInMM;
            Height = DefaultHeightInMM;
        }

        protected FixedSheet(float WidthInMM, float HeightInMM)
        {
            if (WidthInMM <= 0 || HeightInMM <= 0)
                throw new InvalidSheetSizeException(WidthInMM, HeightInMM);

            Width = WidthInMM;
            Height = HeightInMM;
        }
        #endregion

        #region Private members
        /// <summary>
        /// Ширина в миллиметрах.
        /// </summary>
        private float _width;

        /// <summary>
        /// Высота в миллиметрах.
        /// </summary>
        private float _height;
        #endregion

        #region Properties
        /// <summary>
        /// Дефолтная ширина в миллиметрах.
        /// </summary>
        public float DefaultWidthInMM
        {
            get
            {
                return ((SheetWidthAttribute)(GetType().GetCustomAttributes(typeof(SheetWidthAttribute), true)[0])).WidthInMM;
            }
        }

        /// <summary>
        /// Дефолтная высота в миллиметрах.
        /// </summary>
        public float DefaultHeightInMM
        {
            get
            {
                return ((SheetHeightAttribute)(GetType().GetCustomAttributes(typeof(SheetHeightAttribute), true)[0])).HeightInMM;
            }
        }

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

                    // Размеры листа на иконке.
                    //float w = innerWidth / DefaultWidthInMM;
                    //float h = innerHeight / DefaultHeightInMM;

                    //if (w < h)
                    //{
                    //    h = DefaultHeightInMM * w;
                    //    w = DefaultWidthInMM * w;
                    //}
                    //else
                    //{
                    //    w = DefaultWidthInMM * h;
                    //    h = DefaultHeightInMM * h;
                    //}

                    float w = DefaultWidthInMM/500f*innerWidth;
                    float h = DefaultHeightInMM/500f*innerHeight;

                    // Рисуем тень.
                    const int shadowOffset = 2;

                    using (var brush = new SolidBrush(Settings.Colors.ThumbnailBorder))
                        gfx.FillRectangle(
                            brush,
                            shadowOffset + (innerWidth - w) / 2f + innerOffsetX,
                            shadowOffset + (innerHeight - h) / 2f + innerOffsetY,
                            w, h);

                    // Рисуем лист.
                    using (var brush = new LinearGradientBrush(
                        new PointF((innerWidth - w) / 2f + innerOffsetX, (innerHeight - h) / 2f + innerOffsetY),
                        new PointF((innerWidth - w) / 2f + innerOffsetX + w, (innerHeight - h) / 2f + innerOffsetY + h),
                        Settings.Colors.SheetGradient,
                        Color.White))
                    {
                        gfx.FillRectangle(
                            brush,
                            (innerWidth - w)/2f + innerOffsetX,
                            (innerHeight - h)/2f + innerOffsetY,
                            w, h);
                    }

                    gfx.DrawRectangle(
                        Settings.Pens.ThumbnailBorder,
                        (innerWidth - w) / 2f + innerOffsetX,
                        (innerHeight - h) / 2f + innerOffsetY,
                        w, h);

                    // Пишем текст.
                    //string text = string.Format("{0} x {1}", DefaultWidthInMM, DefaultHeightInMM);

                    //using (var font = "Arial".SafeCreateFont(5))
                    //{
                    //    Size size = text.CalcSize(gfx, font);
                    //    gfx.DrawString(
                    //        text,
                    //        font,
                    //        Settings.Brushes.ThumbnailText,
                    //        (ThumbnailWidth - size.Width) >> 1,
                    //        (ThumbnailHeight - size.Height) >> 1);
                    //}
                }

                bmp.DrawBorder(Settings.Colors.ThumbnailBorder);
                return bmp;
            }
        }
        #endregion

        #region ISize
        /// <summary>
        /// Ширина в миллиметрах.
        /// </summary>
        public float Width
        {
            get { return _width; }

            set
            {
                if (value <= 0)
                    throw new InvalidValueException("Sheet.Width", value.ToString());

                _width = value;
            }
        }

        /// <summary>
        /// Высота в миллиметрах.
        /// </summary>
        public float Height
        {
            get { return _height; }

            set
            {
                if (value <= 0)
                    throw new InvalidValueException("Sheet.Height", value.ToString());

                _height = value;
            }
        }
        #endregion

        #region IScaledSize
        /// <summary>
        /// Масштабированная ширина в миллиметрах.
        /// </summary>
        public float ScaledWidth
        {
            get { return Width.Scale(); }
            set { Width = value.Unscale(); }
        }

        /// <summary>
        /// Масштабированная высота в миллиметрах.
        /// </summary>
        public float ScaledHeight
        {
            get { return Height.Scale(); }
            set { Height = value.Unscale(); }
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

            // Битмап для отрисовки листа.
            var bmp = scaled
                          ? new Bitmap(ScaledWidth.MMToPx(), ScaledHeight.MMToPx())
                          : new Bitmap(Width.MMToPx(), Height.MMToPx());

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                // Заливаем лист градиентом.
                using (var brush = new LinearGradientBrush(
                    new Point(0, 0),
                    scaled
                        ? new Point(ScaledWidth.MMToPx(), ScaledHeight.MMToPx())
                        : new Point(Width.MMToPx(), Height.MMToPx()),
                    Settings.Colors.Sheet,
                    Settings.Colors.SheetGradient))
                {
                    gfx.FillRectangle(
                        brush,
                        0, 0,
                        scaled ? ScaledWidth.MMToPx() : Width.MMToPx(),
                        scaled ? ScaledHeight.MMToPx() : Height.MMToPx());
                }

                // Рисуем сетку.
                if (Settings.Environment.Grid)
                    using (var pen = (Pen) Settings.Pens.SheetGrid.Clone())
                    {
                        pen.DashPattern = new[] {4f, 1f};
                        float step = scaled
                                         ? Settings.Environment.CurrentUnits.BigStep.CurrToPx().Scale()
                                         : Settings.Environment.CurrentUnits.BigStep.CurrToPx();

                        for (float i = step; i < ScaledWidth.MMToPx(); i += step)
                            gfx.DrawLine(pen, i, 0, i, scaled ? ScaledHeight.MMToPx() : Height.MMToPx());

                        for (float i = step; i < ScaledHeight.MMToPx(); i += step)
                            gfx.DrawLine(pen, 0, i, scaled ? ScaledWidth.MMToPx() : Width.MMToPx(), i);
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
            var tmp = (FixedSheet) Activator.CreateInstance(GetType());

            foreach (var glyph in Glyphs)
            {
                var clone = (AbstractGlyph) glyph.Clone();
                clone.Selected = false;
                tmp.Add(clone);
            }

            return tmp;
        }
        #endregion
    }
}