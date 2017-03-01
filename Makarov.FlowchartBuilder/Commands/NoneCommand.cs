// <copyright file="NoneCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-II-06</date>
// <summary>Команда, которая не делает ничего.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда, которая не делает ничего.
    /// </summary>
    public sealed class NoneCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        { }
    }
}