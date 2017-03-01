// <copyright file="DoubleSerializer.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-11</date>
// <summary>Сериализатор.</summary>

using System;
using System.Globalization;
using Makarov.Framework.Core;

namespace Makarov.Framework.Serialization
{
    public sealed class DoubleSerializer : AbstractSerializer
    {
        /// <summary>
        /// Сериализуемый тип.
        /// </summary>
        public override Type SerializerType
        {
            get { return typeof(double); }
        }

        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        public override string Serialize(object obj)
        {
            return ((double)obj).ToString("r", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Десериализует строку в значение.
        /// </summary>
        public override object Deserialize(string str)
        {
            return double.Parse(str, CultureInfo.InvariantCulture);
        }
    }
}