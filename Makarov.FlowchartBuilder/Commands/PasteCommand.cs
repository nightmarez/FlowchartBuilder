// <copyright file="PasteCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - вставить.</summary>

using System.Collections.Generic;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - вставить.
    /// </summary>
    public sealed class PasteCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new InvalidContextException(@"Document not exists.");

            // Если буфер обмена пуст, бросаем исключение.
            // Команда не должна быть активна, если буфер пуст.
            if (Core.Instance.Buffer.Count == 0)
                throw new InvalidContextException();

            // Отключаем команду.
            Enabled = false;

            // Список добавленных глифов.
            var lst = new List<AbstractGlyph>();

            // Вставляем глифы в лист.
            Core.Instance.CurrentDocument.DocumentSheet.DeselectAllGlyphs();
            foreach (var glyph in Core.Instance.Buffer)
            {
                glyph.Selected = true;
                Core.Instance.CurrentDocument.DocumentSheet.Add(glyph);
                lst.Add(glyph);
            }

            // Выделяем глифы.
            Core.Instance.CurrentDocument.DocumentSheet.DeselectAllGlyphs(lst);
            foreach (var glyph in lst)
                if (!glyph.Selected)
                    glyph.Selected = true;

            // Очищаем буфер обмена.
            Core.Instance.Buffer.Clear();

            // Апдейтим состояние команд, связанных с глифами.
            Core.Instance.CurrentDocument.DocumentSheet.UpdateGlyphsCommands();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}