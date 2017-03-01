// <copyright file="B4.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-30</date>
// <summary>Лист формата B4.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Iso
{
    [SheetWidth(250)]
    [SheetHeight(353)]
    [SheetName("B4")]
    public class VerticalB4 : VerticalB
    {
    }

    [SheetWidth(353)]
    [SheetHeight(250)]
    [SheetName("B4")]
    public class HorizontalB4 : HorizontalB
    {
    }
}