// <copyright file="AlignBottomCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-23</date>
// <summary>Команда - выровнять по нижней стороне.</summary>

using System.Collections.Generic;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выровнять по нижней стороне.
    /// </summary>
    public sealed class AlignBottomCommand : Command
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

            // Список групп, которые нужно обработать.
            var groups = new List<List<BlockGlyph>>();

            // Проходим по всем незафиксированным глифам на в текущем листе...
            foreach (var glyph in Core.Instance.CurrentDocument.DocumentSheet.NonFixedGlyphs)
            {
                // Если глиф - блок и он выбран...
                if (glyph is BlockGlyph && glyph.Selected)
                {
                    // Приводим глиф к блоку.
                    var g = (BlockGlyph)glyph;

                    if (g.InGroup)
                    {
                        // Добавляем группу в список.
                        if (!groups.Contains(g.Group))
                        {
                            groups.Add(g.Group);

                            // Если текущая группа ниже текущего смещения,
                            // обновляем текущее смещение.
                            if (g.GroupY.MMToPx() + (g.GroupHeight.MMToPx() >> 1) > bottom)
                                bottom = g.GroupY.MMToPx() + (g.GroupHeight.MMToPx() >> 1);
                        }
                    }
                    else
                    {
                        // Если текущий глиф ниже текущего смещения,
                        // обновляем текущее смещение.
                        if (g.Y + (g.Height >> 1) > bottom)
                            bottom = g.Y + (g.Height >> 1);

                        // Добавляем глиф в список.
                        lst.Add(g);
                    }
                }
            }

            // Проходим по всем найденным глифам и смещаем их...
            foreach (var glyph in lst)
                glyph.Y = bottom - (glyph.Height >> 1);

            // Проходим по всем найденным группам и смещаем их...
            foreach (var group in groups)
                group[0].GroupY = (bottom - (group[0].GroupHeight.MMToPx() >> 1)).PxToMM();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}