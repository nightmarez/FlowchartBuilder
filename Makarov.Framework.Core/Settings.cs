// <copyright file="Settings.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-07-12</date>
// <summary>Настройки программы.</summary>

using System;

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Настройки программы.
    /// </summary>
    public static class Settings
    {
        #region Environment
        public static class Environment
        {
            /// <summary>
            /// Запущено ли приложение под mono.
            /// </summary>
            private static bool? _isMono;

            /// <summary>
            /// Запущено ли приложение под mono.
            /// </summary>
            public static bool IsMono
            {
                get
                {
                    if (_isMono != null && _isMono.HasValue)
                        return _isMono.Value;

                    try
                    {
                        return (_isMono = Type.GetType("Mono.Runtime") != null).Value;
                    }
                    catch
                    {
                        return (_isMono = false).Value;
                    }
                }
            }
        }
        #endregion

        #region Xml
        /// <summary>
        /// Всё, что связано с Xml.
        /// </summary>
        public static class Xml
        {
            #region Xml Namespaces
            /// <summary>
            /// Пространства имён xml.
            /// </summary>
            public static class Namespaces
            {
                /// <summary>
                /// Базовое пространство имён приложения.
                /// </summary>
                public const string Base = @"http://flowchartbuilder.blogspot.com";
            }
            #endregion

            #region Xml Tags
            /// <summary>
            /// Имена тегов xml.
            /// </summary>
            public static class Tags
            {
                /// <summary>
                /// Настройки.
                /// </summary>
                public const string FrameworkSettings = @"fb:FrameworkSettings";

                /// <summary>
                /// Имя параметра.
                /// </summary>
                public const string Param = @"fb:Param";

                /// <summary>
                /// Главная нода с переводом.
                /// </summary>
                public const string Translation = @"fb:Translation";

                /// <summary>
                /// Перевод слова.
                /// </summary>
                public const string Word = @"fb:Word";
            }
            #endregion

            #region Xml Attributes
            /// <summary>
            /// Имена аттрибутов xml.
            /// </summary>
            public static class Attributes
            {
                /// <summary>
                /// Имя xml-атрибута, содержащего слово.
                /// </summary>
                public static string Key = @"Key";

                /// <summary>
                /// Имя xml-атрибута, определяющего язык.
                /// </summary>
                public static string Language = @"Language";
            }
            #endregion
        }
        #endregion
    }
}