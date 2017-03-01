// <copyright file="Document_Exceptions.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-17</date>
// <summary>Исключения документа.</summary>

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Документ.
    /// </summary>
    public sealed partial class Document
    {
        /// <summary>
        /// Исключение документа.
        /// </summary>
        public abstract class DocumentException : FlowchartBuilderException
        {
            /// <param name="message">Сообщение.</param>
            protected DocumentException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Неправильный сохранённый документ.
        /// </summary>
        public abstract class IncorrectSavedDocumentException : DocumentException
        {
            /// <param name="reason">Причина.</param>
            protected IncorrectSavedDocumentException(string reason)
                : base(string.Format(@"Incorrect saved document: {0}.", reason ?? string.Empty))
            { }
        }

        /// <summary>
        /// Главная нода документа не найдена.
        /// </summary>
        public sealed class MainDocumentNodeNotFoundException : IncorrectSavedDocumentException
        {
            public MainDocumentNodeNotFoundException()
                : base(@"Main document node not found")
            { }
        }

        /// <summary>
        /// Нода листа не найдена.
        /// </summary>
        public sealed class SheetNodeNotFoundException : IncorrectSavedDocumentException
        {
            public SheetNodeNotFoundException()
                : base(@"Sheet node not found")
            { }
        }

        /// <summary>
        /// Лист не загружен.
        /// </summary>
        public sealed class SheetNotLoadedException : IncorrectSavedDocumentException
        {
            public SheetNotLoadedException()
                : base(@"Sheet not loaded")
            { }
        }

        /// <summary>
        /// Тип глифа не найден.
        /// </summary>
        public sealed class GlyphTypeNotFoundException : IncorrectSavedDocumentException
        {
            /// <param name="glyphType">Тип глифа.</param>
            public GlyphTypeNotFoundException(string glyphType)
                : base(string.Format(@"Glyph type '{0}' not found", glyphType ?? string.Empty))
            { }
        }
    }
}