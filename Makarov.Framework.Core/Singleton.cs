// <copyright file="Singleton.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-VI-12</date>
// <summary>Синглетон.</summary>

using System;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Синглетон.
    /// </summary>
    public abstract class Singleton<T> where T : class
    {
        /// <summary>
        /// Экземпляр класса.
        /// </summary>
        private static T _obj;

        /// <summary>
        /// Возвращает экземпляр класса.
        /// </summary>
        public static T Instance
        {
            get
            {
                unchecked
                {
                    AccessCount++;
                }

                return _obj ?? (_obj = (T)Activator.CreateInstance(typeof(T), true));
            }
        }

        /// <summary>
        /// Счётчик доступа.
        /// </summary>
        public static long AccessCount { get; private set; }

        /// <summary>
        /// Создан ли уже экземпляр класса.
        /// </summary>
        public static bool IsInstanceExists
        {
            get { return _obj != null; }
        }

        /// <summary>
        /// Удаляет экземпляр класса из кеша.
        /// </summary>
        public static void FlushInstance()
        {
            _obj = null;
            AccessCount = 0;
        }
    }
}