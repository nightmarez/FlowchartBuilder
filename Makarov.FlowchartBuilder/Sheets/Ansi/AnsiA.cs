// <copyright file="AnsiA.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-30</date>
// <summary>Лист формата ANSI A.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Ansi
{
    [SheetWidth(216)]
    [SheetHeight(279)]
    [SheetName("A (Vertical layout)")]
    public class VerticalAnsiA : BaseAnsi
    {
    }

    [SheetWidth(279)]
    [SheetHeight(216)]
    [SheetName("A (Horizontal layout)")]
    public class HorizontalAnsiA : BaseAnsi
    {
    }
}