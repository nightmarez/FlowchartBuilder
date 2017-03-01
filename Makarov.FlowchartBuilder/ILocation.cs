// <copyright file="ILocation.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-18</date>
// <summary>Интерфейс объекта, имеющего координаты.</summary>

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Интерфейс объекта, имеющего координаты.
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// Координата X в миллиметрах.
        /// </summary>
        float X { get; set; }

        /// <summary>
        /// Координата Y в миллиметрах.
        /// </summary>
        float Y { get; set; }
    }
}