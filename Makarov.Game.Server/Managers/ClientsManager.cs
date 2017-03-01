// <copyright file="ClientsManager.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-21</date>
// <summary>Менеджер клиентов.</summary>

using System;

namespace Makarov.Game.Server.Managers
{
    /// <summary>
    /// Менеджер клиентов.
    /// </summary>
    public sealed class ClientsManager : IDisposable
    {
        #region Constructors
        public ClientsManager()
        {
            // Создать потоки для приёма и отправки данных.

            // TODO: у каждого клиента должен быть свой поток в классе Client
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Получает данные от клиентов.
        /// </summary>
        public void GetData()
        {
            
        }

        /// <summary>
        /// Отправляет данные клиентам.
        /// </summary>
        public void SetData()
        {
            
        }

        public void Dispose()
        {
            // Удалить потоки для приёма и отправки данных.
        }
        #endregion
    }
}
