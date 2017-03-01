// <copyright file="Pie.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-10-05</date>
// <summary>Полукруг.</summary>

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Makarov.Framework.Graphics.Primitives
{
    /// <summary>
    /// Полукруг.
    /// </summary>
    public class Pie : Primitive
    {
        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        /// <param name="startAngle">Начальный угол.</param>
        /// <param name="angle">Угол.</param>
        public Pie(int x, int y, int width, int height, int startAngle, int angle)
            : base(x, y, width, height)
        {
            StartAngle = startAngle;
            Angle = angle;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="coords">Координаты.</param>
        /// <param name="size">Размеры.</param>
        /// <param name="startAngle">Начальный угол.</param>
        /// <param name="angle">Угол.</param>
        public Pie(Point coords, Size size, int startAngle, int angle)
            : base(coords.X, coords.Y, size.Width, size.Height)
        {
            StartAngle = startAngle;
            Angle = angle;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="rect">Размеры и координаты.</param>
        /// <param name="startAngle">Начальный угол.</param>
        /// <param name="angle">Угол.</param>
        public Pie(System.Drawing.Rectangle rect, int startAngle, int angle)
            : base(rect.X, rect.Y, rect.Width, rect.Height)
        {
            StartAngle = startAngle;
            Angle = angle;
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
            path.AddPie(x, y, width, height, StartAngle, Angle);
            path.CloseFigure();

            return path;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Начальный угол.
        /// </summary>
        public int StartAngle
        {
            get;
            set;
        }

        /// <summary>
        /// Угол.
        /// </summary>
        public int Angle
        {
            get;
            set;
        }
        #endregion
    }
}