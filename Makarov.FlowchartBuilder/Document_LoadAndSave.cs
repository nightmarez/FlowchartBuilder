// <copyright file="Document_LoadAndSave.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-17</date>
// <summary>Документ - загрузка и сохранение.</summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Makarov.FlowchartBuilder.Glyphs;
using Makarov.FlowchartBuilder.Sheets;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Документ.
    /// </summary>
    public sealed partial class Document
    {
        /// <summary>
        /// Загрузить документ.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public void Load(string fileName)
        {
            // TODO:
            Name = Path.GetFileNameWithoutExtension(fileName);

            // Создаёт xml-документ.
            var doc = new XmlDocument();

            // Загружаем xml-документ.
            doc.Load(fileName);

            // Получаем главную ноду.
            var mainNode = doc.DocumentElement;

            // Главная нода не найдена - бросаем исключение.
            if (mainNode == null)
                throw new MainDocumentNodeNotFoundException();

            // Атрибут версии документа.
            var docVersionAttrib = mainNode.GetAttribute(Settings.Xml.Attributes.ProgramVersion);
            var version = new Version(docVersionAttrib);
            // TODO: использовать версию в конвертерах документов со старых версий.

            // Получаем ноду листа.
            XmlElement sheetNode = null;
            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (XmlNode node in mainNode.ChildNodes)
                if (node.Name == Settings.Xml.Tags.Sheet && node is XmlElement)
                {
                    sheetNode = (XmlElement)node;
                    break;
                }
            // ReSharper restore LoopCanBeConvertedToQuery

            // Нода листа не найдена - бросаем исключение.
            if (sheetNode == null)
                throw new SheetNodeNotFoundException();

            // Создаём лист и загружаем его параметры...
            // ----------------------------------------------------

            // Атрибут класса листа.
            var sheetClassAttrib = sheetNode.Attributes[Settings.Xml.Attributes.SheetClassName];

            // Атрибуты размеров листа.
            var sheetWidthAttrib = sheetNode.Attributes[Settings.Xml.Attributes.SheetWidth];
            var sheetHeightAttrib = sheetNode.Attributes[Settings.Xml.Attributes.SheetHeight];

            // Пытаемся получить тип листа.
            var sheetType = Type.GetType(sheetClassAttrib.Value);

            // Лист.
            DocumentSheet = null;

            // Если тип является подтипом базового класса листа,
            // создаём экземпляр листа.
            if (sheetType.IsSubclassOf(typeof(Sheet)))
            {
                // Если размеры заданы, передаём их в конструктор.
                if (sheetWidthAttrib != null && sheetHeightAttrib != null)
                {
                    var fixedSheet = (FixedSheet)Activator.CreateInstance(sheetType);
                    fixedSheet.Width = float.Parse(sheetWidthAttrib.Value);
                    fixedSheet.Height = float.Parse(sheetHeightAttrib.Value);
                    DocumentSheet = fixedSheet;
                }
                else
                    DocumentSheet = (Sheet)Activator.CreateInstance(sheetType);
            }

            // Листа нет - бросаем исключение.
            if (DocumentSheet == null)
                throw new SheetNotLoadedException();

            // Загружаем глифы...
            foreach (XmlNode glyphNode in sheetNode)
            {
                // Атрибут типа глифа.
                var glyphTypeAttrib = glyphNode.Attributes[Settings.Xml.Attributes.GlyphClassName];

                // Получаем тип глифа.
                var glyphType = Type.GetType(glyphTypeAttrib.Value);

                // Если тип не найден, ищем в плагинах.
                if (glyphType == null)
                {
                    bool finded = false; // Найден ли тип глифа.

                    // Проходим по всем загруженным плагинам...
                    foreach (var kvp in Core.Instance.PluginsLoader.LoadedAssemblies)
                    {
                        Assembly asm = kvp.Value;

                        // Проходишь по всем типам в загруженных плагинах...
                        foreach (var asmType in asm.GetTypes())
                        {
                            // Если имя текущего типа из плагина совпадает с именем
                            // глифа из атрибута, значет подходящий тип найден.
                            if (asmType.FullName == glyphTypeAttrib.Value)
                            {
                                glyphType = asmType;
                                finded = true;
                            }
                        }

                        // Тип найден - прекращаем проход по плагинам для текущего типа.
                        if (finded)
                            break;
                    }
                }

                // Тип глифа не найден - бросаем исключение.
                if (glyphType == null)
                    throw new GlyphTypeNotFoundException(glyphTypeAttrib.Value);

                // Если тип является подтипом базового класса глифов,
                // создаём экземпляр этого типа.
                if (glyphType.IsSubclassOf(typeof(AbstractGlyph)))
                {
                    var glyph = (AbstractGlyph)Activator.CreateInstance(glyphType);
                    if (glyph.Deserialize(glyphNode).ToList().Count > 0)
                        MessageBox.Show(@"Document upgraded.");
                    DocumentSheet.Add(glyph);
                }
                else
                    throw new GlyphTypeNotFoundException(glyphTypeAttrib.Value);
            }

            // Создаём историю и сохраняем в ней глифы листа.
            var historyEvents = new List<HistoryEvent>();

            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (AbstractGlyph glyph in DocumentSheet.Glyphs)
            {
                var snapshot = new GlyphSnapshot(glyph);
                historyEvents.Add(new HistoryEventCreateGlyph(snapshot));
            }
            // ReSharper restore LoopCanBeConvertedToQuery

            DocumentHistory = new History(historyEvents);
            // ----------------------------------------------------
        }

        /// <summary>
        /// Сохранить документ.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public void Save(string fileName)
        {
            // Создаём xml-документ.
            var doc = new XmlDocument();

            // Создаём главную ноду.
            var mainNode = (XmlElement)doc.CreateNode(
                XmlNodeType.Element,
                Settings.Xml.Tags.FlowchartBuilderDocument,
                Settings.Xml.Namespaces.Base);

            // Сохраняем версию.
            var versionAttrib = doc.CreateAttribute(Settings.Xml.Attributes.ProgramVersion);
            versionAttrib.Value = Assembly.GetEntryAssembly().GetName().Version.ToString();
            mainNode.Attributes.Append(versionAttrib);

            // Создаём ноду листа.
            var sheetNode = (XmlElement)doc.CreateNode(
                XmlNodeType.Element,
                Settings.Xml.Tags.Sheet,
                Settings.Xml.Namespaces.Base);

            // Сохраняем параметры листа...
            // ----------------------------------------------------
            var sheet = DocumentSheet;

            // Класс листа.
            var sheetClassAttrib = doc.CreateAttribute(
                Settings.Xml.Attributes.SheetClassName,
                Settings.Xml.Namespaces.Base);
            sheetClassAttrib.Value = sheet.GetType().ToString();
            sheetNode.Attributes.Append(sheetClassAttrib);

            // Ширина и высота в миллиметрах, если нужны.
            if (sheet is FixedSheet)
            {
                var fixedSheet = (FixedSheet)sheet;

                var sheetWidthAttrib = doc.CreateAttribute(Settings.Xml.Attributes.SheetWidth);
                sheetWidthAttrib.Value = fixedSheet.Width.ToString();
                sheetNode.Attributes.Append(sheetWidthAttrib);

                var sheetHeightAttrib = doc.CreateAttribute(Settings.Xml.Attributes.SheetHeight);
                sheetHeightAttrib.Value = fixedSheet.Height.ToString();
                sheetNode.Attributes.Append(sheetHeightAttrib);
            }
            // ----------------------------------------------------

            // Сохраняем все глифы листа...
            foreach (var glyph in sheet.Glyphs)
                sheetNode.AppendChild(glyph.Serialize(doc));

            // Добавляем ноду листа в документ.
            mainNode.AppendChild(sheetNode);

            // Добавляем главную ноду в документ.
            doc.AppendChild(mainNode);

            // Сохраняем xml-документ.
            doc.Save(fileName);

            // Сохраняем снимки глифов.
            SaveSnapshots(GetSnapshotsFromSheet());
        }
    }
}