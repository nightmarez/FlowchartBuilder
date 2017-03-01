// <copyright file="LinkPoint.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-21</date>
// <summary>Точки соединения.</summary>

using System;
using System.Drawing;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Точка соединения.
    /// </summary>
    public struct LinkPoint
    {
        #region Types
        /// <summary>
        /// Тип точки соединения.
        /// </summary>
        [Flags]
        public enum LinkPointType
        {
            /// <summary>
            /// Входящая.
            /// </summary>
            In = 1,

            /// <summary>
            /// Исходящая.
            /// </summary>
            Out = 2,
        }
        #endregion

        #region Private members
        /// <summary>
        /// Тип.
        /// </summary>
        private readonly LinkPointType _type;

        /// <summary>
        /// Координаты в миллиметрах.
        /// </summary>
        private PointF _coords;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="coords">Координаты в миллиметрах.</param>
        public LinkPoint(PointF coords)
        {
            _coords = coords;
            _type = LinkPointType.In | LinkPointType.Out;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="coords">Координаты в миллиметрах.</param>
        /// <param name="type">Тип.</param>
        public LinkPoint(PointF coords, LinkPointType type)
        {
            _coords = coords;
            _type = type;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Координаты в миллиметрах.
        /// </summary>
        public PointF Coords
        {
            get { return _coords; }
            set { _coords = value; }
        }

        /// <summary>
        /// Координата X в миллиметрах.
        /// </summary>
        public float XInMM
        {
            get { return Coords.X; }
            set { Coords = new PointF(value, Coords.Y); }
        }

        /// <summary>
        /// Координата Y в миллиметрах.
        /// </summary>
        public float YInMM
        {
            get { return Coords.Y; }
            set { Coords = new PointF(Coords.X, value); }
        }

        /// <summary>
        /// Тип.
        /// </summary>
        public LinkPointType PointType
        {
            get { return _type; }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Преобразование в точку.
        /// </summary>
        public static explicit operator PointF(LinkPoint pt)
        {
            return pt.Coords;
        }
        #endregion
    }
}