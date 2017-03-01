// <copyright file="IInstance.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-I-02</date>
// <summary>Интерфейс экземпляра приложения.</summary>

namespace Makarov.Framework.Instance
{
    /// <summary>
    /// Интерфейс экземпляра приложения.
    /// </summary>
    public interface IInstance
    {
        /// <summary>
        /// Послать сообщение.
        /// </summary>
        /// <param name="messages">Сообщения.</param>
        void Message(string[] messages);
    }
}