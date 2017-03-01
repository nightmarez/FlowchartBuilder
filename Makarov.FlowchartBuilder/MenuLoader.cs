// <copyright file="MenuLoader.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-IV-20</date>
// <summary>Загрузчик меню.</summary>

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using Makarov.FlowchartBuilder.Commands;
using Makarov.FlowchartBuilder.Forms;
using Makarov.Framework.Components;
using attrs = Makarov.FlowchartBuilder.Settings.Xml.Attributes;
using tags = Makarov.FlowchartBuilder.Settings.Xml.Tags;
using BaseSettings = Makarov.Framework.Core.Settings;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Загрузчик меню.
    /// </summary>
    public static class MenuLoader
    {
        #region Public methods
        /// <summary>
        /// Загружает главное меню.
        /// </summary>
        /// <param name="wnd">Главное окно.</param>
        public static void LoadMainMenu(MainForm wnd)
        {
            // Загружаем xml-документ.
            var doc = new XmlDocument();
            doc.Load(Settings.Files.MainMenuDefinition);
            doc.Schemas.Add(Settings.Xml.Namespaces.Base, Settings.Files.MainMenuSchema);
            doc.Validate(delegate(object sender, ValidationEventArgs args) { throw args.Exception; });

            // Загружаем главную ноду.
            var mainNode = doc.GetElementsByTagName(Settings.Xml.Tags.MainMenu)[0];

            // Перебираем все ноды.
            foreach (XmlNode node in mainNode)
            {
                // Если это элемент, обрабатываем его.
                if (node.NodeType == XmlNodeType.Element && node.Name == Settings.Xml.Tags.Menu)
                {
                    wnd.MainMenu.Items.Add(CreateSubmenu(node));
                }
                // Если это не элемент и не комментарий, бросаем исключение.
                else if (node.NodeType != XmlNodeType.Comment)
                    throw new InvalidNodeTypeException(node.NodeType.ToString());
            }
        }

        /// <summary>
        /// Загружает вторичное меню.
        /// </summary>
        /// <param name="wnd">Главное окно.</param>
        public static void LoadSecondaryMenu(MainForm wnd)
        {
            // Загружаем xml-документ.
            var doc = new XmlDocument();
            doc.Load(Settings.Files.SecondaryMenuDefinition);
            doc.Schemas.Add(Settings.Xml.Namespaces.Base, Settings.Files.SecondaryMenuSchema);
            doc.Validate(delegate(object sender, ValidationEventArgs args) { throw args.Exception; });

            // Загружаем главную ноду.
            var mainNode = doc.GetElementsByTagName(Settings.Xml.Tags.SecondaryMenu)[0];

            // Перебираем все ноды.
            foreach (XmlNode node in mainNode)
            {
                // Если это элемент, обрабатываем его.
                if (node.NodeType == XmlNodeType.Element)
                {
                    switch (node.Name)
                    {
                        // Элемент меню, вызывающий команду.
                        case Settings.Xml.Tags.Command:
                            wnd.SecondaryMenu.Items.Add(CreateButton(node));
                            break;

                        // Разделитель.
                        case Settings.Xml.Tags.Separator:
                            wnd.SecondaryMenu.Items.Add(CreateSeparator());
                            break;

                        // Неправильное имя - бросаем исключение.
                        default:
                            throw new InvalidNodeNameException(node.Name);
                    }
                }
                // Если это не элемент и не комментарий, бросаем исключение.
                else if (node.NodeType != XmlNodeType.Comment)
                    throw new InvalidNodeTypeException(node.NodeType.ToString());
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Создаёт подменю главного меню.
        /// </summary>
        /// <param name="node">Xml-нода.</param>
        /// <returns>Подменю главного меню.</returns>
        private static ToolStripItem CreateSubmenu(XmlNode node)
        {
            var item = new ToolStripMenuItem(
                Core.Instance.CurrentTranslator[node.Attributes[Settings.Xml.Attributes.Caption].Value]);

            // Если указано имя иконки, загружаем её.
            if (node.Attributes[Settings.Xml.Attributes.Icon] != null)
            {
                string icon = node.Attributes[Settings.Xml.Attributes.Icon].Value;
                if (!string.IsNullOrEmpty(icon))
                {
                    item.ImageScaling = ToolStripItemImageScaling.None;
                    item.ImageTransparentColor = Color.White;
                    item.Image = Core.Instance.Icons[icon];
                }
            }

            foreach (XmlNode subNode in node)
            {
                if (subNode.NodeType == XmlNodeType.Element)
                {
                    switch (subNode.Name)
                    {
                        // Подменю.
                        case Settings.Xml.Tags.Menu:
                            item.DropDownItems.Add(CreateSubmenu(subNode));
                            break;

                        // Элемент меню, вызывающий команду.
                        case Settings.Xml.Tags.Command:
                            item.DropDownItems.Add(CreateCommand(subNode));
                            break;

                        // Разделитель.
                        case Settings.Xml.Tags.Separator:
                            item.DropDownItems.Add(CreateSeparator());
                            break;

                        // Неправильное имя - бросаем исключение.
                        default:
                            throw new InvalidNodeNameException(node.Name);
                    }
                }
                else if (subNode.NodeType != XmlNodeType.Comment)
                    throw new InvalidNodeTypeException(subNode.NodeType.ToString());
            }

            return item;
        }

        /// <summary>
        /// Создаёт комманду главного меню.
        /// </summary>
        /// <param name="node">Xml-нода.</param>
        /// <returns>Комманда главного меню.</returns>
        private static ToolStripItem CreateCommand(XmlNode node)
        {
            // Имя класса команды.
            var cmdValue = node.Attributes[Settings.Xml.Attributes.Class].Value;

            // Если это не простая команда, а составная, строим подменю.
            if (cmdValue[0] == '@')
            {
                cmdValue = cmdValue.Substring(1);

                // Создаём подменю.
                var submenu = new ToolStripMenuItem(
                Core.Instance.CurrentTranslator[node.Attributes[Settings.Xml.Attributes.Caption].Value]);

                // Если указано имя иконки, загружаем её.
                if (node.Attributes[Settings.Xml.Attributes.Icon] != null)
                {
                    string icon = node.Attributes[Settings.Xml.Attributes.Icon].Value;
                    if (!string.IsNullOrEmpty(icon))
                    {
                        submenu.ImageScaling = ToolStripItemImageScaling.None;
                        submenu.ImageTransparentColor = Color.White;
                        submenu.Image = Core.Instance.Icons[icon];
                    }
                }

                foreach (var cmd in ((CommandWithSubcommands)Command.GetInstance(cmdValue)).Commands)
                {
                    var subitem = new ActiveToolStripMenuItem(
                        Core.Instance.CurrentTranslator[cmd],
                        string.Format("{0}@{1}", cmdValue, cmd));

                    // Вешаем обработчик мыши.
                    subitem.Click += (e, args) =>
                                         {
                                             var commandName = ((string) subitem.Tag).Split('@')[0];
                                             var commandParam = ((string) subitem.Tag).Split('@')[1];
                                             var instance = (CommandWithSubcommands) Command.GetInstance(commandName);
                                             instance.Run(commandParam);
                                         };

                    subitem.ImageScaling = ToolStripItemImageScaling.None;
                    subitem.ImageTransparentColor = Color.White;
                    subitem.ActiveIcon = ((CommandWithSubcommands)Command.GetInstance(cmdValue)).Icon(cmd);

                    submenu.DropDownItems.Add(subitem);
                }

                return submenu;
            }

            // Создаём экземпляр компонента (элемент меню).
            var item = new ActiveToolStripMenuItem(
                node.Attributes[Settings.Xml.Attributes.Caption] != null
                    ? Core.Instance.CurrentTranslator[node.Attributes[Settings.Xml.Attributes.Caption].Value]
                    : string.Empty,
                cmdValue);

            // Если указано имя иконки, загружаем её.
            if (node.Attributes[Settings.Xml.Attributes.Icon] != null)
            {
                string icon = node.Attributes[Settings.Xml.Attributes.Icon].Value;
                if (!string.IsNullOrEmpty(icon))
                {
                    item.ImageScaling = ToolStripItemImageScaling.None;
                    item.ImageTransparentColor = Color.White;
                    item.ActiveIcon = Core.Instance.Icons[icon];
                }
            }

            // Горячие клавиши.
            if (node.Attributes[Settings.Xml.Attributes.Shortcut] != null)
            {
                string shortcut = node.Attributes[Settings.Xml.Attributes.Shortcut].Value;
                if (!string.IsNullOrEmpty(shortcut))
                {
                    string[] parts = shortcut.Split('+');
                    Keys keys = 0;

                    foreach (string part in parts)
                    {
                        switch (part.ToLowerInvariant())
                        {
                            case "ctrl":
                                keys |= Keys.Control;
                                break;

                            case "alt":
                                keys |= Keys.Alt;
                                break;

                            case "shift":
                                keys |= Keys.Shift;
                                break;

                            default:
                                Keys parsed;
                                if (Enum.TryParse(part, out parsed))
                                    keys |= parsed;
                                else
                                    throw new FlowchartBuilderException(
                                        string.Format(@"Key '{0}' not parsed.", part)); 
                                break;
                        }
                    }

                    item.ShortcutKeys = keys;
                }
            }

            // Задаём, нажата ли кнопка.
            var checkedAttrib = node.Attributes[Settings.Xml.Attributes.Checked];
            item.ActiveChecked = checkedAttrib != null
                                     ? bool.Parse(checkedAttrib.Value)
                                     : false;

            // Задаём текст всплывающей подсказки.
            item.ActiveBaloon = node.Attributes[Settings.Xml.Attributes.ToolTip] != null
                                    ? Core.Instance.CurrentTranslator[node.Attributes[Settings.Xml.Attributes.ToolTip].Value]
                                    : string.Empty;

            // Задаём, активен ли элемент (по-умолчанию - активен).
            if (node.Attributes[Settings.Xml.Attributes.Enabled] != null)
                item.ActiveEnabled = bool.Parse(node.Attributes[Settings.Xml.Attributes.Enabled].Value);

            // Добавляем команду, связанную с данным элементом, в кеш.
            Command.GetInstance(cmdValue, item);

            // Вешаем обработчик мыши.
            item.Click += (e, args) => Command.GetInstance((string) item.Tag).Run();

            // Возвращаем элемент.
            return item;
        }

        /// <summary>
        /// Создаёт разделитель меню.
        /// </summary>
        /// <returns>Разделитель меню.</returns>
        private static ToolStripItem CreateSeparator()
        {
            return new ToolStripSeparator();
        }

        /// <summary>
        /// Создаёт кнопку вторичного меню.
        /// </summary>
        /// <param name="node">Xml-нода.</param>
        /// <returns>Кнопка вторичного меню.</returns>
        private static ToolStripItem CreateButton(XmlNode node)
        {
            // Создаём экземпляр компонента (кнопка вторичного меню).
            var item = new ActiveToolStripButton(
                node.Attributes[Settings.Xml.Attributes.Caption] != null
                    ? Core.Instance.CurrentTranslator[node.Attributes[Settings.Xml.Attributes.Caption].Value]
                    : string.Empty,
                node.Attributes[Settings.Xml.Attributes.Class].Value);

            // Если указано имя иконки, загружаем её.
            if (node.Attributes[Settings.Xml.Attributes.Icon] != null)
            {
                string icon = node.Attributes[Settings.Xml.Attributes.Icon].Value;
                if (!string.IsNullOrEmpty(icon))
                {
                    item.ImageScaling = ToolStripItemImageScaling.None;
                    item.ImageTransparentColor = Color.White;
                    item.ActiveIcon = Core.Instance.Icons[icon];
                }
            }

            // Задаём, нажата ли кнопка.
            var checkedAttrib = node.Attributes[Settings.Xml.Attributes.Checked];
            item.ActiveChecked = checkedAttrib != null
                                     ? bool.Parse(checkedAttrib.Value)
                                     : false;

            // Задаём текст всплывающей подсказки.
            item.ActiveBaloon = node.Attributes[Settings.Xml.Attributes.ToolTip] != null
                                    ? Core.Instance.CurrentTranslator[node.Attributes[Settings.Xml.Attributes.ToolTip].Value]
                                    : string.Empty;

            // Задаём, активен ли элемент (по-умолчанию - активен).
            if (node.Attributes[Settings.Xml.Attributes.Enabled] != null)
                item.ActiveEnabled = bool.Parse(node.Attributes[Settings.Xml.Attributes.Enabled].Value);

            // Добавляем команду, связанную с данным элементом, в кеш.
            Command.GetInstance((string) item.Tag, item);

            // Вешаем обработчик мыши.
            item.Click += (e, args) => Command.GetInstance((string) item.Tag).Run();

            // Возвращаем элемент.
            return item;
        }
        #endregion
    }
}
