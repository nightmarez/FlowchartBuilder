// <copyright file="ShowSettingsCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-IX-24</date>
// <summary>Команда - показать окно настроек.</summary>

using Makarov.FlowchartBuilder.Forms;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - показать окно настроек.
    /// </summary>
    public sealed class ShowSettingsCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            Core.Instance.Windows.GetWindow<SettingsForm>().ShowDialog(Core.Instance.MainWindow);
        }
    }
}