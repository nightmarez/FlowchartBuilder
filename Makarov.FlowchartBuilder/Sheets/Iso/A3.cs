// <copyright file="A3.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-29</date>
// <summary>Лист формата A3.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Iso
{
    [SheetWidth(297)]
    [SheetHeight(420)]
    [SheetName("A3")]
    public class VerticalA3 : VerticalA
    {
    }

    [SheetWidth(420)]
    [SheetHeight(297)]
    [SheetName("A3")]
    public class HorizontalA3 : HorizontalA
    {
    }
}