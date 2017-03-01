// <copyright file="ColorSerializer.cs" company="Michael Makarov">
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
    public sealed class ColorSerializer : AbstractSerializer
    {
        /// <summary>
        /// Сериализуемый тип.
        /// </summary>
        public override Type SerializerType
        {
            get { return typeof(Color); }
        }

        /// <summary>
        /// Сериализует значение в строку.
        /// </summary>
        public override string Serialize(object obj)
        {
            var color = (Color)obj;
            return string.Format("{0};{1};{2};{3}", color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Десериализует строку в значение.
        /// </summary>
        public override object Deserialize(string str)
        {
            var arr = str.Split(';');
            return Color.FromArgb(
                byte.Parse(arr[3]),
                byte.Parse(arr[0]),
                byte.Parse(arr[1]),
                byte.Parse(arr[2]));
        }
    }
}