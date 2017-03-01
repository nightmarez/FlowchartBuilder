// <copyright file="B3.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-30</date>
// <summary>Лист формата B3.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Iso
{
    [SheetWidth(353)]
    [SheetHeight(500)]
    [SheetName("B3")]
    public class VerticalB3 : VerticalB
    {
    }

    [SheetWidth(500)]
    [SheetHeight(353)]
    [SheetName("B3")]
    public class HorizontalB3 : HorizontalB
    {
    }
}