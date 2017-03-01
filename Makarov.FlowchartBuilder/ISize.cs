// <copyright file="ISize.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-18</date>
// <summary>Интерфейс размерного объекта.</summary>

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Интерфейс размерного объекта.
    /// </summary>
    public interface ISize
    {
        /// <summary>
        /// Ширина в миллиметрах.
        /// </summary>
        float Width { get; set; }

        /// <summary>
        /// Высота в миллиметрах.
        /// </summary>
        float Height { get; set; }
    }
}