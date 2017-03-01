// <copyright file="AboutCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - о программе.</summary>

using Makarov.FlowchartBuilder.Forms;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - о программе.
    /// </summary>
    public sealed class AboutCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            Core.Instance.Windows.GetWindow<AboutForm>().ShowDialog(Core.Instance.MainWindow);
        }
    }
}