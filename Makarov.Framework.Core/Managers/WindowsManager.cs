// <copyright file="WindowsManager.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-01</date>
// <summary>Менеджер окон.</summary>

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Makarov.Framework.Core.Managers
{
    /// <summary>
    /// Менеджер окон.
    /// </summary>
    public sealed class WindowsManager : IDisposable
    {
        #region Exceptions
        /// <summary>
        /// Базовое исключение менеджера окон.
        /// </summary>
        public abstract class WindowsManagerException : MakarovFrameworkException
        {
            protected WindowsManagerException(string message)
                : base(message ?? string.Empty)
            { }
        }

        /// <summary>
        /// Менеджер окон уже освобождён.
        /// </summary>
        public sealed class WindowsManagerAlreadyDisposedException : WindowsManagerException
        {
            public WindowsManagerAlreadyDisposedException()
                : base(@"Windows manager already disposed exception.")
            { }
        }

        /// <summary>
        /// Тип окна не найден.
        /// </summary>
        public sealed class WindowTypeNotFoundException : WindowsManagerException
        {
            /// <param name="type">Тип окна.</param>
            public WindowTypeNotFoundException(Type type)
                : base(string.Format(@"Window type '{0}' not found.", type.Name))
            { }
        }
        #endregion

        #region Private members

        /// <summary>
        /// Освобождён ли менеджер окон.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Типы окон.
        /// </summary>
        private List<Type> _formTypes;

        /// <summary>
        /// Экземпляры окон.
        /// </summary>
        private List<Form> _windows;
        #endregion

        #region Constructors
        /// <param name="assembly">Сборка, в которой нужно искать классы окон.</param>
        public WindowsManager(Assembly assembly)
        {
            _formTypes = new List<Type>();
            _windows = new List<Form>();

            foreach (Type type in assembly.GetTypes())
                if (type.IsClass && type.IsSubclassOf(typeof(Form)))
                    _formTypes.Add(type);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Типы окон.
        /// </summary>
        public IEnumerable<Type> Forms
        {
            get
            {
                if (_disposed)
                    throw new WindowsManagerAlreadyDisposedException();

                return _formTypes;
            }
        }

        /// <summary>
        /// Экземпляры окон.
        /// </summary>
        public IEnumerable<Form> Windows
        {
            get
            {
                if (_disposed)
                    throw new WindowsManagerAlreadyDisposedException();

                return _windows;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Возвращает окно заданного типа.
        /// </summary>
        public T GetWindow<T>() where T: Form
        {
            if (_disposed)
                throw new WindowsManagerAlreadyDisposedException();

            foreach (Form form in _windows)
                if (form is T)
                    return (T) form;

            foreach (Type type in _formTypes)
                if (type == typeof(T))
                {
                    _formTypes.Remove(type);
                    var frm = (T) Activator.CreateInstance(type);
                    _windows.Add(frm);
                    return frm;
                }

            throw new WindowTypeNotFoundException(typeof (T));
        }

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                throw new WindowsManagerAlreadyDisposedException();

            foreach (Form wnd in _windows)
                wnd.Dispose();

            _windows.Clear();
            _windows = null;

            _formTypes.Clear();
            _formTypes = null;

            _disposed = true;
        }
        #endregion
    }
}