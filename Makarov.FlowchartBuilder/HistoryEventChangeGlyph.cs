// <copyright file="HistoryEventChangeGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-XII-16</date>
// <summary>Событие истории - изменение глифа.</summary>

using System.Collections.Generic;
using System.Linq;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Событие истории - изменение глифа.
    /// </summary>
    public sealed class HistoryEventChangeGlyph : HistoryEvent
    {
        #region Private members
        /// <summary>
        /// (имя свойства, (старое значение, новое значение)).
        /// </summary>
        private readonly Dictionary<string, KeyValuePair<string, string>> _props =
            new Dictionary<string, KeyValuePair<string, string>>();
        #endregion

        #region Constructors
        /// <param name="glyphId">Идентификатор глифа.</param>
        /// <param name="properties">Свойства.</param>
        public HistoryEventChangeGlyph(string glyphId,
            IEnumerable<KeyValuePair<string, KeyValuePair<string, string>>> properties)
            : base(glyphId)
        {
            if (properties != null)
                foreach (KeyValuePair<string, KeyValuePair<string, string>> prop in properties)
                    _props.Add(prop.Key, prop.Value);
        }

        public HistoryEventChangeGlyph(GlyphSnapshotsDiff diff)
            : this(diff.GlyphId, diff.Pairs)
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
        /// Новые пары (имя свойства, значение).
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> NewPairs
        {
            get { return _props.Select(prop => new KeyValuePair<string, string>(prop.Key, prop.Value.Value)); }
        }

        /// <summary>
        /// Старые пары (имя свойства, значение).
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> OldPairs
        {
            get { return _props.Select(prop => new KeyValuePair<string, string>(prop.Key, prop.Value.Key)); }
        }

        /// <summary>
        /// Количество свойств.
        /// </summary>
        public int Count
        {
            get { return _props.Count; }
        }

        /// <summary>
        /// Возвращает значения свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Пара (старое значение, новое значение).</returns>
        public KeyValuePair<string, string> this[string propertyName]
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
        /// Возвращает значения свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Пара (старое значение, новое значение).</returns>
        public KeyValuePair<string, string> GetProperty(string propertyName)
        {
            return this[propertyName];
        }
        #endregion
    }
}