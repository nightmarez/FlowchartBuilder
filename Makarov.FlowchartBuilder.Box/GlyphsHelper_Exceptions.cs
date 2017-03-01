// <copyright file="GlyphsHelper_Exceptions.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-II-18</date>
// <summary>Исключения помошника для работы с глифами.</summary>

using System.Reflection;
using Makarov.FlowchartBuilder.API;
using Makarov.Framework.Core;

namespace Makarov.FlowchartBuilder.Box
{
    /// <summary>
    /// Помошник для работы с глифами.
    /// </summary>
    public static partial class GlyphsHelper
    {
        /// <summary>
        /// Базовое исключение помошника для работы с глифами.
        /// </summary>
        public abstract class GlyphException : MakarovFrameworkException
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
        }
    }
}
