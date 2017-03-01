// <copyright file="Core.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-21</date>
// <summary>Кора.</summary>

using System;
using System.Threading;
using Makarov.Framework.Core;
using Makarov.Framework.Instance;
using Makarov.Game.Server.Managers;

namespace Makarov.Game.Server
{
    /// <summary>
    /// Кора.
    /// </summary>
    public sealed class Core : Singleton<Core>
    {
        #region Public methods
        /// <summary>
        /// Запуск сервера.
        /// </summary>
        public void Run()
        {
            PrintExclamation("Server Start at " + DateTime.Now);
            DateTime time0 = DateTime.Now;

            using (Clients)
            {
                // Основной цикл.
                while (!ServerStopRequired)
                {
                    var timeStep = (int) System.Math.Ceiling((DateTime.Now - time0).TotalMilliseconds);
                    time0 = DateTime.Now;
                    PrintMessage("Main iteration start. Time step: " + timeStep);

                    if (timeStep <= 0)
                        timeStep = 1;

                    // Do events.
                    PrintMessage("Do events."); // TODO: remove this

                    Thread.Sleep(RandomGenerator.Instance.Next(500, 5000));

                    PrintMessage("Main iteration end.");
                }
            }

            PrintExclamation("Server Stop at " + DateTime.Now);
        }

        public void PrintMessage(string message)
        {
            ThreadSafeConsole.WriteLine(message);
        }

        public void PrintError(string errorMessage)
        {
            ThreadSafeConsole.WriteLine(errorMessage, ConsoleColor.Red);
        }

        public void PrintWarning(string warningMessage)
        {
            ThreadSafeConsole.WriteLine(warningMessage, ConsoleColor.Yellow);
        }

        public void PrintExclamation(string exclamationMessage)
        {
            ThreadSafeConsole.WriteLine(exclamationMessage, ConsoleColor.Cyan);
        }

        /// <summary>
        /// Запросить остановку сервера.
        /// </summary>
        /// <param name="reason">Причина.</param>
        public void StopServer(string reason)
        {
            PrintWarning("Server stop required. Reason: " + reason ?? string.Empty);
            ServerStopRequired = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Указывает, требуется ли отсановка сервера.
        /// </summary>
        public bool ServerStopRequired { get; private set; }

        /// <summary>
        /// Менеджер клиентов.
        /// </summary>
        public ClientsManager Clients
        {
            get { return _clients ?? (_clients = new ClientsManager()); }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Менеджер клиентов.
        /// </summary>
        private ClientsManager _clients;

        #endregion
    }
}
