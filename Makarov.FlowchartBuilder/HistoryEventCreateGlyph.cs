// <copyright file="HistoryEventCreateGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-XII-16</date>
// <summary>Событие истории - создание глифа.</summary>

using System.Collections.Generic;
using System.Linq;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Событие истории - создание глифа.
    /// </summary>
    public sealed class HistoryEventCreateGlyph : HistoryEvent
    {
        #region Exceptions
        /// <summary>
        /// Попытка сохранить значение свойства, имя которого уже есть в словаре.
        /// </summary>
        public sealed class PropertyAlreadyExistsException : HistoryEventException
        {
            /// <param name="propertyName">Имя свойства.</param>
            public PropertyAlreadyExistsException(string propertyName)
                : base(string.Format("Property '{0}' already exists.", propertyName ?? string.Empty))
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Свойства глифа.
        /// </summary>
        private readonly Dictionary<string, string> _props = new Dictionary<string, string>();
        #endregion

        #region Properties
        /// <summary>
        /// Тип глифа.
        /// </summary>
        public string GlyphType { get; private set; }
        #endregion

        #region Constructors
        /// <param name="glyphType">Тип глифа.</param>
        /// <param name="glyphId">Идентификатор глифа.</param>
        /// <param name="properties">Пары (имя свойства, значение).</param>
        public HistoryEventCreateGlyph(string glyphType, string glyphId, 
            IEnumerable<KeyValuePair<string, string>> properties)
            : base(glyphId)
        {
            GlyphType = glyphType;

            if (properties != null)
                foreach (KeyValuePair<string, string> pair in properties)
                    AddProperty(pair.Key, pair.Value);
        }

        /// <param name="snapshot">Снимок глифа.</param>
        public HistoryEventCreateGlyph(GlyphSnapshot snapshot)
            : this(snapshot.GlyphId, snapshot.GlyphType, snapshot.Pairs)
        { }
        #endregion

        #region Properties
        /// <summary>
        /// Свойства.
        /// </summary>
        public IEnumerable<string> Properties
        {
            get { return _props.Select(kvp => kvp.Key); }
        }

        /// <summary>
        /// Пары (имя свойства, значение).
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Pairs
        {
            get { return _props; }
        }

        /// <summary>
        /// Количество свойств.
        /// </summary>
        public int Count
        {
            get { return _props.Count; }
        }

        /// <summary>
        /// Возвращает значение свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        public string this[string propertyName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                    throw new EmptyPropertyName();

                if (!IsPropertyExists(propertyName))
                    throw new PropertyNotExistsException(propertyName);

                return _props[propertyName];
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Есть ли свойство с заданным именем.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        public bool IsPropertyExists(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new EmptyPropertyName();

            return _props.ContainsKey(propertyName);
        }

        /// <summary>
        /// Возвращает значение свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        public string GetProperty(string propertyName)
        {
            return this[propertyName];
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Добавляет свойство.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="propertyValue">Значение свойства.</param>
        private void AddProperty(string propertyName, string propertyValue)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new EmptyPropertyName();

            if (_props.ContainsKey(propertyName))
                throw new PropertyAlreadyExistsException(propertyName);

            _props.Add(propertyName, propertyValue);
        }
        #endregion
    }
}