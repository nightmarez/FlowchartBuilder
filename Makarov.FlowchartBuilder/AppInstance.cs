// <copyright file="AppInstance.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-25</date>
// <summary>Remoting-взаимодействие.</summary>

using System;
using System.Windows.Forms;
using Makarov.Framework.Instance;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Объект, представляющий экземпляр приложения при remoting-взаимодействии.
    /// Позволяет второму и последующим экземплярам приложения пересылать информацию
    /// первому о том, что нужно, например, открыть определённый документ.
    /// </summary>
    public sealed class AppInstance : MarshalByRefObject, IInstance
    {
        /// <summary>
        /// Передвигает на передний план главное окно приложения.
        /// </summary>
        private static void UpdateWindowState()
        {
            var mainWnd = Core.Instance.MainWindow;

            // Если окно в данный момент минимизировано,
            // нужно восстановить его предыдущее состояние...
            if (mainWnd.WindowState == FormWindowState.Minimized)
            {
                switch (mainWnd.OldWindowState)
                {
                    case FormWindowState.Normal:
                        mainWnd.WindowState = FormWindowState.Normal;
                        break;

                    case FormWindowState.Maximized:
                        mainWnd.WindowState = FormWindowState.Maximized;
                        break;

                    default:
                        mainWnd.WindowState = FormWindowState.Maximized;
                        break;
                }
            }
        }

        /// <summary>
        /// Сообщения от другого экземпляра.
        /// </summary>
        /// <param name="messages">Сообщения.</param>
        public void Message(string[] messages)
        {
            if (messages != null && messages.Length > 0)
            {
                // Если что-то передано, воспринимаем это как имена файлов,
                // которые нужно загрузить.
                foreach (string message in messages)
                    Core.Instance.AddDocument(new Document(message));
            }
            else
            {
                // Если ничего не передано, создаём новый документ.
                Core.Instance.AddDocument(new Document());
            }

            // Покажем главное окно.
            UpdateWindowState();
        }
    }
}