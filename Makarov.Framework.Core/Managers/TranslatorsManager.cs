// <copyright file="TranslatorsManager.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-02-12</date>
// <summary>Менеджер переводчиков.</summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Makarov.Framework.Core.Managers
{
    /// <summary>
    /// Менеджер переводчиков.
    /// </summary>
    public sealed class TranslatorsManager : ICloneable, IDisposable, IComparable<TranslatorsManager>
    {
        #region Exceptions
        /// <summary>
        /// Исключение менеджера переводчиков.
        /// </summary>
        public class TranslatorsManagerException : MakarovFrameworkException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="message">Сообщение.</param>
            public TranslatorsManagerException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Перевод языка не найден.
        /// </summary>
        public sealed class LanguageNotExistsException : TranslatorsManagerException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="language">Язык.</param>
            public LanguageNotExistsException(string language)
                : base(string.Format("Language '{0}' not exists.", language))
            { }
        }

        /// <summary>
        /// Язык уже существует.
        /// </summary>
        public sealed class LanguageAlreadyExistsException : TranslatorsManagerException
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="language">Язык.</param>
            public LanguageAlreadyExistsException(string language)
                : base(string.Format("Language '{0}' already exists.", language))
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Список переводчиков.
        /// </summary>
        private readonly List<Translator> _translators = new List<Translator>();
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор.
        /// </summary>
        public TranslatorsManager()
        { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="t">Переводчик.</param>
        public TranslatorsManager(Translator t)
        {
            Add(t);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="folder">Папка с переводами.</param>
        /// <param name="schemaFile">Файл схемы переводов.</param>
        public TranslatorsManager(string folder, string schemaFile)
        {
            Task.WaitAll((from file in Directory.GetFiles(folder)
                          let ext = Path.GetExtension(file)
                          where ext != null && ext == ".xml"
                          select Task.Factory.StartNew(() => Add(new Translator(file, schemaFile)))).ToArray());
        }

        /// <summary>
        /// Конструктор (создаёт клон существующего менеджера).
        /// </summary>
        /// <param name="other">Менеджер переводов.</param>
        public TranslatorsManager(TranslatorsManager other)
        {
            foreach (var t in other._translators)
                Add(t);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Создать клон менеджера.
        /// </summary>
        /// <returns>Клон менеджера.</returns>
        public object Clone()
        {
            return new TranslatorsManager(this);
        }

        /// <summary>
        /// Освободить ресурсы.
        /// </summary>
        public void Dispose()
        {
            lock (_translators)
                _translators.Clear();
        }

        /// <summary>
        /// Сравнить два менеджера переводчиков.
        /// </summary>
        /// <param name="tm">Менеджер переводчиков.</param>
        /// <returns>Результат сравнения.</returns>
        public int CompareTo(TranslatorsManager tm)
        {
            if (((object) tm) == null) return 1;
            if (Count != tm.Count) return Count - tm.Count;

            lock (_translators)
                foreach (var t in _translators)
                    if (!tm.IsLanguageExists(t.Language))
                        return 1;
                    else
                    {
                        int diff = tm[t.Language].CompareTo(t);
                        if (diff != 0)
                            return diff;
                    }

            return 0;
        }

        /// <summary>
        /// Загрузить переводы из указанной директории.
        /// </summary>
        /// <param name="folder">Директория с файлами.</param>
        /// <param name="schemaFile">Файл схемы переводов.</param>
        /// <returns>Менеджер переводчиков.</returns>
        public static TranslatorsManager LoadFromFolder(string folder, string schemaFile)
        {
            return new TranslatorsManager(folder, schemaFile);
        }

        /// <summary>
        /// Есть ли перевод для данного языка.
        /// </summary>
        /// <param name="language">Язык.</param>
        /// <returns>Есть ли перевод для данного языка.</returns>
        public bool IsLanguageExists(string language)
        {
            lock (_translators)
                if (_translators.Any(t => t.Language == language))
                    return true;

            return false;
        }

        /// <summary>
        /// Получить переводчик.
        /// </summary>
        /// <param name="language">Язык.</param>
        /// <returns>Переводчик.</returns>
        public Translator GetTranslator(string language)
        {
            if (language == null)
                throw new NullStringValueException("language");

            lock (_translators)
                foreach (var t in _translators)
                    if (t.Language == language)
                        return t;

            throw new LanguageNotExistsException(language);
        }

        /// <summary>
        /// Добавить переводчик в менеджер.
        /// </summary>
        /// <param name="t">Переводчик.</param>
        public void Add(Translator t)
        {
            if (IsLanguageExists(t.Language))
                throw new LanguageAlreadyExistsException(t.Language);

            lock (_translators)
                _translators.Add(t);
        }

        /// <summary>
        /// Удалить язык.
        /// </summary>
        /// <param name="language">Язык.</param>
        public void Delete(string language)
        {
            lock (_translators)
                foreach (var t in _translators)
                    if (t.Language == language)
                    {
                        _translators.Remove(t);
                        return;
                    }

            throw new LanguageNotExistsException(language);
        }

        /// <summary>
        /// Эквивалентны ли объекты.
        /// </summary>
        /// <param name="o">Объект.</param>
        /// <returns>Эквивалентны ли объекты.</returns>
        public override bool Equals(Object o)
        {
            if (o == null) return false;
            if (o.GetType() != GetType()) return false;
            return CompareTo((TranslatorsManager)o) == 0;
        }

        /// <summary>
        /// Хеш-код.
        /// </summary>
        /// <returns>Хеш-код.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                lock (_translators)
                    return _translators.Sum(t => t.GetHashCode());
            }
        }

        /// <summary>
        /// Удалить все переводчики.
        /// </summary>
        public void Clear()
        {
            lock (_translators)
                _translators.Clear();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Количество загруженных переводчиков.
        /// </summary>
        public int Count
        {
            get
            {
                lock (_translators)
                    return _translators.Count;
            }
        }

        /// <summary>
        /// Получить переводчик.
        /// </summary>
        /// <param name="language">Язык.</param>
        /// <returns>Переводчик.</returns>
        public Translator this[string language]
        {
            get { return GetTranslator(language); }
        }

        /// <summary>
        /// Все доступные языки.
        /// </summary>
        public IEnumerable<string> Languages
        {
            get { return Translators.Select(t => t.Language); }
        }

        /// <summary>
        /// Все доступные переводчики.
        /// </summary>
        public IEnumerable<Translator> Translators
        {
            get
            {
                lock (_translators)
                    foreach (var t in _translators)
                        yield return t;
            }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сравнить содержимое двух менеджеров переводчиков.
        /// </summary>
        /// <param name="tm1">Менеджер переводчиков.</param>
        /// <param name="tm2">Менеджер переводчиков.</param>
        /// <returns>Равно ли содержимое менеджеров переводчиков.</returns>
        public static bool operator ==(TranslatorsManager tm1, TranslatorsManager tm2)
        {
            if (((object)tm1) == null && ((object)tm2) == null) return true;
            if (((object)tm1) == null || ((object)tm2) == null) return false;
            return tm1.CompareTo(tm2) == 0;
        }

        /// <summary>
        /// Сравнить содержимое двух менеджеров переводчиков.
        /// </summary>
        /// <param name="tm1">Менеджер переводчиков.</param>
        /// <param name="tm2">Менеджер переводчиков.</param>
        /// <returns>Не равно ли содержимое менеджеров переводчиков.</returns>
        public static bool operator !=(TranslatorsManager tm1, TranslatorsManager tm2)
        {
            return !(tm1 == tm2);
        }

        /// <summary>
        /// Добавить переводчик в менджер.
        /// </summary>
        /// <param name="tm">Менеджер переводчиков.</param>
        /// <param name="t">Переводчик.</param>
        /// <returns>Менеджер переводчиков.</returns>
        public static TranslatorsManager operator +(TranslatorsManager tm, Translator t)
        {
            tm.Add(t);
            return tm;
        }
        #endregion
    }
}