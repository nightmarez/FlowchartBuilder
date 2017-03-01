// <copyright file="Point3D.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-23</date>
// <summary>Точка 3D.</summary>

namespace Makarov.Framework.Graphics
{
    /// <summary>
    /// Точка 3D.
    /// </summary>
    public struct Point3D
    {
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="z">Координата Z.</param>
        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
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
    }
}