// <copyright file="AlignVerticalCenterCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-23</date>
// <summary>Команда - выровнять по центру по вертикали стороне.</summary>

using System.Collections.Generic;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выровнять по центру по вертикали стороне.
    /// </summary>
    public sealed class AlignCenterVerticalCommand : Command
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

            // Сумма смещений.
            int center = 0;

            // Список глифов, которые нужно обработать.
            var lst = new List<BlockGlyph>();

            // Список групп, которые нужно обработать.
            var groups = new List<List<BlockGlyph>>();

            // Сумма глифов в группах.
            int gCount = 0;

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

                            // Добавляем смещение текущей группы к сумме.
                            foreach (var gg in g.Group)
                                center += gg.Y;

                            // Обновляем сумму глифов в группах.
                            gCount += g.Group.Count;
                        }
                    }
                    else
                    {
                        // Добавляем смещение текущего глифа к сумме.
                        center += g.Y;

                        // Добавляем глиф в список.
                        lst.Add(g);
                    }
                }
            }

            // Находим "центр масс".
            center /= lst.Count + gCount;

            // Проходим по всем найденным глифам и смещаем их...
            foreach (var glyph in lst)
                glyph.Y = center;

            // Проходим по всем найденным группам и смещаем их...
            foreach (var group in groups)
                group[0].GroupY = center.PxToMM();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}