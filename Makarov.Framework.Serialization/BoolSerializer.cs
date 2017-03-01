// <copyright file="BoolSerializer.cs" company="Michael Makarov">
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
    public sealed class BoolSerializer : AbstractSerializer
    {
        /// <summary>
        /// Сериализуемый тип.
        /// </summary>
        public override Type SerializerType
        {
            get { return typeof(bool); }
        }

        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        public override string Serialize(object obj)
        {
            var b = (bool) obj;
            if (b) return @"True";
            return @"False";
        }

        /// <summary>
        /// Десериализует строку в значение.
        /// </summary>
        public override object Deserialize(string str)
        {
            if (str == @"True" ||
                str == @"TRUE" ||
                str == @"true" ||
                str.ToLowerInvariant() == @"true" ||
                str.ToUpperInvariant() == @"TRUE")
                return true;

            if (str == @"False" ||
                str == @"FALSE" ||
                str == @"false" ||
                str.ToLowerInvariant() == @"false" ||
                str.ToUpperInvariant() == @"FALSE")
                return false;

            if (str == bool.TrueString)
                return true;

            if (str == bool.FalseString)
                return false;

            return bool.Parse(str);
        }
    }
}