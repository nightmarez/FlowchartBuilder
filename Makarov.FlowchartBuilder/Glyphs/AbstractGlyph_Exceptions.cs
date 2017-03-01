// <copyright file="AbstractGlyph_Serialization.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-12-11</date>
// <summary>Абстрактный глиф.</summary>

using System;
using System.Reflection;

namespace Makarov.FlowchartBuilder.Glyphs
{
    /// <summary>
    /// Абстрактный глиф.
    /// </summary>
    public abstract partial class AbstractGlyph
    {
        /// <summary>
        /// Базовое исключение глифа.
        /// </summary>
        public abstract class GlyphException : FlowchartBuilderException
        {
            /// <param name="message">Сообщение.</param>
            protected GlyphException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Больше одного атрибута активного свойства у одного свойства.
        /// </summary>
        public sealed class TooManyActivePropertyAttributesException : GlyphException
        {
            /// <param name="glyphType">Тип глифа.</param>
            /// <param name="propertyName">Имя свойства.</param>
            public TooManyActivePropertyAttributesException(string glyphType, string propertyName)
                : base(string.Format("Too many ActivePropertyAttributes for property. " +
                "Glyph '{0}', property '{1}'.", glyphType ?? string.Empty, propertyName ?? string.Empty))
            { }

            /// <param name="glyph">Глиф.</param>
            /// <param name="property">Свойство.</param>
            public TooManyActivePropertyAttributesException(AbstractGlyph glyph, PropertyInfo property)
                : this(glyph.GetType().FullName, property.Name)
            { }
        }

        /// <summary>
        /// Не удалось получить имя глифа.
        /// </summary>
        public sealed class UnknownNameException : GlyphException
        {
            /// <param name="type">Тип глифа.</param>
            public UnknownNameException(Type type)
                : base(string.Format(@"Unknown glyph '{0}' name.", type.Name))
            { }
        }

        /// <summary>
        /// Не удалось получить семейство глифа.
        /// </summary>
        public sealed class UnknownFamilyException : GlyphException
        {
            /// <param name="type">Тип глифа.</param>
            public UnknownFamilyException(Type type)
                : base(string.Format(@"Unknown glyph '{0}' family.", type.Name))
            { }
        }

        /// <summary>
        /// Не удалось получить номер глифа в палитре.
        /// </summary>
        public sealed class UnknownOrderException : GlyphException
        {
            /// <param name="type">Тип глифа.</param>
            public UnknownOrderException(Type type)
                : base(string.Format(@"Unknown glyph '{0}' order.", type.Name))
            { }
        }
    }
}