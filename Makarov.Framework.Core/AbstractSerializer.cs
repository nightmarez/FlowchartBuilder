// <copyright file="AbstractSerializer.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-11</date>
// <summary>Сериализатор.</summary>

using System;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Сериализатор.
    /// </summary>
    public abstract class AbstractSerializer
    {
        /// <summary>
        /// Сериализуемый тип.
        /// </summary>
        public abstract Type SerializerType { get; }

        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        public abstract string Serialize(object obj);

        /// <summary>
        /// Десериализует строку в значение.
        /// </summary>
        public abstract object Deserialize(string str);
    }
}
