// <copyright file="Core_PrivateMembers.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-III-30</date>
// <summary>Кора (синглетон).</summary>

using System.Collections.Generic;
using System.Drawing;
using Makarov.FlowchartBuilder.Glyphs;
using Makarov.Framework.Core;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Кора (синглетон).
    /// </summary>
    public sealed partial class Core
    {
        /// <summary>
        /// Индекс текущего документа.
        /// </summary>
        private int _currDocIndex;

        /// <summary>
        /// Освобождена ли кора.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Загрузчик плугинов.
        /// </summary>
        private Loader _pluginsLoader;

        /// <summary>
        /// Кешированный доступ к реестру.
        /// </summary>
        private Cache<string> _cachedSettingsStorage;

        /// <summary>
        /// Доступ к реестру.
        /// </summary>
        private IDict<string> _settingsStorage;

        /// <summary>
        /// Кеш кистей.
        /// </summary>
        private Cache<Brush> _brushes =
            new Cache<Brush>(new DisposableStringDictionary<Brush>());

        /// <summary>
        /// Кеш перьев.
        /// </summary>
        private Cache<Pen> _pens =
            new Cache<Pen>(new DisposableStringDictionary<Pen>());

        /// <summary>
        /// Буфер обмена.
        /// </summary>
        private readonly List<AbstractGlyph> _buffer = new List<AbstractGlyph>();

        /// <summary>
        /// Список документов.
        /// </summary>
        private readonly List<Document> _documents = new List<Document>();
    }
}