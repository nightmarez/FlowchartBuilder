// <copyright file="CommandedGlyphCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-VIII-14</date>
// <summary></summary>

using System;

namespace Makarov.FlowchartBuilder.Commands
{
    public abstract class CommandedGlyphCommand : Command
    {
        public Type GlyphType { get; set; }
    }
}