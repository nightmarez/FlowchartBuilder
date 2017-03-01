// <copyright file="Vertex.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-23</date>
// <summary>Вершина.</summary>

namespace Makarov.Framework.Graphics
{
    public struct Vertex
    {
        public Vertex(Point3D coords, ColorF color, Point3D normal)
        {
            Coords = coords;
            Color = color;
            Normal = normal;
        }

        public Point3D Coords;
        public ColorF Color;
        public Point3D Normal;
    }
}