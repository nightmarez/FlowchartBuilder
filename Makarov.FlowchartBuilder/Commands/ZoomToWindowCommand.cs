// <copyright file="ZoomToWindowCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - масштабировать до размеров окна.</summary>

using Makarov.FlowchartBuilder.Extensions;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - масштабировать до размеров окна.
    /// </summary>
    public sealed class ZoomToWindowCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new InvalidContextException(@"Document not exists.");

            // Вычисляем коэффициенты масштабирования для листа.
            var sizedSheet = (ISize)Core.Instance.CurrentDocument.DocumentSheet;
            float h = sizedSheet.Width.MMToPx() /
                      (float) Core.Instance.MainWindow.InnerRectangle.Width;
            float v = sizedSheet.Height.MMToPx() /
                      (float) Core.Instance.MainWindow.InnerRectangle.Height;

            // Задаём коэффициент масштабирования листу.
            Core.Instance.CurrentDocument.DocumentSheet.ScaleFactor = h > v ? 1 / h : 1 / v;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}