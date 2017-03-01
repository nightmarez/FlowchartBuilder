// <copyright file="A4.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-29</date>
// <summary>Лист формата A4.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Iso
{
    [SheetWidth(210)]
    [SheetHeight(297)]
    [SheetName("A4")]
    public class VerticalA4 : VerticalA
    {
    }

    [SheetWidth(297)]
    [SheetHeight(210)]
    [SheetName("A4")]
    public class HorizontalA4 : HorizontalA
    {
    }
}