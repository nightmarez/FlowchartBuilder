// <copyright file="SettingsExport.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-06-05</date>
// <summary>Экспорт/иморт настроек программы.</summary>

using System;
using System.Windows.Forms;
using System.Xml;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Экспорт/иморт настроек программы.
    /// </summary>
    public static class SettingsExport
    {
        /// <summary>
        /// Отображает диалог сохранения.
        /// </summary>
        public static void ShowSaveDialog()
        {
            var dlg = new SaveFileDialog
                          {
                              DefaultExt = Settings.Environment.AppSettingsExtension,
                              CheckFileExists = false,
                              CheckPathExists = true,
                              Filter = string.Format("(*.{0})|*.{0}", Settings.Environment.AppSettingsExtension),
                              FileName = "FlowchartBuilder." + Settings.Environment.AppSettingsExtension
                          };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Save(dlg.FileName);
            }
        }

        /// <summary>
        /// Отображает диалог загрузки.
        /// </summary>
        public static void ShowLoadDialog()
        {
            var dlg = new OpenFileDialog
                          {
                              DefaultExt = Settings.Environment.AppSettingsExtension,
                              CheckFileExists = true,
                              CheckPathExists = true,
                              Filter = string.Format("(*.{0})|*.{0}", Settings.Environment.AppSettingsExtension)
                          };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Load(dlg.FileName);
            }
        }

        /// <summary>
        /// Сохраняет настройки в файл.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public static void Save(string fileName)
        {
            var doc = new XmlDocument();

            var mainNode = (XmlElement)doc.CreateNode(
                XmlNodeType.Element,
                Settings.Xml.Tags.Settings,
                Settings.Xml.Namespaces.Base);

            

            doc.AppendChild(mainNode);

            doc.Save(fileName);
        }

        /// <summary>
        /// Загружает настройки из файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public static void Load(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}