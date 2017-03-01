// <copyright file="ExportImageCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-05-22</date>
// <summary>Команда - экспортировать изображение.</summary>

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - экспортировать изображение.
    /// </summary>
    public sealed class ExportImageCommand : Command
    {
        /// <summary>
        /// Предыдущее имя файла.
        /// </summary>
        public static string _oldFileName = "Flowchart.png";

        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            if (Settings.Environment.Registered)
            {
                // Если документа не существует, бросаем исключение.
                if (!Core.Instance.IsDocumentsExists)
                    throw new Core.CurrentDocumentNotExistsException();

                using (var dialog = new SaveFileDialog())
                {
                    dialog.Filter = @"*.png|*.png|*.bmp|*.bmp";

                    if (string.IsNullOrEmpty(dialog.FileName))
                        dialog.FileName = _oldFileName;

                    DialogResult result = dialog.ShowDialog(Core.Instance.MainWindow);
                    if (result == DialogResult.OK || result == DialogResult.Yes)
                    {
                        string fileName = dialog.FileName;
                        string ext = Path.GetExtension(fileName).ToLower();
                        ImageFormat format = null;

                        if (!string.IsNullOrEmpty(ext))
                        {
                            switch (ext)
                            {
                                case ".bmp":
                                    format = ImageFormat.Bmp;
                                    break;

                                case ".png":
                                    format = ImageFormat.Png;
                                    break;
                            }
                        }

                        if (format != null)
                        {
                            Core.Instance.CurrentDocument.DocumentSheet.DeselectAllGlyphs();
                            Core.Instance.Redraw();

                            using (Bitmap bmp = Core.Instance.CurrentDocument.DocumentSheet.DrawUnscaled())
                                bmp.Save(fileName, format);

                            _oldFileName = dialog.FileName;
                        }
                        else
                            MessageBox.Show(Core.Instance.CurrentTranslator["Msg_InvalidImageExtension"],
                                            Core.Instance.CurrentTranslator["Msg_Error"],
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(Core.Instance.CurrentTranslator["Msg_FunctionDisabledInNonRegisteredProgram"],
                                Core.Instance.CurrentTranslator["Msg_Warning"],
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
    }
}