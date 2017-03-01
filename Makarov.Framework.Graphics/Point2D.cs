// <copyright file="Point2D.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-23</date>
// <summary>Точка 2D.</summary>

using System.Drawing;

namespace Makarov.Framework.Graphics
{
    /// <summary>
    /// Точка 2D.
    /// </summary>
    public struct Point2D
    {
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public Point2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Координата X.
        /// </summary>
        public float X;

        /// <summary>
        /// Координата Y.
        /// </summary>
        public float Y;

        public static implicit operator Point2D (Point pt)
        {
            return new Point2D(pt.X, pt.Y);
        }

        public static implicit operator PointF(Point2D pt)
        {
            return new PointF(pt.X, pt.Y);
        }

        public static explicit operator Point (Point2D pt)
        {
            return new Point((int)pt.X, (int)pt.Y);
        }
    }
}