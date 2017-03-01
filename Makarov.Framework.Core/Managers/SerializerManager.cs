// <copyright file="SerializerManager.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-11</date>
// <summary>Менеджер сериализаторов.</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Makarov.Framework.Core.Managers
{
    /// <summary>
    /// Менеджер сериализаторов.
    /// </summary>
    public sealed class SerializerManager
    {
        #region Exceptions
        /// <summary>
        /// Базовое исключение менеджера сериализаторов.
        /// </summary>
        public abstract class SerializerManagerException : MakarovFrameworkException
        {
            /// <param name="message">Сообщение.</param>
            protected SerializerManagerException(string message)
                : base(message)
            { }
        }

        /// <summary>
        /// Сериализатор для данного типа уже существует в менеджере.
        /// </summary>
        public sealed class SerializerAlreadyExistsException : SerializerManagerException
        {
            /// <param name="type">Тип.</param>
            public SerializerAlreadyExistsException(string type)
                : base(string.Format(@"Serializer for type '{0}' already exists.", type ?? string.Empty))
            { }

            /// <param name="type">Тип.</param>
            public SerializerAlreadyExistsException(Type type)
                : this(type.Name)
            { }
        }

        /// <summary>
        /// Сериализатор для данного типа не найден.
        /// </summary>
        public sealed class SerializerNotExistsException : SerializerManagerException
        {
            /// <param name="type">Тип.</param>
            public SerializerNotExistsException(string type)
                : base(string.Format(@"Serializer for type '{0}' not exists.", type ?? string.Empty))
            { }

            /// <param name="type">Тип.</param>
            public SerializerNotExistsException(Type type)
                : this(type.Name)
            { }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Сериализаторы.
        /// </summary>
        private readonly List<AbstractSerializer> _serializers = new List<AbstractSerializer>();
        #endregion

        #region Constructors
        /// <param name="assemblies">Сборки, в которых нужно искать сериализаторы</param>
        public SerializerManager(IEnumerable<Assembly> assemblies)
        {
            AddFromAssemblies(assemblies);
        }

        /// <param name="assembly">Сборка, в которой нужно искать сериализаторы.</param>
        public SerializerManager(Assembly assembly)
        {
            AddFromAssembly(assembly);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        /// <param name="value">Значение.</param>
        public string Serialize(object value)
        {
            return Serialize(value.GetType(), value);
        }

        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <param name="value">Значение.</param>
        public string Serialize(Type type, object value)
        {
            foreach (AbstractSerializer serializer in _serializers)
                if (serializer.SerializerType == type)
                    return serializer.Serialize(value);

            throw new SerializerNotExistsException(type);
        }

        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <param name="value">Значение.</param>
        public string Serialize<T>(T value)
        {
            Type targetType = typeof(T);

            foreach (AbstractSerializer serializer in _serializers)
                if (serializer.SerializerType == targetType)
                    return serializer.Serialize(value);

            throw new SerializerNotExistsException(targetType);
        }

        /// <summary>
        /// Десериализует строку в значение указанного типа.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <param name="str">Строка.</param>
        public T Deserialize<T>(string str)
        {
            Type targetType = typeof(T);

            foreach (AbstractSerializer serializer in _serializers)
                if (serializer.SerializerType == targetType)
                    return (T)serializer.Deserialize(str);

            throw new SerializerNotExistsException(targetType);
        }

        /// <summary>
        /// Десериализует строку в значение указанного типа.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <param name="str">Строка.</param>
        public object Deserialize(Type type, string str)
        {
            foreach (AbstractSerializer serializer in _serializers)
                if (serializer.SerializerType == type)
                    return serializer.Deserialize(str);

            throw new SerializerNotExistsException(type);
        }

        /// <summary>
        /// Добавляет сериализатор.
        /// </summary>
        /// <param name="serializer">Сериализатор.</param>
        /// <param name="throwOnExists">Бросать исключение, если сериализатор для типа уже существует.
        /// В противном случае, заменяет на новый сериализатор.</param>
        public void AddSerializer(AbstractSerializer serializer, bool throwOnExists = true)
        {
            if (!IsExists(serializer.SerializerType) || !throwOnExists)
                _serializers.Add(serializer);
            else
                throw new SerializerAlreadyExistsException(serializer.SerializerType);
        }

        /// <summary>
        /// Добавляет сериализаторы из указанной сборки.
        /// </summary>
        /// <param name="assembly">Сборка.</param>
        public void AddFromAssembly(Assembly assembly)
        {
            AddFromAssemblies(new[] { assembly });
        }

        /// <summary>
        /// Добавляет сериализаторы из указанных сборок.
        /// </summary>
        /// <param name="assemblies">Сборки.</param>
        public void AddFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                    if (type.IsClass && type.IsSubclassOf(typeof (AbstractSerializer)))
                        AddSerializer((AbstractSerializer) Activator.CreateInstance(type));
            }
        }

        /// <summary>
        /// Есть ли сериализатор для заданного типа.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        public bool IsExists<T>()
        {
            return IsExists(typeof (T));
        }

        /// <summary>
        /// Есть ли сериализатор для заданного типа.
        /// </summary>
        /// <param name="type">Тип.</param>
        public bool IsExists(Type type)
        {
            return _serializers.Any(serializer => serializer.SerializerType == type);
        }

        /// <summary>
        /// Удаляет все загруженные сериализаторы.
        /// </summary>
        public void Clear()
        {
            _serializers.Clear();
        }
        #endregion
    }
}
