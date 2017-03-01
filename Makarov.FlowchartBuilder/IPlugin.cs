// <copyright file="IPlugin.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-15</date>
// <summary>Интерфейс плугина.</summary>

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Интерфейс плугина.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Имя плугина.
        /// </summary>
        string Name { get; }
    }
}