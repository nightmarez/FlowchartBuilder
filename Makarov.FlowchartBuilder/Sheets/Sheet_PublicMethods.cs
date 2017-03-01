// <copyright file="Sheet_PublicMethods.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-05-09</date>
// <summary>Лист.</summary>

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Makarov.FlowchartBuilder.Forms;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Sheets
{
    public abstract partial class Sheet
    {
        /// <summary>
        /// Отрисовать лист.
        /// </summary>
        /// <returns>Отрисованный лист.</returns>
        public Bitmap Draw()
        {
            return Draw(true);
        }

        /// <summary>
        /// Отрисовать лист без масштабирования.
        /// </summary>
        /// <returns>Отрисованный лист.</returns>
        public Bitmap DrawUnscaled()
        {
            return Draw(false);
        }

        /// <summary>
        /// Отрисовать лист.
        /// </summary>
        /// <param name="scaled">Нужно ли применять масштабирование.</param>
        /// <returns>Отрисованный лист.</returns>
        public abstract Bitmap Draw(bool scaled);

        /// <summary>
        /// Клонирует лист.
        /// </summary>
        /// <returns>Клон листа.</returns>
        public abstract object Clone();

        /// <summary>
        /// Очистить лист.
        /// </summary>
        public void Clear()
        {
            _glyphs.Clear();
        }

        /// <summary>
        /// Добавить глиф.
        /// </summary>
        /// <param name="glyph">Глиф.</param>
        public void Add(AbstractGlyph glyph)
        {
            _glyphs.Insert(0, glyph);
        }

        /// <summary>
        /// Удаляет выделение глифов.
        /// </summary>
        public void DeselectAllGlyphs()
        {
            foreach (AbstractGlyph glyph in _glyphs)
                glyph.Selected = false;
        }

        /// <summary>
        /// Удаляет выделение глифов, исключая указанные.
        /// </summary>
        /// <param name="except">Глифы, выделение которых не нужно удалять.</param>
        public void DeselectAllGlyphs(IEnumerable<AbstractGlyph> except)
        {
            foreach (AbstractGlyph glyph in _glyphs)
                if (glyph is BlockGlyph)
                {
                    bool deselect = true;

                    foreach (AbstractGlyph g in except)
                        if (g == glyph)
                        {
                            deselect = false;
                            break;
                        }

                    if (deselect)
                        glyph.Selected = false;
                }
        }

        /// <summary>
        /// Удаляет выделение глифов, исключая указанные.
        /// </summary>
        /// <param name="except">Глифы, выделение которых не нужно удалять.</param>
        public void DeselectAllGlyphs(IEnumerable<BlockGlyph> except)
        {
            foreach (AbstractGlyph glyph in _glyphs)
                if (glyph is BlockGlyph)
                {
                    bool deselect = true;

                    foreach (BlockGlyph g in except)
                        if (g == glyph)
                        {
                            deselect = false;
                            break;
                        }

                    if (deselect)
                        glyph.Selected = false;
                }
        }

        /// <summary>
        /// Выделить все глифы.
        /// </summary>
        public void SelectAllGlyphs()
        {
            foreach (AbstractGlyph glyph in NonFixedGlyphs)
                glyph.Selected = true;
        }

        /// <summary>
        /// Удалить выделенные глифы.
        /// </summary>
        public void DeleteSelectedGlyphs()
        {
            var toDel = _glyphs.Where(glyph => glyph.Selected).ToList();

            foreach (AbstractGlyph glyph in toDel)
                _glyphs.Remove(glyph);
        }

        /// <summary>
        /// Анлочит глифы.
        /// </summary>
        public void UnlockGlyphs()
        {
            foreach (AbstractGlyph glyph in _glyphs)
                if (glyph is BlockGlyph)
                    ((BlockGlyph)glyph).Direction = MoveDirection.None;
        }

        /// <summary>
        /// Отображает свойства выделенных глифов в окне свойств.
        /// </summary>
        public void ShowProperties()
        {
            /*
             * http://www.rsdn.ru/article/dotnet/PropertyGridFAQ.xml
             */
            var gridView = Core.Instance.Windows.GetWindow<PropertiesForm>().Grid;
            gridView.SelectedObjects = SelectedBlockGlyphs.Select(glyph => glyph.GetProxy()).ToArray();
        }
    }
}