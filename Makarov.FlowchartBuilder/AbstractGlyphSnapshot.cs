// <copyright file="AbstractGlyphSnapshot.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-09</date>
// <summary>Абстрактный класс снимка глифа.</summary>

using System;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Абстрактный класс снимка глифа.
    /// </summary>
    public abstract class AbstractGlyphSnapshot
    {
        #region Exceptions
        /// <summary>
        /// Базовое исключение снимка глифа.
        /// </summary>
        public abstract class GlyphSnapshotException : FlowchartBuilderException
        {
            /// <param name="message">Сообщение.</param>
            protected GlyphSnapshotException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Пустое имя свойства.
        /// </summary>
        public sealed class EmptyPropertyName : GlyphSnapshotException
        {
            public EmptyPropertyName()
                : base(@"Empty property name.")
            { }
        }

        /// <summary>
        /// Свойство с заданным именем не найдено.
        /// </summary>
        public sealed class PropertyNotExistsException : GlyphSnapshotException
        {
            /// <param name="propertyName">Сообщение.</param>
            public PropertyNotExistsException(string propertyName)
                : base(string.Format("Property '{0}' not found.", propertyName ?? string.Empty))
            { }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Идентификатор глифа.
        /// </summary>
        public string GlyphId { get; protected set; }

        /// <summary>
        /// Тип глифа.
        /// </summary>
        public string GlyphType { get; protected set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Возвращает тип глифа.
        /// </summary>
        Type GetGlyphType()
        {
            return Type.GetType(GlyphType);
        }

        /// <summary>
        /// Существует ли заданный тип глифа.
        /// </summary>
        public bool IsGlyphTypeExists()
        {
            return GetGlyphType() != null;
        }
        #endregion
    }
}
