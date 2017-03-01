// <copyright file="HistoryEvent.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-XII-16</date>
// <summary>Событие истории.</summary>

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Событие истории.
    /// </summary>
    public abstract class HistoryEvent
    {
        #region Exceptions
        /// <summary>
        /// Исключение события истории.
        /// </summary>
        public abstract class HistoryEventException : FlowchartBuilderException
        {
            /// <param name="message">Сообщение.</param>
            protected HistoryEventException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Пустой идентификатор глифа.
        /// </summary>
        public sealed class EmptyGlyphIdException : HistoryEventException
        {
            public EmptyGlyphIdException()
                : base(@"Empty glyph Id.")
            { }
        }

        /// <summary>
        /// Пустое имя свойства.
        /// </summary>
        public sealed class EmptyPropertyName : HistoryEventException
        {
            public EmptyPropertyName()
                : base(@"Empty property name.")
            { }
        }

        /// <summary>
        /// Свойство с заданным именем не найдено.
        /// </summary>
        public sealed class PropertyNotExistsException : HistoryEventException
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
        #endregion

        #region Constructors
        /// <param name="glyphId">Идентификатор глифа.</param>
        protected HistoryEvent(string glyphId)
        {
            if (string.IsNullOrWhiteSpace(glyphId))
                throw new EmptyGlyphIdException();

            GlyphId = glyphId;
        }
        #endregion
    }
}