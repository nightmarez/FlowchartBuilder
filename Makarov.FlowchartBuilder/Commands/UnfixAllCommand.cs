// <copyright file="UnfixAllCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-22</date>
// <summary>Команда - расфиксировать все глифы.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - расфиксировать все глифы.
    /// </summary>
    public sealed class UnfixAllCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new InvalidContextException(@"Document not exists.");

            // Если зафиксированных глифов нет, бросаем исключение.
            if (!Core.Instance.CurrentDocument.DocumentSheet.FixedGlyphsExists)
                throw new InvalidContextException();

            // Расфиксируевываем все глифы.
            foreach (var glyph in Core.Instance.CurrentDocument.DocumentSheet.FixedGlyphs)
                glyph.Fixed = false;

            // Апдейтим состояние команд, связанных с глифами.
            Core.Instance.CurrentDocument.DocumentSheet.UpdateGlyphsCommands();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}