// <copyright file="AlignTopCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-23</date>
// <summary>Команда - выровнять по верхней стороне.</summary>

using System.Collections.Generic;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выровнять по верхней стороне.
    /// </summary>
    public sealed class AlignTopCommand : Command
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

                            // Если текущая группа выше текущего смещения,
                            // обновляем текущее смещение.
                            if (g.GroupY.MMToPx() - (g.GroupHeight.MMToPx() >> 1) < top)
                                top = g.GroupY.MMToPx() - (g.GroupHeight.MMToPx() >> 1);
                        }
                    }
                    else
                    {
                        // Если текущий глиф выше текущего смещения,
                        // обновляем текущее смещение.
                        if (g.Y - (g.Height >> 1) < top)
                            top = g.Y - (g.Height >> 1);

                        // Добавляем глиф в список.
                        lst.Add(g);
                    }
                }
            }

            // Проходим по всем найденным глифам и смещаем их...
            foreach (var glyph in lst)
                glyph.Y = top + (glyph.Height >> 1);

            // Проходим по всем найденным группам и смещаем их...
            foreach (var group in groups)
                group[0].GroupY = (top + (group[0].GroupHeight.MMToPx() >> 1)).PxToMM();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}