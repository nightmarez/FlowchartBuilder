// <copyright file="FontSerializer.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-12-11</date>
// <summary>Сериализатор.</summary>

using System;
using System.Drawing;
using System.Globalization;
using Makarov.Framework.Core;

namespace Makarov.Framework.Serialization
{
    public sealed class FontSerializer : AbstractSerializer
    {
        /// <summary>
        /// Сериализуемый тип.
        /// </summary>
        public override Type SerializerType
        {
            get { return typeof(Font); }
        }

        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        public override string Serialize(object obj)
        {
            var font = (Font)obj;
            return font.Name + ";" + font.Size.ToString("r", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Десериализует строку в значение.
        /// </summary>
        public override object Deserialize(string str)
        {
            string[] pairs = str.Split(';');
            return Fonts.SafeCreateFont(pairs[0], float.Parse(pairs[1], CultureInfo.InvariantCulture));
        }
    }
}