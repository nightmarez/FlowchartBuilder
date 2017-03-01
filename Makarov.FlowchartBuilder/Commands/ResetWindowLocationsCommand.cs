// <copyright file="ResetWindowLocationsCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-22</date>
// <summary>Команда - выставить дефолтное расположение окон.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выставить дефолтное расположение окон.
    /// </summary>
    public sealed class ResetWindowLocationsCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            Core.Instance.SetDefaultWindowsPos();
        }
    }
}