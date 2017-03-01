// <copyright file="AlignBottomSheetCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-V-28</date>
// <summary>Команда - выровнять по нижней стороне листа.</summary>

using System.Collections.Generic;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выровнять по нижней стороне листа.
    /// </summary>
    public sealed class AlignBottomSheetCommand : Command
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
            // Берём максимально верхнее положение на листе.
            int bottom = 0;

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

                    // Если текущий глиф ниже текущего смещения,
                    // обновляем текущее смещение.
                    if (g.Y + (g.Height >> 1) > bottom)
                        bottom = g.Y + (g.Height >> 1);

                    // Добавляем глиф в список.
                    lst.Add(g);
                }
            }

            // Проходим по всем найденным глифам и смещаем их...
            var sizedSheet = (ISize)Core.Instance.CurrentDocument.DocumentSheet;
            bottom = sizedSheet.Height.MMToPx() - bottom;
            foreach (var glyph in lst)
                glyph.Y += bottom;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}