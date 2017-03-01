// <copyright file="CharSerializer.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-11</date>
// <summary>Сериализатор.</summary>

using System;
using Makarov.Framework.Core;

namespace Makarov.Framework.Serialization
{
    public sealed class CharSerializer : AbstractSerializer
    {
        /// <summary>
        /// Сериализуемый тип.
        /// </summary>
        public override Type SerializerType
        {
            get { return typeof(char); }
        }

        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        public override string Serialize(object obj)
        {
            return ((char)obj).ToString();
        }

        /// <summary>
        /// Десериализует строку в значение.
        /// </summary>
        public override object Deserialize(string str)
        {
            return str[0];
        }
    }
}