// <copyright file="NewCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - новый файл.</summary>

using Makarov.FlowchartBuilder.Forms;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - новый файл.
    /// </summary>
    public sealed class NewCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Отображаем окно выбора листа.
            using (var form = new SheetsForm())
                form.ShowDialog(Core.Instance.MainWindow);
        }
    }
}