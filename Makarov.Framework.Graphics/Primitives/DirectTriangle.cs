// <copyright file="DirectTriangle.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-10-05</date>
// <summary>Прямоугольный треугольник.</summary>

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Makarov.Framework.Graphics.Primitives
{
    /// <summary>
    /// Прямоугольный треугольник.
    /// </summary>
    public class DirectTriangle : Primitive
    {
        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        public DirectTriangle(int x, int y, int width, int height)
            : base(x, y, width, height)
        { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="coords">Координаты.</param>
        /// <param name="size">Размеры.</param>
        public DirectTriangle(Point coords, Size size)
            : base(coords.X, coords.Y, size.Width, size.Height)
        { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="rect">Размеры и координаты.</param>
        public DirectTriangle(System.Drawing.Rectangle rect)
            : base(rect.X, rect.Y, rect.Width, rect.Height)
        { }
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
            path.AddLine(x, y, x + width, y + height);
            path.AddLine(x + width, y + height, x, y + height);
            path.AddLine(x, y + height, x, y);
            path.CloseFigure();

            return path;
        }
        #endregion
    }
}