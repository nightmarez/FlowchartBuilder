// <copyright file="AbstractGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-21</date>
// <summary>Абстрактный глиф.</summary>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using Makarov.FlowchartBuilder.API.Attributes;
using Makarov.Framework.Core.Managers;

namespace Makarov.FlowchartBuilder.Glyphs
{
    /// <summary>
    /// Абстрактный глиф.
    /// </summary>
    public abstract partial class AbstractGlyph : ICloneable
    {
        #region Private members
        /// <summary>
        /// Выбран ли глиф.
        /// </summary>
        private bool _selected;

        /// <summary>
        /// Зафиксирован ли глиф.
        /// </summary>
        private bool _fixed;
        #endregion

        #region Constructors
        protected AbstractGlyph()
        {
            Id = Guid.NewGuid().ToString();
        }
        #endregion

        #region Thumbnails
        /// <summary>
        /// Рисует рамку на иконке.
        /// </summary>
        /// <param name="gfx">Graphics иконки.</param>
        protected void DrawThumbnailBorder(Graphics gfx)
        {
            gfx.DrawRectangle(Settings.Pens.ThumbnailBorder, 0, 0, ThumbnailWidth - 1, ThumbnailHeight - 1);
        }

        /// <summary>
        /// Ширина иконки в пикселях.
        /// </summary>
        public virtual int ThumbnailWidth
        {
            get { return 48; }
        }

        /// <summary>
        /// Высота иконки в пикселях.
        /// </summary>
        public virtual int ThumbnailHeight
        {
            get { return 48; }
        }

        /// <summary>
        /// Иконка.
        /// </summary>
        public virtual Bitmap Thumbnail
        {
            get
            {
                var bmp = new Bitmap(ThumbnailWidth, ThumbnailHeight);

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.Clear(Color.White);

                    using (Brush brush = new LinearGradientBrush(
                        new Point(ThumbnailWidth >> 1, 0),
                        new Point(ThumbnailWidth >> 1, ThumbnailHeight),
                        Settings.Colors.ThumbnailGradientStart,
                        Settings.Colors.ThumbnailGradientEnd))
                    {
                        gfx.FillRectangle(brush, 0, 0, ThumbnailWidth, ThumbnailHeight);
                    }
                }

                return bmp;
            }
        }

        /// <summary>
        /// Идентификатор глифа.
        /// </summary>
        public string Id { get; private set; }
        #endregion

        #region Delegates and events
        public delegate void GlyphDelegate(AbstractGlyph glyph, Graphics gfx);

        /// <summary>
        /// Начало перерисовки глифа.
        /// </summary>
        public event GlyphDelegate BeginRedraw;

        /// <summary>
        /// Конец перерисовки глифа.
        /// </summary>
        public event GlyphDelegate EndRedraw;
        #endregion

        #region Public members
        /// <summary>
        /// Клонирует объект.
        /// </summary>
        /// <remarks>Клонирует все свойства, помеченные аттрибутом ActivePropertyAttribute.</remarks>
        public object Clone()
        {
            // Получаем реальный тип текущего объекта.
            Type t = GetType();

            // Создаём новый экземпляр типа текущего объекта.
            var tmp = Activator.CreateInstance(t);

            // Проходим по всем свойствам...
            foreach (var prop in t.GetProperties(BindingFlags.Public))
            {
                // Если атрибут ActivePropertyAttribute есть у текущего свойства,
                // копируем значение свойства в такое же свойство нового объекта.
                if (prop.GetCustomAttributes(true).OfType<ActivePropertyAttribute>().Any())
                    prop.SetValue(tmp, prop.GetValue(this, null), null);
            }

            // Возвращаем клон.
            return tmp;
        }

        /// <summary>
        /// Создаёт прокси объект.
        /// <remarks>Создаёт прокси объект, который содержит прокси свойства
        /// для свойств исходного объекта, помеченных аттрибутом ActivePropertyAttribute.</remarks>
        /// </summary>
        public object GetProxy()
        {
            return ProxyManager.CreateProxy<ActivePropertyAttribute>(this);
        }
        #endregion
    }
}