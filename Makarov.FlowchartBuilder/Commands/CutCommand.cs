// <copyright file="CutCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - вырезать.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - вырезать.
    /// </summary>
    public sealed class CutCommand : Command
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

            // Добавляем в буфер выбранные глифы.
            Core.Instance.Buffer.Clear();
            foreach (var glyph in Core.Instance.CurrentDocument.DocumentSheet.SelectedGlyphs)
                Core.Instance.Buffer.Add(glyph);

            // Если буфер обмена пуст, бросаем исключение.
            // Команда не должна быть активна, если нечего добавить в буфер.
            if (Core.Instance.Buffer.Count == 0)
                throw new InvalidContextException();

            // Удаляем выбранные глифы из текущего листа.
            Core.Instance.CurrentDocument.DocumentSheet.DeleteSelectedGlyphs();

            // Отключаем команду, т.к. нет больше выбранных глифов.
            Enabled = false;

            // Включаем команду Paste.
            GetInstance("PasteCommand").Enabled = true;

            // Апдейтим состояние команд, связанных с глифами.
            Core.Instance.CurrentDocument.DocumentSheet.UpdateGlyphsCommands();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}