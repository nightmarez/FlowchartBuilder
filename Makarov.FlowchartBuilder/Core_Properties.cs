// <copyright file="Core_Properties.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-III-30</date>
// <summary>Кора (Синглетон).</summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using Makarov.FlowchartBuilder.Forms;
using Makarov.FlowchartBuilder.Glyphs;
using Makarov.Framework.Core;
using Makarov.Framework.Core.SettingStorages;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Кора (Синглетон).
    /// </summary>
    public sealed partial class Core
    {
        /// <summary>
        /// Главное окно.
        /// </summary>
        public MainForm MainWindow
        {
            get { return Windows.GetWindow<MainForm>(); }
        }

        /// <summary>
        /// Текущий документ.
        /// </summary>
        public Document CurrentDocument
        {
            get
            {
                // Если документа нет, бросаем исключение.
                if (!IsDocumentsExists)
                    throw new CurrentDocumentNotExistsException();

                // Возвращаем текущий элемент.
                return _documents[_currDocIndex];
            }
        }

        /// <summary>
        /// Есть ли документы.
        /// </summary>
        public bool IsDocumentsExists
        {
            get { return _documents.Count > 0; }
        }

        /// <summary>
        /// Первый ли это запуск программы.
        /// </summary>
        public bool IsFirstRun
        {
            get;
            private set;
        }

        /// <summary>
        /// Буфер обмена.
        /// </summary>
        public List<AbstractGlyph> Buffer
        {
            get { return _buffer; }
        }

        /// <summary>
        /// Текущий тип глифа.
        /// </summary>
        public Type CurrentGlyphType
        {
            get;
            set;
        }

        /// <summary>
        /// Выбран ли тип глифа.
        /// </summary>
        public bool CurrGlyphTypeSelected
        {
            get { return CurrentGlyphType != null; }
        }

        /// <summary>
        /// Нужно ли перерисовывать главное окно.
        /// </summary>
        public bool NeedRedraw
        {
            get;
            set;
        }

        /// <summary>
        /// Загрузчик плугинов.
        /// </summary>
        public Loader PluginsLoader
        {
            get
            {
                return _pluginsLoader ?? 
                    (_pluginsLoader = new Loader(Settings.Directories.Plugins));
            }
        }

        /// <summary>
        /// Текущий переводчик.
        /// </summary>
        public Translator CurrentTranslator
        {
            get { return Translators[Settings.Environment.Languages.CurrentLanguage]; }
        }

        /// <summary>
        /// Доступ к настройкам.
        /// </summary>
        private IDict<string> SettingsStorage
        {
            get
            {
                if (_settingsStorage != null)
                    return _settingsStorage;

                if (Framework.Core.Settings.Environment.IsMono)
                {
                    try
                    {
                        return (_settingsStorage = new XmlStorage(
                            Settings.Files.XmlSettingsFile));
                    }
                    catch
                    {
                        return (_settingsStorage = new FakeStorage());
                    }
                }

                try
                {
                    return (_settingsStorage = new RegistryStorage(
                        Settings.RegistryKeys.ProgramSettingsKey));
                }
                catch
                {
                    try
                    {
                        return (_settingsStorage = new XmlStorage(
                            Settings.Files.XmlSettingsFile));
                    }
                    catch
                    {
                        return (_settingsStorage = new FakeStorage());
                    }
                }
            }
        }

        /// <summary>
        /// Кешированный доступ к настройкам.
        /// </summary>
        public Cache<string> CachedSettingsStorage
        {
            get 
            { 
                return _cachedSettingsStorage ?? 
                    (_cachedSettingsStorage = new Cache<string>(SettingsStorage)); 
            }
        }

        /// <summary>
        /// Кеш перьев.
        /// </summary>
        public Cache<Pen> CachedPens
        {
            get { return _pens; }
        }

        /// <summary>
        /// Кеш кистей.
        /// </summary>
        public Cache<Brush> CachedBrushes
        {
            get { return _brushes; }
        }
    }
}