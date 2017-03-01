// <copyright file="LoadSettingsCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-VI-05</date>
// <summary>Команда - загрузить настройки из файла.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - загрузить настройки из файла.
    /// </summary>
    public sealed class LoadSettingsCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            SettingsExport.ShowLoadDialog();
        }
    }
}