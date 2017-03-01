// <copyright file="AlignTopSheetCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-V-28</date>
// <summary>Команда - выровнять по врхней стороне листа.</summary>

using System.Collections.Generic;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выровнять по верхней стороне листа.
    /// </summary>
    public sealed class AlignTopSheetCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new InvalidContextException(@"Document not exists.");

            // Если нет выбранных глифов, бросаем исключение.
            if (!Core.Instance.CurrentDocument.DocumentSheet.SelectedGlyphsExists)
                throw new InvalidContextException(@"Selected glyphs not exists.");

            // Вычисляем дефолтное стартовое смещение по вертикали.
            // Берём максимально нижнее положение на листе.
            var sizedSheet = (ISize)Core.Instance.CurrentDocument.DocumentSheet;
            int top = sizedSheet.Height.MMToPx();

            // Список глифов, которые нужно обработать.
            var lst = new List<BlockGlyph>();

            // Проходим по всем незафиксированным глифам на в текущем листе...
            foreach (var glyph in Core.Instance.CurrentDocument.DocumentSheet.NonFixedGlyphs)
            {
                // Если глиф - блок и он выбран...
                if (glyph is BlockGlyph && glyph.Selected)
                {
                    // Приводим глиф к блоку.
                    var g = (BlockGlyph) glyph;

                    // Если текущий глиф выше текущего смещения,
                    // обновляем текущее смещение.
                    if (g.Y - (g.Height >> 1) < top)
                        top = g.Y - (g.Height >> 1);

                    // Добавляем глиф в список.
                    lst.Add(g);
                }
            }

            // Проходим по всем найденным глифам и смещаем их...
            foreach (var glyph in lst)
                glyph.Y -= top;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}