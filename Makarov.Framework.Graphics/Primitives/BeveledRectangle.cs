// <copyright file="BeveledRectangle.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-06-12</date>
// <summary>Скошеный прямоугольник.</summary>

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Makarov.Framework.Graphics.Primitives
{
    /// <summary>
    /// Скошеный прямоугольник.
    /// </summary>
    public class BeveledRectangle : Primitive
    {
        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        /// <param name="offsetX">Смещение скоса по X.</param>
        public BeveledRectangle(int x, int y, int width, int height, int offsetX)
            : base(x, y, width, height)
        {
            OffsetX = offsetX;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="coords">Координаты.</param>
        /// <param name="size">Размеры.</param>
        /// <param name="offsetX">Смещение скоса по X.</param>
        public BeveledRectangle(Point coords, Size size, int offsetX)
            : base(coords.X, coords.Y, size.Width, size.Height)
        {
            OffsetX = offsetX;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="rect">Размеры и координаты.</param>
        /// <param name="offsetX">Смещение скоса по X.</param>
        public BeveledRectangle(System.Drawing.Rectangle rect, int offsetX)
            : base(rect.X, rect.Y, rect.Width, rect.Height)
        {
            OffsetX = offsetX;
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Создать путь.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        /// <returns>Путь.</returns>
        protected override GraphicsPath CreatePath(int x, int y, int width, int height)
        {
            var path = new GraphicsPath();

            path.StartFigure();
            path.AddLine(x + OffsetX, y, x + width, y);
            path.AddLine(x + width, y, x + width - OffsetX, y + height);
            path.AddLine(x + width - OffsetX, y + height, x, y + height);
            path.AddLine(x, y + height, x + OffsetX, y);
            path.CloseFigure();

            return path;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Смещение скоса по X.
        /// </summary>
        public int OffsetX
        {
            get; set;
        }
        #endregion
    }
}