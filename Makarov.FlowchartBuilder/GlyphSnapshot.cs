// <copyright file="GlyphSnapshot.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-06</date>
// <summary>Снимок глифа.</summary>

using System.Collections.Generic;
using System.Linq;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Снимок глифа.
    /// </summary>
    public sealed class GlyphSnapshot : AbstractGlyphSnapshot
    {
        #region Exceptions
        /// <summary>
        /// Попытка сохранить значение свойства, имя которого уже есть в словаре.
        /// </summary>
        public sealed class PropertyAlreadyExistsException : GlyphSnapshotException
        {
            /// <param name="propertyName">Имя свойства.</param>
            public PropertyAlreadyExistsException(string propertyName)
                : base(string.Format("Property '{0}' already exists.", propertyName ?? string.Empty))
            { }
        }
        #endregion

        #region Constructors
        /// <param name="glyph">Глиф.</param>
        public GlyphSnapshot(AbstractGlyph glyph)
        {
            // Получаем идентификатор глифа.
            GlyphId = glyph.Id;

            // Получаем тип глифа.
            GlyphType = glyph.GetType().FullName;

            // Проходим по активным свойствам глифа и сохраняем их значения...
            foreach (KeyValuePair<string, string> kvp in glyph.SerializeProperties())
                AddProperty(kvp.Key, kvp.Value);
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
        /// Находит различие между глифами.
        /// </summary>
        public GlyphSnapshotsDiff Diff(GlyphSnapshot otherSnapshot)
        {
            return new GlyphSnapshotsDiff(this, otherSnapshot);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Находит различие между глифами.
        /// </summary>
        public static GlyphSnapshotsDiff operator -(GlyphSnapshot glyphSnapshot1, GlyphSnapshot glyphSnapshot2)
        {
            return new GlyphSnapshotsDiff(glyphSnapshot1, glyphSnapshot2);
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