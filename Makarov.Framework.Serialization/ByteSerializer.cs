// <copyright file="ByteSerializer.cs" company="Michael Makarov">
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
    public sealed class ByteSerializer : AbstractSerializer
    {
        /// <summary>
        /// Сериализуемый тип.
        /// </summary>
        public override Type SerializerType
        {
            get { return typeof(byte); }
        }

        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        public override string Serialize(object obj)
        {
            return ((byte)obj).ToString();
        }

        /// <summary>
        /// Десериализует строку в значение.
        /// </summary>
        public override object Deserialize(string str)
        {
            return byte.Parse(str);
        }
    }
}