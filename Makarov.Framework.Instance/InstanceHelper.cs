// <copyright file="InstanceHelper.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-I-02</date>
// <summary>Помошник для доступа к экземпляру приложения.</summary>

using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace Makarov.Framework.Instance
{
    /// <summary>
    /// Помошник для доступа к экземпляру приложения.
    /// </summary>
    public static class InstanceHelper
    {
        /// <summary>
        /// Регистрирует объект-сервер.
        /// </summary>
        public static void RegisterServer(string portName, string objectUri, IInstance instance)
        {
            var channel = new IpcChannel(portName);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                instance.GetType(),
                objectUri,
                WellKnownObjectMode.Singleton);
        }

        /// <summary>
        /// Получает объект.
        /// </summary>
        public static IInstance GetObject(string portName, string serverPortName, string objectUri)
        {
            var channel = new IpcChannel(portName);
            ChannelServices.RegisterChannel(channel, false);
            return (IInstance) Activator.GetObject(
                typeof (IInstance),
                string.Format(@"ipc://{0}/{1}", serverPortName, objectUri));
        }
    }
}
