// <copyright file="AbstractGlyph_Serialization.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-12-11</date>
// <summary>Абстрактный глиф.</summary>

using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Makarov.FlowchartBuilder.Box;

namespace Makarov.FlowchartBuilder.Glyphs
{
    /// <summary>
    /// Абстрактный глиф.
    /// </summary>
    public abstract partial class AbstractGlyph
    {
        /// <summary>
        /// Сериализует активные свойства.
        /// </summary>
        /// <returns>Пары (имя свойства, сериализованное значение).</returns>
        public IEnumerable<KeyValuePair<string, string>> SerializeProperties()
        {
            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (PropertyInfo property in GlyphsHelper.GetActiveProperties(this))
            {
                yield return new KeyValuePair<string, string>(
                    property.Name,
                    Core.Instance.Serializers.Serialize(property.PropertyType, property.GetValue(this, null)));
            }
            // ReSharper restore LoopCanBeConvertedToQuery
        }

        /// <summary>
        /// Десериализует активные свойства.
        /// </summary>
        /// <param name="pairs">Пары (имя свойства, сериализованное значение).</param>
        /// <returns>Свойства, которых не удалось задать (нет в глифе).</returns>
        public IEnumerable<string> DeserializeProperties(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            // Активные свойства глифа.
            var activeProperties = new List<PropertyInfo>(GlyphsHelper.GetActiveProperties(this));

            // Проходим по заданным парам...
            foreach (KeyValuePair<string, string> kvp in pairs)
            {
                // Найдено ли свойство, чтобы задать ему значение.
                bool finded = false;

                // Проходим по активным свойствам глифа...
                foreach (PropertyInfo property in activeProperties)
                {
                    // Если нашли нужное свойство, задаём ему значение.
                    if (property.Name == kvp.Key)
                    {
                        property.SetValue(
                            this,
                            Core.Instance.Serializers.Deserialize(property.PropertyType, kvp.Value),
                            null);

                        // Свойство найдено, значит...
                        finded = true;

                        // ...больше его искать не нужно.
                        break;
                    }
                }

                // Если свойство в глифе не найдено, вернём его имя.
                if (!finded)
                    yield return kvp.Key;
            }
        }

        /// <summary>
        /// Сериализация в xml.
        /// </summary>
        /// <param name="doc">Xml-документ.</param>
        /// <returns>Нода сериализованного глифа.</returns>
        public XmlNode Serialize(XmlDocument doc)
        {
            OnBeginSerialization(doc);

            // Создаём ноду.
            var node = (XmlElement)doc.CreateNode(
                XmlNodeType.Element,
                Settings.Xml.Tags.Glyph,
                Settings.Xml.Namespaces.Base);

            // Атрибут для хранения типа глифа.
            XmlAttribute glyphClassAttrib = doc.CreateAttribute(
                Settings.Xml.Attributes.GlyphClassName,
                Settings.Xml.Namespaces.Base);
            glyphClassAttrib.Value = GetType().FullName;
            node.Attributes.Append(glyphClassAttrib);

            // Перебираем сериализованные свойства (имя свойства, сериализованное значение)...
            foreach (KeyValuePair<string, string> kvp in SerializeProperties())
            {
                // Создаём дочернюю ноду для хранения свойства.
                var subNode = (XmlElement)doc.CreateNode(
                    XmlNodeType.Element,
                    Settings.Xml.Tags.Property,
                    Settings.Xml.Namespaces.Base);

                // Атрибут дочерней ноды для хранения имени свойства.
                XmlAttribute attrib = doc.CreateAttribute(
                    Settings.Xml.Attributes.Value,
                    Settings.Xml.Namespaces.Base);
                attrib.Value = kvp.Key;
                subNode.Attributes.Append(attrib);

                // Сохраняем значение свойства.
                subNode.InnerText = kvp.Value;

                // Добавляем дочернюю ноду в освновную.
                node.AppendChild(subNode);
            }

            return OnEndSerialization(node);
        }

        /// <summary>
        /// Начало сериализации.
        /// </summary>
        /// <param name="doc">Документ, в который производится сериализация.</param>
        protected virtual void OnBeginSerialization(XmlDocument doc) { }

        /// <summary>
        /// Конец сериализации.
        /// </summary>
        /// <param name="node">Нода, в которую произведена сериализация.</param>
        /// <returns>Нода, в которую произведена сериализация.</returns>
        protected virtual XmlNode OnEndSerialization(XmlNode node)
        {
            return node;
        }

        /// <summary>
        /// Десериализация из xml.
        /// </summary>
        /// <param name="node">Нода сериализованного глифа.</param>
        /// <returns>Свойства, которых не удалось задать (нет в глифе).</returns>
        public IEnumerable<string> Deserialize(XmlNode node)
        {
            node = OnBeginDeserialization(node);

            // Пары (имя свойства, сериализованное значение).
            var properties = new Dictionary<string, string>();

            // Проходим по всем дочерним нодам...
            foreach (XmlNode subNode in node)
            {
                // Имя свойства.
                string propName = subNode.Attributes[Settings.Xml.Attributes.Value].Value;

                // Сериализованное значение свойства.
                string propValue = subNode.InnerText;

                // Добавляем свойство в коллекцию.
                properties.Add(propName, propValue);
            }

            // Задаём свойства глифу.
            return DeserializeProperties(OnEndDeserialization(properties));
        }

        /// <summary>
        /// Начало десериализации.
        /// </summary>
        /// <param name="node">Нода, из которой производится десериализация.</param>
        /// <returns>Нода, из которой производится десериализация.</returns>
        protected XmlNode OnBeginDeserialization(XmlNode node)
        {
            return node;
        }

        /// <summary>
        /// Конец десериализации.
        /// </summary>
        /// <param name="properties">Пары (имя свойства, сериализованное значение).</param>
        /// <returns>Пары (имя свойства, сериализованное значение).</returns>
        protected IEnumerable<KeyValuePair<string, string>> OnEndDeserialization(IEnumerable<KeyValuePair<string, string>> properties)
        {
            return properties;
        }
    }
}
