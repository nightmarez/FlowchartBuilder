// <copyright file="FixAllCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-22</date>
// <summary>Команда - зафиксировать все глифы.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - зафиксировать все глифы.
    /// </summary>
    public sealed class FixAllCommand : Command
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
            if (!Core.Instance.CurrentDocument.DocumentSheet.GlyphsExists)
                throw new InvalidContextException(@"Glyphs not exists.");

            // Фиксируем все глифы.
            foreach (var glyph in Core.Instance.CurrentDocument.DocumentSheet.Glyphs)
                glyph.Fixed = true;

            // Апдейтим состояние команд, связанных с глифами.
            Core.Instance.CurrentDocument.DocumentSheet.UpdateGlyphsCommands();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}