// <copyright file="FakeStorage.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-25</date>
// <summary>Класс для эмуляции хранилища настроек.</summary>

namespace Makarov.Framework.Core.SettingStorages
{
    /// <summary>
    /// Класс для эмуляции хранилища настроек.
    /// </summary>
    public sealed class FakeStorage : IDict<string>
    {
        private readonly StringDictionary<string> _dict = new StringDictionary<string>();

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
                    return;
                }

                _dict[x] = value;
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
    }
}