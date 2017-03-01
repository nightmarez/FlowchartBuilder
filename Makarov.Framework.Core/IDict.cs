// <copyright file="IDict.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-25</date>
// <summary>Интерфейс словаря, к элементам которого можно обращаться по строковому ключу.</summary>

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Интерфейс словаря, к элементам которого можно обращаться по
    /// строковому ключу.
    /// </summary>
    /// <typeparam name="T">Тип хранимых данных.</typeparam>
    public interface IDict<T>
    {
        /// <summary>
        /// Доступ к элементам по ключу.
        /// </summary>
        T this[string x] { get; set; }

        /// <summary>
        /// Есть ли ключ в словаре.
        /// </summary>
        bool IsKeyExists(string key);
    }
}