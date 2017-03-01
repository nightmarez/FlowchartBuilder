// <copyright file="BitmapExtensions.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-29</date>
// <summary>Расширитель класса Bitmap.</summary>

using System.Drawing;

namespace Makarov.FlowchartBuilder.Extensions
{
    /// <summary>
    /// Расширитель класса Bitmap.
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// Рисует рамку.
        /// </summary>
        /// <param name="bmp">Битмап.</param>
        /// <param name="color">Цвет.</param>
        public static void DrawBorder(this Bitmap bmp, Color color)
        {
            using (Graphics gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(color))
                gfx.DrawRectangle(pen, 0, 0, bmp.Width - 1, bmp.Height - 1);
        }
    }
}