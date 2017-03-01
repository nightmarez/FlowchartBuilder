// <copyright file="Exceptions.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-02-16</date>
// <summary>Исключения.</summary>

using System;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Исключение приложения.
    /// </summary>
    public class FlowchartBuilderException : ApplicationException
    {
        public FlowchartBuilderException()
            : base("Unknown exception in FlowchartBuilder.")
        { }

        /// <param name="message">Сообщение.</param>
        public FlowchartBuilderException(string message)
            : base(message)
        { }

        /// <param name="message">Сообщение.</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public FlowchartBuilderException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    /// <summary>
    /// Объект синглетона уже существует.
    /// </summary>
    public class SingletonObjectAlreayExistsException : FlowchartBuilderException
    {
        /// <param name="typeName">Имя типа.</param>
        public SingletonObjectAlreayExistsException(string typeName)
            : base(string.Format("Singleton object (of '{0}' class) already exists.", typeName))
        { }

        /// <param name="t">Тип.</param>
        public SingletonObjectAlreayExistsException(Type t)
            : this(t.Name)
        { }
    }

    /// <summary>
    /// Неправильное значение.
    /// </summary>
    public class InvalidValueException : FlowchartBuilderException
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
    }

    /// <summary>
    /// Неправильный тип ноды.
    /// </summary>
    public sealed class InvalidNodeTypeException : FlowchartBuilderException
    {
        /// <param name="nodeType">Тип ноды.</param>
        public InvalidNodeTypeException(string nodeType)
            : base(string.Format("Invalid node '{0}'.", nodeType))
        { }
    }

    /// <summary>
    /// Неправильное имя ноды.
    /// </summary>
    public sealed class InvalidNodeNameException : FlowchartBuilderException
    {
        /// <param name="nodeName">Имя ноды.</param>
        public InvalidNodeNameException(string nodeName)
            : base(string.Format("Invalid node '{0}'.", nodeName))
        { }
    }

    /// <summary>
    /// Действие произошло в неправильном контексте.
    /// </summary>
    public sealed class InvalidContextException : FlowchartBuilderException
    {
        public InvalidContextException()
            : base("Invalid context.")
        { }

        /// <param name="message">Сообщение.</param>
        public InvalidContextException(string message)
            : base(string.Format("Invalid context: {0}", message ?? string.Empty))
        { }
    }

    /// <summary>
    /// Не удалось получить серверный экземпляр приложения при remoting-взаимодействии.
    /// </summary>
    public sealed class CantGetServerAppInstanceException : FlowchartBuilderException
    {
        public CantGetServerAppInstanceException()
            : base(@"Can't get server application instance.")
        { }
    }
}