// <copyright file="AbstractGlyph_Serialization.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-12-11</date>
// <summary>Абстрактный глиф.</summary>

using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Glyphs
{
    /// <summary>
    /// Абстрактный глиф.
    /// </summary>
    public abstract partial class AbstractGlyph
    {
        /// <summary>
        /// Выбран ли глиф.
        /// </summary>
        public bool Selected
        {
            get { return _selected; }

            set
            {
                if (value && _fixed)
                    throw new InvalidContextException();

                _selected = value;
            }
        }

        /// <summary>
        /// Зафиксирован ли глиф.
        /// </summary>
        /// <remarks>Зафиксированный глиф нельзя модифицировать.</remarks>
        public bool Fixed
        {
            get { return _fixed; }

            set
            {
                _fixed = value;

                if (value)
                    Selected = false;
            }
        }

        /// <summary>
        /// Заголовок.
        /// </summary>
        [Description(@"Caption.")]
        [Category(@"Layout")]
        [DisplayName(@"Caption")]
        [ActiveProperty]
        public string Caption
        {
            get;
            set;
        }

        /// <summary>
        /// Есть ли нумерация (для размещения в галерее) для данного глифа.
        /// </summary>
        public bool OrderExists
        {
            get
            {
                return GetType().GetCustomAttributes(typeof(GlyphOrderAttribute), true).Length > 0;
            }
        }

        /// <summary>
        /// Номер глифа в палитре.
        /// </summary>
        public int Order
        {
            get
            {
                Type type = GetType();
                object[] attribs = type.GetCustomAttributes(typeof(GlyphOrderAttribute), true);

                if (attribs.Length == 0)
                {
                    if (Settings.Environment.IsDebug)
                        throw new UnknownOrderException(type);
                }

                return ((GlyphOrderAttribute)attribs[attribs.Length - 1]).Order;
            }
        }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name
        {
            get
            {
                Type type = GetType();
                object[] attribs = type.GetCustomAttributes(typeof (GlyphNameAttribute), true);

                if (attribs.Length == 0)
                {
                    if (Settings.Environment.IsDebug)
                        throw new UnknownNameException(type);

                    return Core.Instance.CurrentTranslator["UndefinedGlyphName"];
                }

                return ((GlyphNameAttribute)attribs[attribs.Length - 1]).Name;
            }
        }

        /// <summary>
        /// Семейство.
        /// </summary>
        public string Family
        {
            get
            {
                Type type = GetType();
                object[] attribs = type.GetCustomAttributes(typeof(GlyphFamilyAttribute), true);

                if (attribs.Length == 0)
                {
                    if (Settings.Environment.IsDebug)
                        throw new UnknownFamilyException(type);

                    return Core.Instance.CurrentTranslator["UndefinedGlyphFamily"];
                }

                return ((GlyphFamilyAttribute)attribs[attribs.Length - 1]).Family;
            }
        }

        /// <summary>
        /// Режим сглаживания.
        /// </summary>
        protected static SmoothingMode SmoothMode
        {
            get
            {
                return Settings.Environment.Antialiasing
                           ? SmoothingMode.HighQuality
                           : SmoothingMode.HighSpeed;
            }
        }
    }
}