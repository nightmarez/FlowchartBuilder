// <copyright file="DontRedraw.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-06-01</date>
// <summary>Класс, временно отключающий перерисовку главного окна.</summary>

using System;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Класс, временно отключающий перерисовку главного окна.
    /// </summary>
    public sealed class DontRedraw : IDisposable
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public DontRedraw()
        {
            // Отключаем перерисовку главного окна.
            Core.Instance.NeedRedraw = false;

            // Объект не освобождён.
            _disposed = false;
        }

        /// <summary>
        /// Включает перерисовку главного окна.
        /// </summary>
        public void Dispose()
        {
            // Если объект уже освобождён, выходим.
            if (_disposed) return;

            // Включаем перерисовку главного окна.
            Core.Instance.NeedRedraw = true;

            // Отмечаем, что объект уже освобождён.
            _disposed = true;
        }

        /// <summary>
        /// Освобождён ли объект.
        /// </summary>
        private bool _disposed;
    }
}