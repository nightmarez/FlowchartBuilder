// <copyright file="IScaledSize.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-18</date>
// <summary>Интерфейс масштабируемого размерного объекта.</summary>

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Интерфейс масштабируемого размерного объекта.
    /// </summary>
    public interface IScaledSize
    {
        /// <summary>
        /// Масштабированная ширина в миллиметрах.
        /// </summary>
        float ScaledWidth { get; set; }

        /// <summary>
        /// Масштабированная высота в миллиметрах.
        /// </summary>
        float ScaledHeight { get; set; }
    }
}