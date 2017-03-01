// <copyright file="Point4D.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-23</date>
// <summary>Точка 4D.</summary>

namespace Makarov.Framework.Graphics
{
    /// <summary>
    /// Точка 4D.
    /// </summary>
    public struct Point4D
    {
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="z">Координата Z.</param>
        /// <param name="w">Координата W.</param>
        public Point4D(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Координата X.
        /// </summary>
        public float X;

        /// <summary>
        /// Координата Y.
        /// </summary>
        public float Y;

        /// <summary>
        /// Координата Z.
        /// </summary>
        public float Z;

        /// <summary>
        /// Координата W;
        /// </summary>
        public float W;

        /// <summary>
        /// Нормализует точку по W.
        /// </summary>
        public Point4D Normalize()
        {
            return new Point4D(X/W, Y/W, Z/W, 1f);
        }
    }
}