// <copyright file="Loader.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-IV-28</date>
// <summary>Загрузчик типов из сборок.</summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Загрузчик типов из сборок.
    /// </summary>
    public sealed class Loader
    {
        #region Constructors
        /// <param name="directory">Папка, в которой нужно искать сборки.</param>
        public Loader(string directory)
        {
            // Получаем список сборок, находящихся в указанной папке.
            string[] files = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories);

            // Заполняем словарь найденных сборок.
            _assemblies = new Dictionary<string, string>(files.Length);
            foreach (string file in files)
            {
                // Имя сборки.
                string name = Path.GetFileNameWithoutExtension(file) ?? string.Empty;

                // Если такое имя уже есть, сгенерируем уникальное.
                if (_assemblies.ContainsKey(name))
                {
                    int i = -1;
                    string uniqName;
                    do
                    {
                        uniqName = name + "_" + (++i);
                    } while (_assemblies.ContainsKey(uniqName));

                    name = uniqName;
                }

                // Добавляем в словарь.
                _assemblies.Add(name, file);
            }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Найденные осборки.
        /// <remarks>Пары (полное имя, путь и имя файла с расширением).</remarks>
        /// </summary>
        private readonly Dictionary<string, string> _assemblies;

        /// <summary>
        /// Загруженные сборки.
        /// <remarks>Пары (полное имя, сборка).</remarks>
        /// </summary>
        private Dictionary<string, Assembly> _loadedAsms;
        #endregion

        #region Public methods
        /// <summary>
        /// Существует ли сборка с заданным именем.
        /// </summary>
        /// <param name="name">Имя сборки.</param>
        public bool IsAssemblyExists(string name)
        {
            return _assemblies.ContainsKey(name ?? string.Empty);
        }

        /// <summary>
        /// Загружена ли сборка с заданным именем.
        /// </summary>
        /// <param name="name">Имя сборки.</param>
        public bool IsAssemblyLoaded(string name)
        {
            return _loadedAsms.ContainsKey(name ?? string.Empty);
        }

        /// <summary>
        /// Возвращает сборку по имени.
        /// </summary>
        /// <param name="name">Имя сборки.</param>
        public Assembly GetAssembly(string name)
        {
            if (_loadedAsms.ContainsKey(name))
                return _loadedAsms[name];

            if (_assemblies.ContainsKey(name))
            {
                if (_loadedAsms == null)
                    _loadedAsms = new Dictionary<string, Assembly>(_assemblies.Count);

                Assembly asm = Assembly.LoadFile(name);
                _loadedAsms.Add(name, asm);
                return asm;
            }

            return null;
        }

        /// <summary>
        /// Список найденных сборок.
        /// </summary>
        public IEnumerable<string> Assemblies
        {
            get { return _assemblies.Select(kvp => kvp.Key); }
        }

        /// <summary>
        /// Список загруженных сборок.
        /// </summary>
        public IDictionary<string, Assembly> LoadedAssemblies
        {
            get
            {
                // Если ничего ещё не загружено...
                if (_loadedAsms == null)
                {
                    // Создаём словарь пар (имя сборки, сборка).
                    _loadedAsms = new Dictionary<string, Assembly>(_assemblies.Count);
                }

                // Проходим по всем найденным сборкам и догружаем то, что ещё не загружено...
                foreach (KeyValuePair<string, string> kvp in _assemblies)
                    if (!_loadedAsms.ContainsKey(kvp.Key))
                        _loadedAsms.Add(kvp.Key, Assembly.LoadFile(kvp.Value));

                // Возвращаем загруженные сборки.
                return _loadedAsms;
            }
        }

        /// <summary>
        /// Получить список классов, дочерних заданному, из загруженных сборок.
        /// </summary>
        /// <typeparam name="T">Тип, наследников которого нужно искать.</typeparam>
        /// <returns>Список найденных классов.</returns>
        public IEnumerable<Type> GetTypes<T>()
        {
            return from kvp in LoadedAssemblies 
                   from type in kvp.Value.GetTypes() 
                   where type is T || type.IsSubclassOf(typeof(T))
                   select type;
        }
        #endregion
    }
}