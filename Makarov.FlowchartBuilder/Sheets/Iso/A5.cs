// <copyright file="A5.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-29</date>
// <summary>Лист формата A5.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Iso
{
    [SheetWidth(148)]
    [SheetHeight(210)]
    [SheetName("A5")]
    public class VerticalA5 : VerticalA
    {
    }

    [SheetWidth(210)]
    [SheetHeight(148)]
    [SheetName("A5")]
    public class HorizontalA5 : HorizontalA
    {
    }
}