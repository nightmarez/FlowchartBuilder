// <copyright file="Primitive.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-10-05</date>
// <summary>Примитив.</summary>

using System.Drawing;
using System.Drawing.Drawing2D;
using Makarov.Framework.Core;

namespace Makarov.Framework.Graphics.Primitives
{
    /// <summary>
    /// Примитив.
    /// </summary>
    public abstract class Primitive
    {
        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        protected Primitive(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="coords">Координаты.</param>
        /// <param name="size">Размеры.</param>
        protected Primitive(Point coords, Size size)
            : this(coords.X, coords.Y, size.Width, size.Height)
        { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="rect">Размеры и координаты.</param>
        protected Primitive(System.Drawing.Rectangle rect)
            : this(rect.X, rect.Y, rect.Width, rect.Height)
        { }
        #endregion

        #region Exceptions
        /// <summary>
        /// Неправильная ширина.
        /// </summary>
        public class InvalidPrimitiveWidthException : MakarovFrameworkException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="width">Ширина.</param>
            public InvalidPrimitiveWidthException(int width)
                : base(string.Format("Invalid width = '{0}'", width))
            { }
        }

        /// <summary>
        /// Неправильная высота.
        /// </summary>
        public class InvalidPrimitiveHeightException : MakarovFrameworkException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="height">Высота.</param>
            public InvalidPrimitiveHeightException(int height)
                : base(string.Format("Invalid height = '{0}'", height))
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Ширина.
        /// </summary>
        private int _width;

        /// <summary>
        /// Высота.
        /// </summary>
        private int _height;
        #endregion

        #region Properties
        /// <summary>
        /// Координата X.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата Y.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Ширина.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set
            {
                if (value <= 0)
                    throw new InvalidPrimitiveWidthException(value);

                _width = value;
            }
        }

        /// <summary>
        /// Высота.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set
            {
                if (value <= 0)
                    throw new InvalidPrimitiveHeightException(value);

                _height = value;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Создать путь.
        /// </summary>
        public GraphicsPath Create()
        {
            return CreatePath(X, Y, Width, Height);
        }

        /// <summary>
        /// Создать путь.
        /// </summary>
        public GraphicsPath CreateCentered()
        {
            return CreatePath(X - (Width >> 1), Y - (Height >> 1), Width, Height);
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
        protected abstract GraphicsPath CreatePath(int x, int y, int width, int height);
        #endregion
    }
}