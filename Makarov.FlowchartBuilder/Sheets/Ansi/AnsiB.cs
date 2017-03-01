// <copyright file="AnsiB.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-30</date>
// <summary>Лист формата ANSI B.</summary>

using Makarov.FlowchartBuilder.API;
using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Ansi
{
    [SheetWidth(279)]
    [SheetHeight(432)]
    [SheetName("B (Vertical layout)")]
    public class VerticalAnsiB : BaseAnsi
    {
    }

    [SheetWidth(432)]
    [SheetHeight(279)]
    [SheetName("B (Horizontal layout)")]
    public class HorizontalAnsiB : BaseAnsi
    {
    }
}