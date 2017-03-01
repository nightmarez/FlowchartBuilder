// <copyright file="PluginsManager.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-15</date>
// <summary>Менеджер плугинов.</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using Makarov.FlowchartBuilder.API.Attributes;
using Makarov.FlowchartBuilder.Glyphs;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Менеджер плугинов.
    /// </summary>
    public sealed class PluginsManager
    {
        #region Exceptions
        /// <summary>
        /// Базовое исключение менеджера плугинов.
        /// </summary>
        public class PluginsManagerException : FlowchartBuilderException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="message">Сообщение.</param>
            public PluginsManagerException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Экземпляр менеджера плугинов уже существует.
        /// </summary>
        public sealed class PluginsManagerAlreadyExistsException : SingletonObjectAlreayExistsException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            public PluginsManagerAlreadyExistsException()
                : base("PluginsManager")
            { }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Экземпляр менеджера плугинов.
        /// </summary>
        private static PluginsManager _instance;

        /// <summary>
        /// Конструктор.
        /// </summary>
        private PluginsManager()
        {
            // Если экземпляр класса уже создан, бросаем исключение.
            if (_instance != null)
                throw new PluginsManagerAlreadyExistsException();

            // Загружаем плугины.
            Refresh();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Экземпляр менеджера плугинов.
        /// </summary>
        public static PluginsManager Instance
        {
            get { return _instance ?? (_instance = new PluginsManager()); }
        }

        /// <summary>
        /// Плугины.
        /// </summary>
        public IEnumerable<IPlugin> Plugins
        {
            get { return _plugins; }
        }

        /// <summary>
        /// Глифы.
        /// </summary>
        public IEnumerable<Type> Glyphs
        {
            get { return _glyphs; }
        }

        /// <summary>
        /// Глифы, которые нужно отображать в палитре глифов.
        /// </summary>
        public IEnumerable<Type> VisibleGlyphs
        {
            get
            {
                foreach (Type type in Glyphs)
                {
                    object[] visibleAttribs =
                        type.GetCustomAttributes(typeof(VisibleGlyphAttribute), true);

                    if (visibleAttribs.Length > 0)
                    {
                        if (((VisibleGlyphAttribute)visibleAttribs[visibleAttribs.Length - 1]).IsVisible)
                            yield return type;
                    }
                    else
                        yield return type;
                }
            }
        }

        /// <summary>
        /// Библиотеки глифов.
        /// </summary>
        public IEnumerable<string> GlyphsLibraries
        {
            get { return _glyphsLibs; }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Плугины.
        /// </summary>
        private readonly List<IPlugin> _plugins = new List<IPlugin>();

        /// <summary>
        /// Глифы.
        /// </summary>
        private readonly List<Type> _glyphs = new List<Type>();

        /// <summary>
        /// Библиотеки глифов.
        /// </summary>
        private readonly List<string> _glyphsLibs = new List<string>();
        #endregion

        #region Public methods
        /// <summary>
        /// Перезагрузить менеджер.
        /// </summary>
        public void Refresh()
        {
            // Обнуляем списки.
            _plugins.Clear();
            _glyphs.Clear();

            // Получаем список неабстрактных классов, определённых в сборке.
            var classes = from type in Core.Instance.PluginsLoader.GetTypes<object>()
                          where type.IsClass && !type.IsAbstract
                          select type;

            // Проходим по полученным классам...
            foreach (Type cls in classes)
            {
                // Получаем список интерфейсов.
                Type[] ifaces = cls.GetInterfaces();

                // Проходим по всем интерфейсам...
                // ReSharper disable LoopCanBeConvertedToQuery
                foreach (Type iface in ifaces)
                {
                    // Если интерфейс - IPlugin, создаём экземпляр класса.
                    if (iface == typeof (IPlugin))
                    {
                        // Добавляем объект в список загруженных плугинов.
                        _plugins.Add((IPlugin) Activator.CreateInstance(cls));
                        break;
                    }
                }
                // ReSharper restore LoopCanBeConvertedToQuery
            }

            // Получаем список классов глифов.
            var glyphs = from type in classes
                         where type.IsSubclassOf(typeof (AbstractGlyph))
                         select type;

            // Проходим по классам глифов...
            foreach (Type cls in glyphs)
            {
                // Добавляем тип глифа в коллекцию.
                _glyphs.Add(cls);

                // Добавляем библиотеку в коллекцию.
                foreach (var nameAttr in cls.GetCustomAttributes(typeof (GlyphFamilyAttribute), true))
                {
                    var name = ((GlyphFamilyAttribute)nameAttr).Family;
                    if (!_glyphsLibs.Contains(name))
                        _glyphsLibs.Add(name);
                }
            }
        }
        #endregion
    }
}