// <copyright file="GlyphSnapshotsDiff.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-09</date>
// <summary>Разница между снимками глифа.</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Makarov.FlowchartBuilder.API;
using Makarov.FlowchartBuilder.API.Attributes;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Разница между снимками глифа.
    /// </summary>
    public sealed class GlyphSnapshotsDiff : AbstractGlyphSnapshot
    {
        #region Exceptions
        public abstract class GlyphSnapshotsDiffException : FlowchartBuilderException
        {
            /// <param name="message">Сообщение.</param>
            protected GlyphSnapshotsDiffException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Несовместимые типы глифов.
        /// </summary>
        public sealed class IncompatibleGlyphsException : GlyphSnapshotsDiffException
        {
            /// <param name="type1">Тип первого глифа.</param>
            /// <param name="type2">Тип второго глифа.</param>
            public IncompatibleGlyphsException(string type1, string type2)
                : base(string.Format("Incompatible glyphs: '{0}' and '{1}'.",
                type1 ?? string.Empty, type2 ?? string.Empty))
            { }

            /// <param name="type1">Тип первого глифа.</param>
            /// <param name="type2">Тип второго глифа.</param>
            public IncompatibleGlyphsException(Type type1, Type type2)
                : this(type1.Name, type2.Name)
            { }
        }

        /// <summary>
        /// Различные идентификаторы глифов.
        /// </summary>
        public sealed class DifferentIdsException : GlyphSnapshotsDiffException
        {
            /// <param name="id1">Идентификатор первого глифа.</param>
            /// <param name="id2">Идентификатор второго глифа.</param>
            public DifferentIdsException(string id1, string id2)
                : base(string.Format("Differect glyphs ids: '{0}' and '{1}'.",
                id1 ?? string.Empty, id2 ?? string.Empty))
            { }
        }

        /// <summary>
        /// Неподходящий тип глифа.
        /// </summary>
        public sealed class IncompatibleGlyphType : GlyphSnapshotsDiffException
        {
            /// <param name="requiredType">Какой тип нужен.</param>
            /// <param name="existsType">Какой тип у переданного глифа.</param>
            public IncompatibleGlyphType(string requiredType, string existsType)
                : base(string.Format("Incompatible glyph type '{0}', but required '{1}'.",
                requiredType ?? string.Empty, existsType ?? string.Empty))
            { }

            /// <param name="requiredType">Какой тип нужен.</param>
            /// <param name="existsType">Какой тип у переданного глифа.</param>
            public IncompatibleGlyphType(Type requiredType, Type existsType)
                : this(requiredType.Name, existsType.Name)
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// (имя свойства, (старое значение, новое значение)).
        /// </summary>
        private readonly Dictionary<string, KeyValuePair<string, string>> _props =
            new Dictionary<string, KeyValuePair<string, string>>();
        #endregion

        #region Constructors
        public GlyphSnapshotsDiff(GlyphSnapshot snapshot1, GlyphSnapshot snapshot2)
        {
            // Проверяем совместимость типов.
            if (snapshot1.GlyphType != snapshot2.GlyphType)
                throw new IncompatibleGlyphsException(
                    snapshot1.GlyphType, 
                    snapshot2.GlyphType);

            GlyphType = snapshot1.GlyphType;

            // Проверяем совместимость идентификаторов. Логично сравнивать лишь один и тот же глиф
            // в различные исторические моменты.
            if (snapshot1.GlyphId != snapshot2.GlyphId)
                throw new DifferentIdsException(snapshot1.GlyphId, snapshot2.GlyphId);

            GlyphId = snapshot1.GlyphId;

            // Сохраняем несовпадающие свойства.
            foreach (string prop in snapshot1.Properties)
            {
                string prop1 = snapshot1[prop];
                string prop2 = snapshot2[prop];

                if (prop1 != prop2)
                    _props.Add(prop, new KeyValuePair<string, string>(prop1, prop2));
            }
        }
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
        /// Новые пары (имя свойства, (старое значение, новое значение)).
        /// </summary>
        public IEnumerable<KeyValuePair<string, KeyValuePair<string, string>>> Pairs
        {
            get { return _props; }
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
        /// Задаёт свойства глифу.
        /// </summary>
        /// <param name="glyph">Свойства, которые есть в словаре, но которых не оказалось в глифе
        /// (например, по причине того, что в этой версии программы они были удалены).</param>
        public IEnumerable<KeyValuePair<string, KeyValuePair<string, string>>> Apply(AbstractGlyph glyph)
        {
            return Apply(glyph, false);
        }

        /// <summary>
        /// Задаёт старые свойства глифу.
        /// </summary>
        /// <param name="glyph">Свойства, которые есть в словаре, но которых не оказалось в глифе
        /// (например, по причине того, что в этой версии программы они были удалены).</param>
        public IEnumerable<KeyValuePair<string, KeyValuePair<string, string>>> ApplyOld(AbstractGlyph glyph)
        {
            return Apply(glyph, true);
        }

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

        #region Private methods
        /// <summary>
        /// Задаёт свойства глифу.
        /// </summary>
        /// <param name="glyph">Свойства, которые есть в словаре, но которых не оказалось в глифе
        /// (например, по причине того, что в этой версии программы они были удалены).</param>
        /// <param name="oldValues">Использовать ли старые значения.</param>
        public IEnumerable<KeyValuePair<string, KeyValuePair<string, string>>> Apply(AbstractGlyph glyph, bool oldValues)
        {
            Type type = glyph.GetType();
            string typeName = type.FullName;

            if (typeName != GlyphType)
                throw new IncompatibleGlyphType(GlyphType, typeName);

            // Не смотря на то, что у свойства Id сеттер приватный,
            // задать новое значение ему нам нужно.
            type.GetProperty("Id").SetValue(glyph, GlyphId, null);

            // Задаём свойства.
            foreach (KeyValuePair<string, KeyValuePair<string, string>> prop in _props)
            {
                PropertyInfo property = type.GetProperty(prop.Key);

                if (property == null)
                    yield return prop;
                else
                {
                    object[] attribs = property.GetCustomAttributes(typeof (ActivePropertyAttribute), true);

                    if (attribs.Length == 0)
                        yield return prop;
                    else
                        property.SetValue(glyph, prop.Value.Value, null);
                }
            }
        }
        #endregion
    }
}
