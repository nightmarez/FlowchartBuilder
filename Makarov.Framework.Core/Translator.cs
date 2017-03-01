// <copyright file="Translator.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-02-11</date>
// <summary>Переводчик.</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Переводчик.
    /// </summary>
    public sealed class Translator: ICloneable, IDisposable, IComparable<Translator>
    {
        #region Exceptions
        /// <summary>
        /// Исключение в переводчике.
        /// </summary>
        public class TranslatorException : MakarovFrameworkException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="message">Сообщение.</param>
            public TranslatorException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Слово не найдено в словаре.
        /// </summary>
        public sealed class WordNotExistsExcetion : TranslatorException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="word">Слово.</param>
            public WordNotExistsExcetion(string word)
                : base(string.Format("Word '{0}' not exists.", word))
            { }
        }

        /// <summary>
        /// Словарь не загружался из файла (создан программно).
        /// </summary>
        public sealed class DictionaryNotLoadedFromFileException : TranslatorException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            public DictionaryNotLoadedFromFileException()
                : base("Dictionary not loaded from file.")
            { }
        }

        /// <summary>
        /// Неправильная нода.
        /// </summary>
        public sealed class InvalidNodeTypeException : TranslatorException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="t">Тип ноды.</param>
            public InvalidNodeTypeException(XmlNodeType t)
                : base(string.Format("Invalid node '{0}'.", t))
            { }
        }

        /// <summary>
        /// Неправильное имя ноды.
        /// </summary>
        public sealed class InvalidNodeNameException : TranslatorException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="nodeName">Имя ноды.</param>
            public InvalidNodeNameException(string nodeName)
                : base(string.Format("Invalid node name '{0}'", nodeName))
            { }
        }

        /// <summary>
        /// Несовместимые языки переводчиков.
        /// </summary>
        public sealed class IncompatibleLanguagesException : TranslatorException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            public IncompatibleLanguagesException()
                : base("Incompatible languages.")
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Словарь.
        /// </summary>
        private readonly List<KeyValuePair<string, string>> _words = new List<KeyValuePair<string, string>>();
        #endregion

        #region Properties
        /// <summary>
        /// Язык.
        /// </summary>
        public string Language
        {
            get; private set;
        }

        /// <summary>
        /// Количество слов в словаре.
        /// </summary>
        public int Count
        {
            get { return _words.Count; }
        }

        /// <summary>
        /// Загружен ли словарь из файла.
        /// </summary>
        public bool IsLoadedFromFile
        {
            get; private set;
        }

        /// <summary>
        /// Из какого файла загружен словарь.
        /// </summary>
        public string FileName
        {
            get; private set;
        }

        /// <summary>
        /// Доступ к переводам.
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <returns>Перевод.</returns>
        public string this[string word]
        {
            get { return GetTranslation(word); }
            set { SetTranslation(word, value); }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Очистить словарь.
        /// </summary>
        public void Clear()
        {
            _words.Clear();
        }

        /// <summary>
        /// Добавить слово и перевод.
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <param name="translation">Перевод.</param>
        public void Add(string word, string translation)
        {
            if (word == null)
                throw new NullStringValueException("word");

            if (translation == null)
                throw new NullStringValueException("translation");

            // Ищем слово в словаре.
            foreach (var kvp in _words)
                if (kvp.Key == word)
                {
                    // Если нашли слово, удаляем его.
                    _words.Remove(kvp);
                    break;
                }

            //Добавляем слово и перевод.
            _words.Add(new KeyValuePair<string, string>(word, translation));
        }

        /// <summary>
        /// Есть ли слово в словаре.
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <returns>Есть ли слово в словаре.</returns>
        public bool IsWordExists(string word)
        {
            if (word == null)
                throw new NullStringValueException("word");

            return _words.Any(kvp => kvp.Key == word);
        }

        /// <summary>
        /// Удалить слово из словаря.
        /// </summary>
        /// <param name="word">Слово.</param>
        public void Delete(string word)
        {
            if (word == null)
                throw new NullStringValueException("word");

            foreach (var kvp in _words)
                if (kvp.Key == word)
                {
                    _words.Remove(kvp);
                    return;
                }

            throw new WordNotExistsExcetion(word);
        }

        /// <summary>
        /// Получить перевод.
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <returns>Перевод.</returns>
        public string GetTranslation(string word)
        {
            if (word == null)
                throw new NullStringValueException("word");

            foreach (var kvp in _words)
                if (kvp.Key == word)
                    return kvp.Value;

            return "+ " + word;
        }

        /// <summary>
        /// Установить перевод.
        /// </summary>
        /// <param name="word">Слово.</param>
        /// <param name="translation">Перевод.</param>
        public void SetTranslation(string word, string translation)
        {
            Add(word, translation);
        }

        /// <summary>
        /// Перезагрузить из файла.
        /// </summary>
        public void Reload(string schemaFile)
        {
            if (!IsLoadedFromFile)
                throw new DictionaryNotLoadedFromFileException();

            Load(FileName, schemaFile);
        }

        /// <summary>
        /// Загрузить (дозагрузить) слова из файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public void Load(string fileName, string schemaFile)
        {
            // Загружаем xml-документ.
            var doc = new XmlDocument();
            doc.Load(fileName);

            if (!Settings.Environment.IsMono)
            {
                // TODO: падает под Mono
                doc.Schemas.Add(Settings.Xml.Namespaces.Base, schemaFile);
                doc.Validate(delegate(object sender, ValidationEventArgs args) { throw args.Exception; });
            }

            var mainNode = doc.GetElementsByTagName(Settings.Xml.Tags.Translation)[0];

            string lang = mainNode.Attributes[Settings.Xml.Attributes.Language].Value;
            if (string.IsNullOrEmpty(Language))
                Language = lang;
            else if (Language != lang)
                throw new IncompatibleLanguagesException();
            else
                Language = lang;

            foreach (XmlNode node in mainNode)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    if (node.Name == Settings.Xml.Tags.Word)
                        Add(node.Attributes[Settings.Xml.Attributes.Key].Value,
                            node.InnerText);
                }
                else if (node.NodeType != XmlNodeType.Comment)
                    throw new InvalidNodeTypeException(node.NodeType);
            }

            IsLoadedFromFile = true;
            FileName = fileName;
        }

        /// <summary>
        /// Сохранить в файл.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public void Save(string fileName)
        {
            var doc = new XmlDocument();

            var mainNode = doc.CreateElement(Settings.Xml.Tags.Translation);
            var langAttrib = doc.CreateAttribute(Settings.Xml.Attributes.Language);
            langAttrib.Value = Language;
            mainNode.Attributes.Append(langAttrib);

            foreach (var kvp in _words)
            {
                var node = doc.CreateElement(Settings.Xml.Tags.Word);
                var wordAttrib = doc.CreateAttribute(Settings.Xml.Attributes.Key);
                wordAttrib.Value = kvp.Key;
                node.Attributes.Append(wordAttrib);
                node.InnerText = kvp.Value;
                mainNode.AppendChild(node);
            }

            doc.AppendChild(mainNode);
            doc.Save(fileName);
        }

        /// <summary>
        /// Создать клон переводчика.
        /// </summary>
        /// <returns>Клон переводчика.</returns>
        public object Clone()
        {
            return new Translator(this);
        }

        /// <summary>
        /// Освободить ресурсы.
        /// </summary>
        public void Dispose()
        {
            _words.Clear();
            IsLoadedFromFile = false;
            FileName = null;
            Language = null;
        }

        /// <summary>
        /// Сравнить два переводчика.
        /// </summary>
        /// <param name="other">Переводчик.</param>
        /// <returns>Результат сравнения.</returns>
        public int CompareTo(Translator other)
        {
            if (((object)other) == null) return 1;

            if (Language != other.Language)
                return (Language ?? "").CompareTo(other.Language ?? "");

            if (other._words.Count != _words.Count)
                return _words.Count - other._words.Count;

            if (other._words.Any(kvp => !IsWordExists(kvp.Key) || this[kvp.Key] != kvp.Value))
                return 1;
            
            return 0;
        }

        /// <summary>
        /// Объединить два переводчика.
        /// </summary>
        /// <param name="t1">Переводчик.</param>
        /// <param name="t2">Переводчик.</param>
        /// <returns>Объекдинённый переводчик.</returns>
        public static Translator Compose(Translator t1, Translator t2)
        {
            if (t1.Language != t2.Language)
                throw new IncompatibleLanguagesException();

            var tmp = CreateEmptyTranslator(t1.Language);

            foreach (var kvp in t1._words)
                tmp.Add(kvp.Key, kvp.Value);

            foreach (var kvp in t2._words)
                tmp.Add(kvp.Key, kvp.Value);

            return tmp;
        }

        /// <summary>
        /// Создать пустой переводчик.
        /// </summary>
        /// <param name="language">Язык.</param>
        /// <returns>Переводчик.</returns>
        public static Translator CreateEmptyTranslator(string language)
        {
            if (language == null)
                throw new NullStringValueException("language");

            return new Translator {Language = language};
        }

        /// <summary>
        /// Эквивалентны ли объекты.
        /// </summary>
        /// <param name="o">Объект.</param>
        /// <returns>Эквивалентны ли объекты.</returns>
        public override bool Equals(Object o)
        {
            if (o.GetType() != GetType()) return false;
            return CompareTo((Translator) o) == 0;
        }

        /// <summary>
        /// Хеш-код.
        /// </summary>
        /// <returns>Хеш-код.</returns>
        public override int GetHashCode()
        {
            return Language.GetHashCode();
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        private Translator()
        { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public Translator(string fileName, string schemaFile)
        {
            Load(fileName, schemaFile);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="other">Переводчик, копию которого нужно создать.</param>
        public Translator(Translator other)
        {
            foreach (var kvp in other._words)
                _words.Add(kvp);

            IsLoadedFromFile = other.IsLoadedFromFile;
            FileName = other.FileName;
            Language = other.Language;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Объекдинить два переводчика в один.
        /// </summary>
        /// <param name="t1">Переводчик.</param>
        /// <param name="t2">Переводчик.</param>
        /// <returns>Объединённый переводчик.</returns>
        public static Translator operator +(Translator t1, Translator t2)
        {
            return Compose(t1, t2);
        }

        /// <summary>
        /// Добавить слово с переводом в переводчик.
        /// </summary>
        /// <param name="t">Переводчик.</param>
        /// <param name="kvp">Слово с переводом.</param>
        /// <returns>Переводчик.</returns>
        public static Translator operator +(Translator t, KeyValuePair<string, string> kvp)
        {
            t.Add(kvp.Key, kvp.Value);
            return t;
        }

        /// <summary>
        /// Добавить слово с переводом в переводчик.
        /// </summary>
        /// <param name="kvp">Слово с переводом.</param>
        /// <param name="t">Переводчик.</param>
        /// <returns>Переводчик.</returns>
        public static Translator operator +(KeyValuePair<string, string> kvp, Translator t)
        {
            t.Add(kvp.Key, kvp.Value);
            return t;
        }

        /// <summary>
        /// Удалить слово из переводчика.
        /// </summary>
        /// <param name="t">Переводчик.</param>
        /// <param name="word">Слово.</param>
        /// <returns>Переводчик.</returns>
        public static Translator operator -(Translator t, string word)
        {
            t.Delete(word);
            return t;
        }

        /// <summary>
        /// Сравнить содержимое двух переводчиков.
        /// </summary>
        /// <param name="t1">Переводчик.</param>
        /// <param name="t2">Переводчик.</param>
        /// <returns>Равно ли содержимое переводчиков.</returns>
        public static bool operator ==(Translator t1, Translator t2)
        {
            if (((object)t1) == null && ((object)t2) == null) return true;
            if (((object)t1) == null || ((object)t2) == null) return false;
            return t1.CompareTo(t2) == 0;
        }

        /// <summary>
        /// Сравнить содержимое двух переводчиков.
        /// </summary>
        /// <param name="t1">Переводчик.</param>
        /// <param name="t2">Переводчик.</param>
        /// <returns>Не равно ли содержимое переводчиков.</returns>
        public static bool operator !=(Translator t1, Translator t2)
        {
            return !(t1 == t2);
        }
        #endregion
    }
}