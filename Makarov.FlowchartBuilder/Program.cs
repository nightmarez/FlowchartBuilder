// <copyright file="Program.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-II-16</date>
// <summary>Основной класс программы.</summary>

using System;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Forms;
using Makarov.Framework.Core;
using Makarov.Framework.Instance;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Основной класс программы.
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// Основная функция программы.
        /// </summary>
        /// <param name="args">Аргументы.</param>
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                // Серверный экземпляр приложения. Если он создан не будет,
                // скорее всего он уже создан, значит текущий экземпляр - не первый.
                IInstance instance = null;

                try
                {
                    // Пытаемся зарегистрировать сервер.
                    InstanceHelper.RegisterServer(
                        Settings.Environment.ServerPort,
                        Settings.Environment.ApplicationName,
                        new AppInstance());
                }
                catch (RemotingException)
                {
                    // Зарегистрировать сервер не удалось - пытаемся подключиться к серверу.
                    instance = InstanceHelper.GetObject(
                        Settings.Environment.ClientPort,
                        Settings.Environment.ServerPort,
                        Settings.Environment.ApplicationName);

                    // Подключиться к серверу не удалось - что-то не работает.
                    if (instance == null)
                        throw new CantGetServerAppInstanceException();
                }

                if (instance == null)
                {
                    // Если это первый экземпляр приложения, запускаем его...
                    Application.ThreadException += Application_ThreadException;
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    using (Core.Instance)
                    {
                        // Запускаем основное окно...
                        Core.Instance.Run(args);
                    }
                }
                else
                {
                    // Если это не первый экземпляр приложения, нужно передать в первый
                    // информацию об открываемых файлах.
                    instance.Message(args.Length == 0 ? null : args);
                }
            }
            catch (Exception e)
            {
                ShowErrorMessage(e.GetType().Name, e.Message, e.StackTrace);
            }
            catch
            {
                ShowErrorMessage(string.Empty, string.Empty, string.Empty);
            }
        }

        // ReSharper disable InconsistentNaming
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowErrorMessage(e.Exception.GetType().Name, e.Exception.Message, e.Exception.StackTrace);
        }
        // ReSharper restore InconsistentNaming

        /// <summary>
        /// Отображает сообщение об ошибке.
        /// </summary>
        /// <param name="exception">Имя класса исключения.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="stackTrace">Стек.</param>
        private static void ShowErrorMessage(string exception, string message, string stackTrace)
        {
            new CriticalErrorDialog(exception, message, stackTrace,
                Settings.Environment.ErrorReportMail,
                Settings.Environment.ErrorReportTitle).ShowDialog();
        }
    }
}
