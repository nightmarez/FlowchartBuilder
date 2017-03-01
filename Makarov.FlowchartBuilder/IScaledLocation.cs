// <copyright file="IScaledLocation.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-18</date>
// <summary>Интерфейс объекта, имеющего масштабированные координаты.</summary>

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Интерфейс объекта, имеющего масштабированные координаты.
    /// </summary>
    public interface IScaledLocation
    {
        /// <summary>
        /// Масштабированная координата X в миллиметрах.
        /// </summary>
        float ScaledX { get; set; }

        /// <summary>
        /// Масштабированная координата Y в миллиметрах.
        /// </summary>
        float ScaledY { get; set; }
    }
}