// <copyright file="Sheet_Properties.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-05-09</date>
// <summary>Лист.</summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using Makarov.FlowchartBuilder.API.Attributes;
using Makarov.FlowchartBuilder.Commands;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Glyphs;
using System.Linq;

namespace Makarov.FlowchartBuilder.Sheets
{
    /// <summary>
    /// Лист.
    /// </summary>
    public abstract partial class Sheet
    {
        /// <summary>
        /// Коэффициент масштабирования.
        /// </summary>
        public float ScaleFactor
        {
            get { return _scaleFactor; }
            set
            {
                if (value <= MinScaleFactor)
                {
                    value = MinScaleFactor;
                    Command.GetInstance<ZoomOutCommand>().Enabled = false;
                }
                else
                    Command.GetInstance<ZoomOutCommand>().Enabled = true;

                if (value >= MaxScaleFactor)
                {
                    value = MaxScaleFactor;
                    Command.GetInstance<ZoomInCommand>().Enabled = false;
                }
                else
                    Command.GetInstance<ZoomInCommand>().Enabled = true;

                _scaleFactor = value;
            }
        }

        /// <summary>
        /// Максимальный коэффициент масштабирования.
        /// </summary>
        public static float MaxScaleFactor
        {
            get { return 2.0f; }
        }

        /// <summary>
        /// Минимальный коэффициент масштабирования.
        /// </summary>
        public static float MinScaleFactor
        {
            get { return 0.2f; }
        }

        /// <summary>
        /// Глифы.
        /// </summary>
        public IEnumerable<AbstractGlyph> Glyphs
        {
            get { return _glyphs; }
        }

        /// <summary>
        /// Количество глифов.
        /// </summary>
        public int GlyphsCount
        {
            get { return _glyphs.Count; }
        }

        /// <summary>
        /// Есть ли глифы.
        /// </summary>
        public bool GlyphsExists
        {
            get
            {
                return GlyphsCount > 0;
            }
        }

        /// <summary>
        /// Выбранные глифы.
        /// </summary>
        public IEnumerable<AbstractGlyph> SelectedGlyphs
        {
            get { return _glyphs.Where(glyph => glyph.Selected); }
        }

        /// <summary>
        /// Выбранные глифы-блоки.
        /// </summary>
        public IEnumerable<BlockGlyph> SelectedBlockGlyphs
        {
            get { return SelectedGlyphs.OfType<BlockGlyph>(); }
        }

        /// <summary>
        /// Количество выбранных глифов.
        /// </summary>
        public int SelectedGlyphsCount
        {
            get { return _glyphs.Count(glyph => glyph.Selected); }
        }

        /// <summary>
        /// Количество выбранных глифов-блоков.
        /// </summary>
        public int SelectedBlockGlyphsCount
        {
            get { return _glyphs.Count(glyph => glyph.Selected && glyph is BlockGlyph); }
        }

        /// <summary>
        /// Есть ли выбранные глифы.
        /// </summary>
        public bool SelectedGlyphsExists
        {
            get { return _glyphs.Any(glyph => glyph.Selected); }
        }

        /// <summary>
        /// Невыбранные глифы.
        /// </summary>
        public IEnumerable<AbstractGlyph> NonSelectedGlyphs
        {
            get { return _glyphs.Where(glyph => !glyph.Selected); }
        }

        /// <summary>
        /// Количество невыбранных глифов.
        /// </summary>
        public int NonSelectedGlyphsCount
        {
            get { return _glyphs.Count(glyph => !glyph.Selected); }
        }

        /// <summary>
        /// Есть ли невыбранные глифы.
        /// </summary>
        public bool NonSelectedGlyphsExists
        {
            get { return _glyphs.Any(glyph => !glyph.Selected); }
        }

        /// <summary>
        /// Зафиксированные глифы.
        /// </summary>
        public IEnumerable<AbstractGlyph> FixedGlyphs
        {
            get { return _glyphs.Where(glyph => glyph.Fixed); }
        }

        /// <summary>
        /// Количество зафиксированных глифов.
        /// </summary>
        public int FixedGlyphsCount
        {
            get { return _glyphs.Count(glyph => glyph.Fixed); }
        }

        /// <summary>
        /// Есть ли зафиксированные глифы.
        /// </summary>
        public bool FixedGlyphsExists
        {
            get { return _glyphs.Any(glyph => glyph.Fixed); }
        }

        /// <summary>
        /// Незафиксированные глифы.
        /// </summary>
        public IEnumerable<AbstractGlyph> NonFixedGlyphs
        {
            get { return _glyphs.Where(glyph => !glyph.Fixed); }
        }

        /// <summary>
        /// Количество незафиксированных глифов.
        /// </summary>
        public int NonFixedGlyphsCount
        {
            get { return _glyphs.Count(glyph => !glyph.Fixed); }
        }

        /// <summary>
        /// Есть ли незафиксированные глифы.
        /// </summary>
        public bool NonFixedGlyphsExists
        {
            get { return _glyphs.Any(glyph => !glyph.Fixed); }
        }

        /// <summary>
        /// Залоченные глифы.
        /// </summary>
        public IEnumerable<AbstractGlyph> LockedGlyphs
        {
            get { return _glyphs.Where(glyph => !(glyph is BlockGlyph && ((BlockGlyph)glyph).Direction == MoveDirection.None)); }
        }

        /// <summary>
        /// Количество залоченных глифов.
        /// </summary>
        public int LockedGlyphsCount
        {
            get { return _glyphs.Count(glyph => !(glyph is BlockGlyph && ((BlockGlyph)glyph).Direction == MoveDirection.None)); }
        }

        /// <summary>
        /// Есть ли залоченные глифы.
        /// </summary>
        public bool LockedGlyphsExists
        {
            get { return _glyphs.Any(glyph => !(glyph is BlockGlyph && ((BlockGlyph)glyph).Direction == MoveDirection.None)); }
        }

        /// <summary>
        /// Незалоченные глифы.
        /// </summary>
        public IEnumerable<AbstractGlyph> NonLockedGlyphs
        {
            get { return _glyphs.Where(glyph => glyph is BlockGlyph && ((BlockGlyph)glyph).Direction == MoveDirection.None); }
        }

        /// <summary>
        /// Количество незалоченных глифов.
        /// </summary>
        public int NonLockedGlyphsCount
        {
            get { return _glyphs.Count(glyph => glyph is BlockGlyph && ((BlockGlyph)glyph).Direction == MoveDirection.None); }
        }

        /// <summary>
        /// Есть ли незалоченные глифы.
        /// </summary>
        public bool NonLockedGlyphsExists
        {
            get { return _glyphs.Any(glyph => glyph is BlockGlyph && ((BlockGlyph)glyph).Direction == MoveDirection.None); }
        }

        /// <summary>
        /// Начало выделения в пикселях.
        /// </summary>
        public Point SelectionStart
        {
            get { return _selectionStart.Value; }
        }

        /// <summary>
        /// Конец выделения в пикселях.
        /// </summary>
        public Point SelectionEnd
        {
            get { return _selectionEnd; }
        }

        /// <summary>
        /// Размер выделения в пикселях.
        /// </summary>
        public Size SelectionSize
        {
            get
            {
                return new Size(
                    Math.Abs(SelectionEnd.X - SelectionStart.X),
                    Math.Abs(SelectionEnd.Y - SelectionStart.Y));
            }
        }

        /// <summary>
        /// Есть ли выделение.
        /// </summary>
        public bool SelectionExists
        {
            get { return _selectionStart.HasValue; }
        }

        /// <summary>
        /// Выделение в пикселях.
        /// </summary>
        public Rectangle Selection
        {
            get { return new Rectangle(SelectionStart, SelectionSize); }
        }

        /// <summary>
        /// Иконка.
        /// </summary>
        public virtual Bitmap Thumbnail
        {
            get
            {
                var bmp = new Bitmap(ThumbnailWidth, ThumbnailHeight);
                bmp.DrawBorder(Settings.Colors.ThumbnailBorder);
                return bmp;
            }
        }

        /// <summary>
        /// Ширина иконки.
        /// </summary>
        public static int ThumbnailWidth
        {
            get { return 100; }
        }

        /// <summary>
        /// Высота иконки.
        /// </summary>
        public static int ThumbnailHeight
        {
            get { return 100; }
        }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name
        {
            get
            {
                Type type = GetType();
                object[] attribs = type.GetCustomAttributes(typeof(SheetNameAttribute), true);

                if (attribs.Length == 0)
                {
                    if (Settings.Environment.IsDebug)
                        throw new UnknownNameException(type);

                    return Core.Instance.CurrentTranslator["UndefinedSheetName"];
                }

                return ((SheetNameAttribute)attribs[attribs.Length - 1]).Name;
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
                object[] attribs = type.GetCustomAttributes(typeof(SheetFamilyAttribute), true);

                if (attribs.Length == 0)
                {
                    if (Settings.Environment.IsDebug)
                        throw new UnknownFamilyException(type);

                    return Core.Instance.CurrentTranslator["UndefinedSheetFamily"];
                }

                return ((SheetFamilyAttribute)attribs[attribs.Length - 1]).Family;
            }
        }

        /// <summary>
        /// Группы глифов.
        /// </summary>
        public List<List<BlockGlyph>> Groups
        {
            get;
            private set;
        }
    }
}