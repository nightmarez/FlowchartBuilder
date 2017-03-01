// <copyright file="Exceptions.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-02-11</date>
// <summary>Исключения.</summary>

using System;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Исключение фреймворка.
    /// </summary>
    public class MakarovFrameworkException : ApplicationException
    {
        public MakarovFrameworkException()
            : base("Unknown exception in Makarov.Framework.")
        { }

        /// <param name="message">Сообщение.</param>
        public MakarovFrameworkException(string message)
            : base(message)
        { }

        /// <param name="message">Сообщение.</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public MakarovFrameworkException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    /// <summary>
    /// Объект синглетона уже существует.
    /// </summary>
    public class SingletonObjectAlreadyExistsException : MakarovFrameworkException
    {
        /// <param name="typeName">Имя типа.</param>
        public SingletonObjectAlreadyExistsException(string typeName)
            : base(string.Format("Singleton object (of class '{0}') already exists.", typeName))
        { }

        /// <param name="t">Тип.</param>
        public SingletonObjectAlreadyExistsException(Type t)
            : this(t.Name)
        { }

        /// <param name="message">Сообщение.</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SingletonObjectAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    /// <summary>
    /// Неправильное значение.
    /// </summary>
    public class InvalidValueException : MakarovFrameworkException
    {
        /// <param name="entity">Сущность, содержащая неправильное значение.</param>
        public InvalidValueException(string entity)
            : base(string.Format("'{0}' contains invalid value.", entity))
        { }

        /// <param name="entity">Сущность, содержащая неправильное значение.</param>
        /// <param name="val">Значение.</param>
        public InvalidValueException(string entity, string val)
            : base(string.Format("'{0}' contains invalid value: '{1}'.", entity, val))
        { }

        /// <param name="message">Сообщение.</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public InvalidValueException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    /// <summary>
    /// Нулевая строка.
    /// </summary>
    public sealed class NullStringValueException : InvalidValueException
    {
        /// <param name="entity">Сущность, содержащая неправильное значение.</param>
        public NullStringValueException(string entity)
            : base(entity, "null")
        { }

        public NullStringValueException()
            : base("String", "null")
        { }

        /// <param name="message">Сообщение.</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public NullStringValueException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}