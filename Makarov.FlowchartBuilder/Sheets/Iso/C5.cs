// <copyright file="C5.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-30</date>
// <summary>Лист формата C5.</summary>

using Makarov.FlowchartBuilder.API.Attributes;

namespace Makarov.FlowchartBuilder.Sheets.Iso
{
    [SheetWidth(162)]
    [SheetHeight(229)]
    [SheetName("C5")]
    public class VerticalC5 : VerticalC
    {
    }

    [SheetWidth(229)]
    [SheetHeight(162)]
    [SheetName("C5")]
    public class HorizontalC5 : HorizontalC
    {
    }
}