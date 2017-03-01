// <copyright file="SaveSettingsCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-06-05</date>
// <summary>Команда - сохранить настройки в файл.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - сохранить настройки в файл.
    /// </summary>
    public sealed class SaveSettingsCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            SettingsExport.ShowSaveDialog();
        }
    }
}