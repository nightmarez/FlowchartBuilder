// <copyright file="OpenCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-04</date>
// <summary>Команда - открыть файл.</summary>

using System.Windows.Forms;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - открыть файл.
    /// </summary>
    public sealed class OpenCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = @"FlowchartBuilder Documents|*.mfb";

                if (dialog.ShowDialog(Core.Instance.MainWindow) == DialogResult.OK)
                {
                    Core.Instance.AddDocument(new Document(dialog.FileName));
                    GetInstance("ActualSizeCommand").Run();
                    Core.Instance.Redraw();
                }
            }
        }
    }
}