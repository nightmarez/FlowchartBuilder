// <copyright file="Renderer.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-23</date>
// <summary>Рендерер.</summary>

using System;
using System.Drawing;
using System.Windows.Forms;
using Makarov.Framework.Core;

namespace Makarov.Framework.Graphics
{
    /// <summary>
    /// Рендерер.
    /// </summary>
    public class Renderer : IDisposable
    {
        #region Exceptions
        /// <summary>
        /// Исключение рендерера.
        /// </summary>
        public abstract class RendererException : MakarovFrameworkException
        {
            /// <param name="message">Сообщение.</param>
            protected RendererException(string message)
                : base(message ?? string.Empty)
            { }
        }

        /// <summary>
        /// Рендерер уже освобождён.
        /// </summary>
        public sealed class RendererAlreadyDisposedException : RendererException
        {
            public RendererAlreadyDisposedException()
                : base(@"Renderer already disposed.")
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Окно вывода графики.
        /// </summary>
        private readonly Form _wnd;

        /// <summary>
        /// Освобождён ли рендерер.
        /// </summary>
        private bool _disposed;
        #endregion

        #region Constructors
        /// <param name="window">Окно вывода графики.</param>
        public Renderer(Form window)
        {
            _wnd = window;

            _wnd.Resize += (sender, e) => Redraw();
            _wnd.Paint += (sender, e) => Redraw();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Окно вывода графики.
        /// </summary>
        public Form Window
        {
            get
            {
                if (_disposed)
                    throw new RendererAlreadyDisposedException();

                return _wnd;
            }
        }

        /// <summary>
        /// Ширина области вывода графики.
        /// </summary>
        public int Width
        {
            get
            {
                if (_disposed)
                    throw new RendererAlreadyDisposedException();

                return _wnd.ClientSize.Width;
            }
        }

        /// <summary>
        /// Высота области вывода графики.
        /// </summary>
        public int Height
        {
            get
            {
                if (_disposed)
                    throw new RendererAlreadyDisposedException();

                return _wnd.ClientSize.Height;
            }
        }
        #endregion

        #region Public methods
        public void Dispose()
        {
            if (_disposed)
                throw new RendererAlreadyDisposedException();

            _disposed = true;
        }

        /// <summary>
        /// Перерисовать графику.
        /// </summary>
        public virtual void Redraw()
        {
            using (System.Drawing.Graphics gfx = _wnd.CreateGraphics())
                gfx.Clear(Color.Blue);
        }
        #endregion
    }
}