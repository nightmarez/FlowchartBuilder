// <copyright file="StringDictionary.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-25</date>
// <summary>Строковой словарь.</summary>

using System;
using System.Collections.Generic;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Словарь строк.
    /// </summary>
    public class StringDictionary<T>: Dictionary<string, T>, IDict<T>
    {
        T IDict<T>.this[string key]
        {
            get { return base[key]; }
            set { base[key] = value; }
        }

        /// <summary>
        /// Существует ли заданный ключ.
        /// </summary>
        /// <param name="key">Ключ.</param>
        public bool IsKeyExists(string key)
        {
            return ContainsKey(key);
        }
    }
}
