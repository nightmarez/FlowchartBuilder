// <copyright file="SelectAllCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - выделить всё.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выделить всё.
    /// </summary>
    public sealed class SelectAllCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new Core.CurrentDocumentNotExistsException();

            // Если нет незафиксированных глифов, бросаем исключение.
            if (!Core.Instance.CurrentDocument.DocumentSheet.NonFixedGlyphsExists)
                throw new InvalidContextException();

            // Выделяем все незафиксированные глифы.
            Core.Instance.CurrentDocument.DocumentSheet.SelectAllGlyphs();

            // Апдейтим состояние команд, связанных с глифами.
            Core.Instance.CurrentDocument.DocumentSheet.UpdateGlyphsCommands();

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}