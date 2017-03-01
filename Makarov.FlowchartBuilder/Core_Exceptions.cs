// <copyright file="Core_Exceptions.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-01-02</date>
// <summary>Кора (Синглетон).</summary>

using System;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Кора (Синглетон).
    /// </summary>
    public sealed partial class Core
    {
        /// <summary>
        /// Исключение коры.
        /// </summary>
        public abstract class CoreException : FlowchartBuilderException
        {
            /// <param name="message">Сообщение.</param>
            protected CoreException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Кора уже создана.
        /// </summary>
        public sealed class CoreAlreadyExistsException : SingletonObjectAlreayExistsException
        {
            public CoreAlreadyExistsException()
                : base("Core already exists.")
            { }
        }

        /// <summary>
        /// Кора уже освобождена.
        /// </summary>
        public sealed class CoreAlreadyDisposedException : CoreException
        {
            public CoreAlreadyDisposedException()
                : base("Core already disposed.")
            { }
        }

        /// <summary>
        /// Текущий документ не существует.
        /// </summary>
        public sealed class CurrentDocumentNotExistsException : CoreException
        {
            public CurrentDocumentNotExistsException()
                : base("Current document not exists.")
            { }
        }

        /// <summary>
        /// Тип окна не найден.
        /// </summary>
        public sealed class WindowTypeNotFoundException : CoreException
        {
            /// <param name="type">Тип окна.</param>
            public WindowTypeNotFoundException(string type)
                : base(string.Format(@"Window type '{0}' not found.", type ?? string.Empty))
            { }

            /// <param name="type">Тип окна.</param>
            public WindowTypeNotFoundException(Type type)
                : this(type.Name)
            { }
        }

        /// <summary>
        /// Нельзя использовать главное окно как дочернее.
        /// </summary>
        public sealed class CantUseMainWindowAsChildException : CoreException
        {
            public CantUseMainWindowAsChildException()
                : base(@"Can't use main window as child window.")
            { }
        }
    }
}