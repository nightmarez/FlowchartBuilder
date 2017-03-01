// <copyright file="GlyphsHelper.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-II-18</date>
// <summary>Помошник для работы с глифами.</summary>

using System;
using System.Collections.Generic;
using System.Reflection;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Box
{
    /// <summary>
    /// Помошник для работы с глифами.
    /// </summary>
    public static partial class GlyphsHelper
    {
        /// <summary>
        /// Возвращает активные свойства объекта.
        /// </summary>
        /// <param name="obj">Объект.</param>
        public static PropertyInfo[] GetActiveProperties(object obj)
        {
            return GetActiveProperties(obj.GetType());
        }

        /// <summary>
        /// Возвращает активные свойства типа.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        public static PropertyInfo[] GetActiveProperties<T>()
        {
            return GetActiveProperties(typeof (T));
        }

        /// <summary>
        /// Возвращает активные свойства типа.
        /// </summary>
        /// <param name="type">Тип.</param>
        public static PropertyInfo[] GetActiveProperties(Type type)
        {
            var props = new List<PropertyInfo>();

            // Тип атрибута, помечающего активное свойство.
            Type activePropertyAttribType = typeof(ActivePropertyAttribute);

            // Получаем все свойства типа.
            PropertyInfo[] properties = type.GetProperties();

            // Проходим по свойствам...
            foreach (PropertyInfo property in properties)
            {
                // Ищем атрибут, указывающий, что данное свойство является активным.
                object[] attribs = property.GetCustomAttributes(activePropertyAttribType, true);

                // Найден атрибут - возвращаем активное свойство.
                if (attribs.Length == 1) props.Add(property);

                // Много атрибутов - непорядок, бросаем исключение.
                if (attribs.Length > 1)
                    throw new TooManyActivePropertyAttributesException(type.ToString(), property.Name);
            }

            return props.ToArray();
        }
    }
}
