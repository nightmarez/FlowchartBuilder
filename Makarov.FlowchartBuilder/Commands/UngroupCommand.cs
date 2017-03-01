// <copyright file="UngroupCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-05-20</date>
// <summary>Команда - разгруппировать глифы.</summary>

using System.Collections.Generic;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - разгруппировать глифы.
    /// </summary>
    public sealed class UngroupCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Получаем выделенные глифы-блоки.
            var selectedGlyphs = new List<BlockGlyph>();
            foreach (var glyph in Core.Instance.CurrentDocument.DocumentSheet.SelectedGlyphs)
                if (glyph is BlockGlyph)
                    selectedGlyphs.Add((BlockGlyph)glyph);

            // Ищем группы, в которой содержатся выделенные глифы.
            var selectedGroups = new List<List<BlockGlyph>>();

            foreach (var glyph in selectedGlyphs)
                foreach (var group in Core.Instance.CurrentDocument.DocumentSheet.Groups)
                    foreach (var g in group)
                        if (g == glyph)
                        {
                            selectedGroups.Add(group);
                            continue;
                        }

            // Удаляем все найденные группы из списка групп.
            foreach (var group in selectedGroups)
                Core.Instance.CurrentDocument.DocumentSheet.Groups.Remove(group);

            // Отключаем комманду.
            Enabled = false;

            // Апдейтим состояние команд, связанных с глифами.
            Core.Instance.CurrentDocument.DocumentSheet.UpdateGlyphsCommands();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}