// <copyright file="Cache.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-01-31</date>
// <summary>Класс для кеширования ресурсов.</summary>

using System;
using System.Collections.Generic;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Класс для кеширования ресурсов.
    /// </summary>
    /// <typeparam name="T">Тип хранимых данных.</typeparam>
    public sealed class Cache<T> : IDisposable
    {
        /// <param name="obj">Объект, хранящий данные.</param>
        public Cache(IDict<T> obj)
        {
            _obj = obj;
        }

        /// <summary>
        /// Объект, хранящий данные.
        /// </summary>
        private IDict<T> _obj;

        /// <summary>
        /// Освобождение ресурсов и запись данных.
        /// </summary>
        public void Dispose()
        {
            if (_obj == null)
                return;

            if (_obj is IDisposable)
                ((IDisposable)_obj).Dispose();

            _dict.Clear();
            _obj = null;
        }

        /// <summary>
        /// Словарь, кеширующий данные.
        /// </summary>
        private readonly Dictionary<string, T> _dict = new Dictionary<string, T>();

        /// <summary>
        /// Возвращает значение.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <returns>Значение.</returns>
        public T GetValue(string name)
        {
            // Если значение есть в словаре, возвращаем его.
            if (_dict.ContainsKey(name))
                return _dict[name];

            // Считываем значение из источника.
            T val = _obj[name];

            // Записываем знаение в кеширующий словарь.
            _dict.Add(name, val);

            // Возвращаем значение.
            return val;
        }

        /// <summary>
        /// Сохраняет значение.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="val">Значение.</param>
        public void SetValue(string name, T val)
        {
            // Если значение есть в словаре...
            if (_dict.ContainsKey(name))
            {
                // Считываем значение.
                T v1 = _dict[name];

                // Сравниваем его с новым значением.
                if (!v1.Equals(val))
                {
                    // Если не равны, то записываем новое значение.
                    _dict[name] = val;
                    _obj[name] = val;
                }

                return;
            }

            // Если значения в словаре нет, записываем в словарь.
            _dict.Add(name, val);
            _obj[name] = val;
        }

        /// <summary>
        /// Доступ к значению.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <returns>Значение.</returns>
        public T this[string name]
        {
            get { return GetValue(name); }
            set { SetValue(name, value); }
        }

        /// <summary>
        /// Есть ли ключ в кеше или источнике.
        /// </summary>
        /// <param name="key">Ключ.</param>
        public bool KeyExists(string key)
        {
            return _dict.ContainsKey(key) || _obj.IsKeyExists(key);
        }
    }
}