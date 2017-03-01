// <copyright file="CommandWithSubcommands.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-V-30</date>
// <summary>Команда, содержащая подкоманды.</summary>

using System.Collections.Generic;
using System.Drawing;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда, содержащая подкоманды.
    /// </summary>
    public abstract class CommandWithSubcommands : Command
    {
        /// <summary>
        /// Выполнить подкоманду.
        /// </summary>
        /// <param name="name">Имя подкоманды.</param>
        public abstract void Run(string name);

        /// <summary>
        /// Подкоманды.
        /// </summary>
        public abstract IEnumerable<string> Commands { get; }

        /// <summary>
        /// Возвращает иконку подкоманды.
        /// </summary>
        /// <param name="cmd">Имя подкоманды.</param>
        /// <returns>Иконка подкоманды.</returns>
        public virtual Bitmap Icon(string cmd)
        {
            return null;
        }
    }
}