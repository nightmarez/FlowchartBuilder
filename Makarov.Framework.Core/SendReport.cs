// <copyright file="SendReport.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2010 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2010-05-09</date>
// <summary>Класс для отправки отчёта об ошибке.</summary>

namespace Makarov.Framework.Core
{
    /// <summary>
    /// Класс для отправки отчёта об ошибке.
    /// </summary>
    public sealed class SendReport
    {
        /// <summary>
        /// Отправляет отчёт.
        /// </summary>
        /// <param name="mail">Адрес, на который нужно выслать письмо.</param>
        /// <param name="title">Заголовок письма.</param>
        /// <param name="info">Сообщение об ошибке.</param>
        public static bool Send(string mail, string title, string info)
        {
            try
            {
                
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}