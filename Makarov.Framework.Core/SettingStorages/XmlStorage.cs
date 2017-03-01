// <copyright file="XmlStorage.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-25</date>
// <summary>Класс для хранения настроек в XML.</summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Makarov.Framework.Core.SettingStorages
{
    /// <summary>
    /// Класс для хранения настроек в XML.
    /// </summary>
    public sealed class XmlStorage : IDict<string>
    {
        private readonly StringDictionary<string> _dict = new StringDictionary<string>();

        private string _settingsFileName;

        public XmlStorage(string settingsFileName)
        {
            _settingsFileName = settingsFileName;
            LoadData();
        }

        /// <summary>
        /// Доступ к элементам по ключу.
        /// </summary>
        public string this[string x]
        {
            get
            {
                if (string.IsNullOrEmpty(x))
                    return null;

                if (!_dict.ContainsKey(x))
                    return null;

                return _dict[x];
            }

            set
            {
                if (string.IsNullOrEmpty(x))
                    return;

                if (!_dict.ContainsKey(x))
                {
                    _dict.Add(x, value);
                    SaveData();
                    return;
                }

                _dict[x] = value;
                SaveData();
            }
        }

        /// <summary>
        /// Есть ли ключ в словаре.
        /// </summary>
        public bool IsKeyExists(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            return _dict.ContainsKey(key);
        }

        /// <summary>
        /// Имя файла настроек.
        /// </summary>
        public string SettingsFileName
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    _settingsFileName);
            }
        }

        /// <summary>
        /// Загружает данные.
        /// </summary>
        public void LoadData()
        {
            if (!File.Exists(SettingsFileName))
                return;

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(SettingsFileName);

            XmlNode mainNode = xmlDoc.DocumentElement;

            if (mainNode != null)
                foreach (XmlNode node in mainNode)
                {
                    if (node.Attributes == null)
                        continue;

                    string key = node.Attributes["name"].Value;
                    string val = node.InnerText;
                    _dict.Add(key, val);
                }
        }

        /// <summary>
        /// Сохраняет данные.
        /// </summary>
        public void SaveData()
        {
            var xmlDoc = new XmlDocument();
            XmlNode mainNode = xmlDoc.CreateElement("Settings");

            foreach (KeyValuePair<string, string> kvp in _dict)
            {
                XmlNode node = xmlDoc.CreateElement("Key");
                XmlAttribute attrib = xmlDoc.CreateAttribute("name");
                attrib.Value = kvp.Key;

                if (node.Attributes == null)
                    continue;

                node.Attributes.Append(attrib);
                node.InnerText = kvp.Value;
                mainNode.AppendChild(node);
            }

            xmlDoc.AppendChild(mainNode);
            xmlDoc.Save(SettingsFileName);
        }
    }
}