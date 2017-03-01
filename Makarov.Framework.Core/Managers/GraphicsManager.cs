// <copyright file="GraphicsManager.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-22</date>
// <summary>Менеджер графических изображений.</summary>

using System;
using System.Drawing;
using System.IO;

namespace Makarov.Framework.Core.Managers
{
    /// <summary>
    /// Менеджер графических изображений.
    /// </summary>
    public sealed class GraphicsManager
    {
        #region Exceptions
        /// <summary>
        /// Исключение менеджера графических изображений.
        /// </summary>
        public class GraphicsManagerException : MakarovFrameworkException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="message">Сообщение.</param>
            public GraphicsManagerException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Картинка не найдена.
        /// </summary>
        public sealed class ImageNotFoundException : GraphicsManagerException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="name">Имя картинки.</param>
            public ImageNotFoundException(string name)
                : base(string.Format("Image '{0}' not found.", name))
            { }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="directory">Директория с графическими изображениями.</param>
        public GraphicsManager(string directory)
        {
            if (!System.IO.Directory.Exists(directory))
                throw new DirectoryNotFoundException(string.Format("Directory '{0}' not found.", directory));

            Directory = directory;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Рабочая директория.
        /// </summary>
        public string Directory
        {
            get; private set;
        }

        /// <summary>
        /// Даёт доступ к изображениям.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <returns>Изображение.</returns>
        public Bitmap this[string name]
        {
            get
            {
                Requested++;

                var arr = new[] { ".bmp", ".png", ".jpg" };
                Bitmap result = null;

                try
                {
                    if (_images.ContainsKey(name))
                    {
                        if (_images[name].IsAlive)
                        {
                            result = (Bitmap) ((Bitmap) _images[name].Target).Clone();
                            Resurrected++;
                        }
                        else
                        {
                            ResurrectionFailures++;
                        }
                    }
                }
                catch
                {
                    result = null;
                    ResurrectionFailures++;
                }

                if (result == null)
                    foreach (string ext in arr)
                    {
                        string filename = Path.Combine(Directory, name + ext);

                        if (File.Exists(filename))
                        {
                            var bmp = (Bitmap)Image.FromFile(filename);
                            Loaded++;

                            if (_images.ContainsKey(name))
                                _images.Remove(name);

                            _images.Add(name, new WeakReference(bmp));
                            result = (Bitmap)bmp.Clone();
                        }
                    }

                if (result == null)
                    throw new ImageNotFoundException(name);

                return result;
            }
        }

        /// <summary>
        /// Сколько восстановлено объектов.
        /// </summary>
        public int Resurrected
        {
            get; private set;
        }

        /// <summary>
        /// Ошибок воскрешения.
        /// </summary>
        public int ResurrectionFailures
        {
            get; private set;
        }

        /// <summary>
        /// Сколько запрошено объектов.
        /// </summary>
        public int Requested
        {
            get; private set;
        }

        /// <summary>
        /// Сколько сделано загрузок.
        /// </summary>
        public int Loaded
        {
            get; private set;
        }
        #endregion

        #region Private members
        /// <summary>
        /// Изображения.
        /// </summary>
        private readonly StringDictionary<WeakReference> _images = new StringDictionary<WeakReference>();
        #endregion

        #region Public methods
        /// <summary>
        /// Очистить кеш.
        /// </summary>
        public void Clear()
        {
            _images.Clear();
        }
        #endregion
    }
}