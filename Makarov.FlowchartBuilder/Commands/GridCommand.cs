// <copyright file="GridCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - сетка.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - сетка.
    /// </summary>
    public sealed class GridCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Сохраняем новые настройки сетки.
            Settings.Environment.Grid = !Settings.Environment.Grid;

            Checked = Settings.Environment.Grid;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}