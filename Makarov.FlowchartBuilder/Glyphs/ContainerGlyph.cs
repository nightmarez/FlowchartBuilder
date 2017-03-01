// <copyright file="BlockGlyph.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-20</date>
// <summary>Глиф-контейнер.</summary>

using System.Collections.Generic;

namespace Makarov.FlowchartBuilder.Glyphs
{
    /// <summary>
    /// Глиф-контейнер.
    /// </summary>
    public class ContainerGlyph : BlockGlyph
    {
        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="x">Координата центра X в миллиметрах.</param>
        /// <param name="y">Координата центра Y в миллиметрах.</param>
        /// <param name="width">Ширина в миллиметрах.</param>
        /// <param name="height">Высота в миллиметрах.</param>
        public ContainerGlyph(int x, int y, int width, int height)
            : base(x, y, width, height)
        { }

        public ContainerGlyph()
        { }
        #endregion

        #region Private members
        /// <summary>
        /// Внутренние глифы.
        /// </summary>
        private readonly List<AbstractGlyph> _innerGlyphs = new List<AbstractGlyph>();
        #endregion

        #region Public methods
        /// <summary>
        /// Удалить все внутренние глифы.
        /// </summary>
        public void Clear()
        {
            _innerGlyphs.Clear();
        }

        /// <summary>
        /// Добавить глиф.
        /// </summary>
        /// <param name="glyph">Глиф.</param>
        public void Add(AbstractGlyph glyph)
        {
            _innerGlyphs.Add(glyph);
        }

        /// <summary>
        /// Добавить глифы.
        /// </summary>
        /// <param name="glyphs">Глифы.</param>
        public void Add(IEnumerable<AbstractGlyph> glyphs)
        {
            foreach (var glyph in glyphs)
                Add(glyph);
        }
        #endregion
    }
}