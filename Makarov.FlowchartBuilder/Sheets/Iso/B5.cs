// <copyright file="B5.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-30</date>
// <summary>Лист формата B5.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Iso
{
    [SheetWidth(176)]
    [SheetHeight(250)]
    [SheetName("B5")]
    public class VerticalB5 : VerticalB
    {
    }

    [SheetWidth(250)]
    [SheetHeight(176)]
    [SheetName("B5")]
    public class HorizontalB5 : HorizontalB
    {
    }
}