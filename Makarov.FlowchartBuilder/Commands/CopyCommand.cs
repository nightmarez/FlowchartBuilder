// <copyright file="CopyCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - копировать.</summary>

using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - копировать.
    /// </summary>
    public sealed class CopyCommand : Command
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
                Core.Instance.Buffer.Add((AbstractGlyph)glyph.Clone());

            // Если буфер обмена пуст, бросаем исключение.
            // Команда не должна быть активна, если нечего добавить в буфер.
            if (Core.Instance.Buffer.Count == 0)
                throw new InvalidContextException();

            // Апдейтим состояние команд, связанных с глифами.
            Core.Instance.CurrentDocument.DocumentSheet.UpdateGlyphsCommands();

            // Включаем команду Paste.
            GetInstance("PasteCommand").Enabled = true;
        }
    }
}