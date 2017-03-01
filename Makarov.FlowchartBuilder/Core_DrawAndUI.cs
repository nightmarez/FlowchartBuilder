// <copyright file="Core_DrawAndUI.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-01-02</date>
// <summary>Кора (Синглетон).</summary>

using System;
using System.Drawing;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Forms;
using Makarov.FlowchartBuilder.Sheets;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Кора (Синглетон).
    /// </summary>
    public sealed partial class Core
    {
        /// <summary>
        /// Обновляет расположение скроллбаров.
        /// </summary>
        /// <param name="wndMain">Главное окно.</param>
        /// <returns>Смещение листа.</returns>
        private Point UpdateScrollbars(MainForm wndMain)
        {
            // Скроллбары.
            var hbar = wndMain.HorizontalScrollBar;
            var vbar = wndMain.VerticalScrollBar;

            // Толщина линеек.
            var rulersBorderWidth = (int)Math.Ceiling(
                Settings.Environment.Rulers
                    ? Settings.Pens.RulersBorder.Width
                    : 0);

            // Обновляем расположение скроллбаров.
            if (IsDocumentsExists && CurrentDocument.DocumentSheet is FixedSheet)
            {
                // Лист.
                var sheet = (FixedSheet)CurrentDocument.DocumentSheet;

                // Прямоугольник, в который производится отрисовка.
                var rect = wndMain.InnerRectangle;

                // Была ли изменена видимость линеек.
                bool rulersVisibleChanged = false;

                // Прямоугольник, в который производится отрисовка с
                // учётом толщины линеек.
                rect = new Rectangle(
                    rect.Left + rulersBorderWidth,
                    rect.Top + rulersBorderWidth,
                    rect.Width - rulersBorderWidth,
                    rect.Height - rulersBorderWidth);

                // Видимость горизонтального скроллбара.
                if (sheet.ScaledWidth.MMToPx() > rect.Width)
                {
                    if (!hbar.Visible)
                    {
                        hbar.Visible = true;
                        rulersVisibleChanged = true;
                    }
                }
                else
                {
                    if (hbar.Visible)
                    {
                        hbar.Visible = false;
                        rulersVisibleChanged = true;
                    }
                }

                // Видимость вертикального скроллбара.
                if (sheet.ScaledHeight.MMToPx() > rect.Height)
                {
                    if (!vbar.Visible)
                    {
                        vbar.Visible = true;
                        rulersVisibleChanged = true;
                    }

                }
                else
                {
                    if (vbar.Visible)
                    {
                        vbar.Visible = false;
                        rulersVisibleChanged = true;
                    }
                }

                // Параметры горизонтального скроллбара.
                if (hbar.Visible)
                {
                    hbar.Left = rect.Left;
                    hbar.Top = rect.Top + rect.Height - hbar.Height;
                    hbar.Width = rect.Width - (vbar.Visible ? vbar.Width : 0);

                    int dx = sheet.ScaledWidth.MMToPx() - rect.Width;

                    if (hbar.Maximum != dx)
                    {
                        hbar.Minimum = 0;
                        hbar.Maximum = dx;
                        hbar.SmallChange = dx / 10;
                        hbar.LargeChange = dx / 10;
                        hbar.Value = dx >> 1;
                    }
                }

                // Параметры вертикального скроллбара.
                if (vbar.Visible)
                {
                    vbar.Left = rect.Left + rect.Width - vbar.Width;
                    vbar.Top = rect.Top;
                    vbar.Height = rect.Height - (hbar.Visible ? hbar.Height : 0);

                    int dy = sheet.ScaledHeight.MMToPx() - rect.Height;

                    if (vbar.Maximum != dy)
                    {
                        vbar.Minimum = 0;
                        vbar.Maximum = dy;
                        vbar.SmallChange = dy / 10;
                        vbar.LargeChange = dy / 10;
                        vbar.Value = dy;
                    }
                }

                // Если изменена видимость скроллбаров,
                // заапдейтить расположение дочерних окон.
                if (rulersVisibleChanged)
                {
                    // Корректируем положение дочерних окон.
                    foreach (var wnd in Windows.Windows)
                        if (wnd is DockingForm)
                            ((DockingForm)wnd).UpdateDockedWindowPosition();
                }
            }

            // Смещение листа, заданное скроллбарами.
            return new Point(
                hbar.Visible ? hbar.Value - (hbar.Maximum >> 1) : 0,
                vbar.Visible ? vbar.Value - (vbar.Maximum >> 1) : 0);
        }

        /// <summary>
        /// Отрисовывает линейки.
        /// </summary>
        /// <param name="gfx">Graphics.</param>
        /// <param name="clientRect">Область графикса, в которой идёт отрисовка.</param>
        /// <param name="leftOffset">Смещение начала измерения линейки по горизонтали.</param>
        /// <param name="topOffset">Смещение начала измерения линейки по вертикали.</param>
        private void DrawRulers(Graphics gfx, Rectangle clientRect,
            float leftOffset, float topOffset)
        {
            if (!Settings.Environment.Rulers) return;

            MainForm wndMain = MainWindow;

            // Коэффициент масштабирования.
            float scaleFactor = IsDocumentsExists
                ? CurrentDocument.DocumentSheet.ScaleFactor
                : 1f;

            // Ширина линеек.
            int rulersSize = Settings.Environment.RulersSize;

            using (Brush brush = new SolidBrush(Settings.Colors.RulersBackground))
            {
                gfx.FillRectangle(
                    brush,
                    clientRect.Left - rulersSize,
                    clientRect.Top - rulersSize,
                    clientRect.Width + rulersSize,
                    Settings.Environment.RulersSize
                    );

                gfx.FillRectangle(
                    brush,
                    clientRect.Left - rulersSize,
                    clientRect.Top - rulersSize,
                    Settings.Environment.RulersSize,
                    clientRect.Height + rulersSize
                    );
            }

            gfx.DrawRectangle(
                Settings.Pens.RulersBorder,
                clientRect.Left - rulersSize,
                clientRect.Top - rulersSize,
                clientRect.Width + rulersSize,
                Settings.Environment.RulersSize
                );

            gfx.DrawRectangle(
                Settings.Pens.RulersBorder,
                clientRect.Left - rulersSize,
                clientRect.Top - rulersSize,
                Settings.Environment.RulersSize,
                clientRect.Height + rulersSize
                );

            using (Font font = "Arial".SafeCreateFont(6))
            {
                Units units = Settings.Environment.CurrentUnits;

                for (int i = 0;
                     UnitsConverter.CurrToPx(i) <= wndMain.ClientSize.Width - leftOffset;
                     i += units.SmallStep)
                {
                    int len;
                    if (i % units.BigStep == 0)
                    {
                        len = rulersSize >> 1;
                        gfx.DrawString((i / scaleFactor).ToString("F1"), font, Brushes.Black,
                                        leftOffset + UnitsConverter.CurrToPx(i) + 1,
                                        clientRect.Top - rulersSize + (rulersSize >> 2));
                    }
                    else
                        len = rulersSize >> 2;

                    gfx.DrawLine(
                        Pens.Black,
                        leftOffset + UnitsConverter.CurrToPx(i), clientRect.Top - rulersSize,
                        leftOffset + UnitsConverter.CurrToPx(i), clientRect.Top - rulersSize + len);
                }

                for (int i = 0;
                     UnitsConverter.CurrToPx(i) <= wndMain.ClientSize.Height - topOffset;
                     i += units.SmallStep)
                {
                    int len;
                    if (i % units.BigStep == 0)
                    {
                        len = rulersSize >> 1;
                        gfx.DrawString((i / scaleFactor).ToString("F1"), font, Brushes.Black,
                                        clientRect.Left - rulersSize + (rulersSize >> 2),
                                        topOffset + UnitsConverter.CurrToPx(i) + 1);
                    }
                    else
                        len = rulersSize >> 2;

                    gfx.DrawLine(
                        Pens.Black,
                        clientRect.Left - rulersSize, topOffset + UnitsConverter.CurrToPx(i),
                        clientRect.Left - rulersSize + len, topOffset + UnitsConverter.CurrToPx(i));
                }

                using (Brush brush = new SolidBrush(Settings.Colors.RulersBackground))
                {
                    gfx.FillRectangle(
                        brush,
                        clientRect.Left - rulersSize,
                        clientRect.Top - rulersSize,
                        Settings.Environment.RulersSize,
                        Settings.Environment.RulersSize
                        );
                }

                gfx.DrawRectangle(
                    Settings.Pens.RulersBorder,
                    clientRect.Left - rulersSize,
                    clientRect.Top - rulersSize,
                    Settings.Environment.RulersSize,
                    Settings.Environment.RulersSize
                    );

                gfx.DrawString(
                    units.ShortName,
                    font,
                    Brushes.Black,
                    clientRect.Left - rulersSize + 1,
                    clientRect.Top - rulersSize + 1);
            }
        }
    }
}