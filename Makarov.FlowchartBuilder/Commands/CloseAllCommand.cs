// <copyright file="CloseAllCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-IV-18</date>
// <summary>Команда - закрыть всё.</summary>

using System;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - закрыть всё.
    /// </summary>
    public sealed class CloseAllCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new InvalidContextException(@"Document not exists.");

            throw new NotImplementedException();
        }
    }
}