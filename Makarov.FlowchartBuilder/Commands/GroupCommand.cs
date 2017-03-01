// <copyright file="GroupCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-V-20</date>
// <summary>Команда - группировать глифы.</summary>

using System.Collections.Generic;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - группировать глифы.
    /// </summary>
    public sealed class GroupCommand : Command
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

            // Получаем выделенные глифы-блоки.
            var selectedGlyphs = new List<BlockGlyph>();
            foreach (var glyph in Core.Instance.CurrentDocument.DocumentSheet.SelectedGlyphs)
                if (glyph is BlockGlyph)
                    selectedGlyphs.Add((BlockGlyph)glyph);

            // Если выделенных глифов-блоков не хватает для создания группы,
            // бросаем исключение.
            if (selectedGlyphs.Count <= 1)
                throw new InvalidContextException();

            // Удаляем все выделенные глифы из других групп.
            var newGroups = new List<List<BlockGlyph>>();
            foreach (var group in Core.Instance.CurrentDocument.DocumentSheet.Groups)
            {
                var newGroup = new List<BlockGlyph>();

                foreach (var glyph in group)
                    if (!selectedGlyphs.Contains(glyph) && !newGroup.Contains(glyph))
                        newGroup.Add(glyph);

                newGroups.Add(newGroup);
            }

            Core.Instance.CurrentDocument.DocumentSheet.Groups.Clear();
            foreach (var group in newGroups)
                if (group.Count > 0)
                    Core.Instance.CurrentDocument.DocumentSheet.Groups.Add(group);

            // Создаём новую группу и добавляем в неё выделенные глифы.
            var currGroup = new List<BlockGlyph>();
            foreach (var glyph in selectedGlyphs)
                currGroup.Add(glyph);

            // Добавляем новую группу в список групп.
            Core.Instance.CurrentDocument.DocumentSheet.Groups.Add(currGroup);

            // Отключаем комманду.
            Enabled = false;

            // Апдейтим состояние команд, связанных с глифами.
            Core.Instance.CurrentDocument.DocumentSheet.UpdateGlyphsCommands();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}