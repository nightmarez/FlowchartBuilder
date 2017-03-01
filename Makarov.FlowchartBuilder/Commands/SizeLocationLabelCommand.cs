// <copyright file="SizeLocationLabelCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-22</date>
// <summary>Команда - показывать информацию над изменяемым глифом.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - показывать информацию над изменяемым глифом.
    /// </summary>
    public sealed class SizeLocationLabelCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Сохраняем новые настройки.
            Settings.Environment.SizeLocationLabel = !Settings.Environment.SizeLocationLabel;

            Checked = Settings.Environment.SizeLocationLabel;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}