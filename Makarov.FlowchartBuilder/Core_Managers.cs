// <copyright file="Core_Managers.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-3</date>
// <summary>Кора (Синглетон).</summary>

using System.Reflection;
using Makarov.Framework.Core.Managers;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Кора (Синглетон).
    /// </summary>
    public sealed partial class Core
    {
        /// <summary>
        /// Менеджер изображений.
        /// </summary>
        public GraphicsManager Images
        {
            get { return _graphicsManager ?? (_graphicsManager = new GraphicsManager(Settings.Directories.Graphics)); }
        }

        /// <summary>
        /// Менеджер иконок.
        /// </summary>
        public GraphicsManager Icons
        {
            get { return _iconsManager ?? (_iconsManager = new GraphicsManager(Settings.Directories.Icons)); }
        }

        /// <summary>
        /// Менеджер окон.
        /// </summary>
        public WindowsManager Windows
        {
            get { return _windowsManager ?? (_windowsManager = new WindowsManager(Assembly.GetExecutingAssembly())); }
        }

        /// <summary>
        /// Менеджер переводчиков.
        /// </summary>
        public TranslatorsManager Translators
        {
            get
            {
                return _translatorsManager ?? (_translatorsManager = TranslatorsManager.LoadFromFolder(
                    Settings.Directories.Translations,
                    Settings.Files.TranslationsSchema));
            }
        }

        /// <summary>
        /// Менеджер сериализаторов.
        /// </summary>
        public SerializerManager Serializers
        {
            get
            {
                return _serializer ??
                    (_serializer = new SerializerManager(Assembly.LoadFrom(Settings.Files.SerializationDll)));
            }
        }

        /// <summary>
        /// Менеджер единиц измерения.
        /// </summary>
        public UnitsManager Units
        {
            get { return _unitsManager ?? (_unitsManager = new UnitsManager()); }
        }

        /// <summary>
        /// Менеджер изображений.
        /// </summary>
        private GraphicsManager _graphicsManager;

        /// <summary>
        /// Менеджер иконок.
        /// </summary>
        private GraphicsManager _iconsManager;

        /// <summary>
        /// Менеджер окон.
        /// </summary>
        private WindowsManager _windowsManager;

        /// <summary>
        /// Менеджер переводчиков.
        /// </summary>
        private TranslatorsManager _translatorsManager;

        /// <summary>
        /// Менеджер документов.
        /// </summary>
        private DocumentsManager _documentsManager;

        /// <summary>
        /// Менеджер сериализаторов.
        /// </summary>
        private SerializerManager _serializer;

        /// <summary>
        /// Менеджер единиц измерения.
        /// </summary>
        private UnitsManager _unitsManager;
    }
}