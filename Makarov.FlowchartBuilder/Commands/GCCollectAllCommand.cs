// <copyright file="GCCollectCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2011 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2011-IV-07</date>
// <summary>Команда - собрать мусор.</summary>

using System;

namespace Makarov.FlowchartBuilder.Commands
{
    public sealed class GCCollectAllCommand : Command
    {
        public override void Run()
        {
            for (int i = 0; i < GC.MaxGeneration; i++)
                GC.Collect(i, GCCollectionMode.Forced);
        }
    }
}
