// <copyright file="SaveCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-04</date>
// <summary>Команда - сохранить файл.</summary>

using System.Windows.Forms;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - сохранить файл.
    /// </summary>
    public sealed class SaveCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "FlowchartBuilder Documents|*.mfb";

                if (dialog.ShowDialog(Core.Instance.MainWindow) == DialogResult.OK)
                {
                    Core.Instance.CurrentDocument.Save(dialog.FileName);
                }
            }
        }
    }
}